////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: ucCamera.cs
//FileType: Visual C# Source file
//Author : Jason Jay
//Created On : 2022.10.13 
//Last Modified On : 2023.5.10 
//Copy Rights : ShinBo Ltd.
//Description : Class for Star Camera ASI2600MMPro, control interface  DLL given.
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ZWO_ASI_Test.ASICameraDll2;
using ZWO_ASI_Test;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Cam_ASI2600MMPro_Test
{
    public partial class ucCamera : UserControl
    {
        ASICameraDll2 asicamera = new ASICameraDll2();
        public event delegate_Logger Log;

        const string THIS = "ucCAM";
        protected int cooler_onoff;
        protected int target_temperature;
        protected int gain;
        protected int exposureTime;
        protected int flip;

        const int ID = 0;  //우선 1번 CAM 만

        int sensor_temperature;
        int cameras;
        string property_name;
        string resolution;

        ASI_CAMERA_INFO pASICameraInfo;
        ASI_BAYER_PATTERN bayerPattern;
        ASI_EXPOSURE_STATUS exp_status;
        ASI_CONTROL_CAPS caps;
        ASI_CONTROL_TYPE control_types;

        string supportedBins;
        string supportedVideoFormat;

        IntPtr ImageBuf;  //For Preview
        ASI_IMG_TYPE IMG_TYPE = ASI_IMG_TYPE.ASI_IMG_RAW16;
        private int ROI_X = 6248;  //6248  3124  1562  781  390  195  97
        private int ROI_Y = 4176;  //4176  2088  1044  522  261  130  65  
        private int _WIDTH = 6248;
        private int _HEIGHT = 4176;
        private int START_POS_X = 0;
        private int START_POS_Y = 0;
        private int BIN = 2;
        private int TARGET_TEMP = 170;

         
        private int WIDTH
        {
            get { return _WIDTH; }
            set { _WIDTH = value; }
        }
        private int HEIGHT
        {
            get { return _HEIGHT; }
            set { _HEIGHT = value; }
        }

        string path = Application.StartupPath;

        public ucCamera()
        {
            InitializeComponent();

            ASI_ERROR_CODE result;

            result = Initialization();
            if (result != ASI_ERROR_CODE.ASI_SUCCESS)
                Environment.Exit(0);

            ASISetControlValue(ID, ASI_CONTROL_TYPE.ASI_TARGET_TEMP, TARGET_TEMP); //200:

        }

        public ASI_CAMERA_INFO GetCameraProperty()
        {
            return pASICameraInfo;
        }


        public int Control_CoolerOnOff
        {
            get { return cooler_onoff; }
            set
            {
                ASISetControlValue(ID, ASI_CONTROL_TYPE.ASI_COOLER_ON, value);
                cooler_onoff = ASIGetControlValue(ID, ASI_CONTROL_TYPE.ASI_COOLER_ON);
            }
        }
        public int Control_Gain
        {
            get { return gain; }
            set
            {
                ASISetControlValue(ID, ASI_CONTROL_TYPE.ASI_GAIN, value);
                gain = ASIGetControlValue(ID, ASI_CONTROL_TYPE.ASI_GAIN);
            }
        }

        public int Control_ExposureTime
        {
            get { return exposureTime; }
            set
            {
                ASISetControlValue(ID, ASI_CONTROL_TYPE.ASI_EXPOSURE, value);
                exposureTime = ASIGetControlValue(ID, ASI_CONTROL_TYPE.ASI_EXPOSURE);
            }
        }
        public int Control_Flip
        {
            get { return flip; }
            set
            {
                ASISetControlValue(ID, ASI_CONTROL_TYPE.ASI_FLIP, value);
                Log(LOG.D, THIS, $"Flip Set {value}");
                flip = ASIGetControlValue(ID, ASI_CONTROL_TYPE.ASI_FLIP);
                Log(LOG.D, THIS, $"Flip Get {flip}");
            }
        }

        public int SensorTemperature
        {
            get
            {
                sensor_temperature = ASIGetControlValue(ID, ASI_CONTROL_TYPE.ASI_TEMPERATURE);
                return sensor_temperature;
            }
        }

        public int TargetTemperature
        {
            get
            {
                target_temperature = ASIGetControlValue(ID, ASI_CONTROL_TYPE.ASI_TARGET_TEMP);
                //Log(LOG.I, THIS, $"[CMD] ({target_temperature}) <--- ASIGetControlValue()......");
                //Console.WriteLine($"[CAM] GET teget_temperature ... {target_temperature}");
                return target_temperature;
            }
            set
            {
                ASISetControlValue(ID, ASI_CONTROL_TYPE.ASI_TARGET_TEMP, value);
                target_temperature = ASIGetControlValue(ID, ASI_CONTROL_TYPE.ASI_TARGET_TEMP);
                //Log(LOG.I, THIS, $"[CMD] ({target_temperature}) <--- ASISetControlValue()......");
                //Console.WriteLine($"[CAM] PUT teget_temperature ... {target_temperature}");
            }
        }


        public ASI_ERROR_CODE Prepate_Preview()
        {
            ASI_ERROR_CODE result;
            //int iDropFrame;
            //int image_count = 0;
            int size = WIDTH * HEIGHT;

            ImageBuf = Marshal.AllocCoTaskMem(size);

            result = ASISetROIFormat(ID, ROI_X, ROI_Y, BIN, IMG_TYPE);
            Log(LOG.I, THIS, $"[CMD] ({result}) <--- ASISetROIFormat( {ROI_X}, {ROI_Y} , bin2 ,  {IMG_TYPE})......");

            result = ASIStartVideoCapture(ID);
            Log(LOG.I, THIS, $"[CMD] ({result}) <--- ASIStartVideoCapture()......");

            return result;
        }

        public void Preview()
        {
            ASI_ERROR_CODE result;
            int size = WIDTH * HEIGHT;

            var exposureTime = ASIGetControlValue(ID, ASI_CONTROL_TYPE.ASI_EXPOSURE);
            exposureTime /= 1000;
            result = ASIGetVideoData(ID, ImageBuf, size, exposureTime * 2 + 500);
            Mat mat = new Mat(HEIGHT, WIDTH, MatType.CV_8U, ImageBuf);
            if (result == ASI_ERROR_CODE.ASI_SUCCESS)
            {   
                pictureBox1.Image = BitmapConverter.ToBitmap(mat);
            }
            Log(LOG.I, THIS, $"[CMD] ({result}) <--- ASIGetVideoData()");
        }

        public string  DoCapture()
        {
            string file = @"Capture_" + DateTime.Now.ToString("MM-dd-yyyy_hh-mm-ss") + ".png";
            pictureBox1.Image.Save(path + file, System.Drawing.Imaging.ImageFormat.Png);
            return file;
        }

        public void Stop_Preview()
        {
            ASI_ERROR_CODE result;

            result = ASIStopVideoCapture(ID);
            Log(LOG.I, THIS, $"[CMD] ({result}) <--- ASIStartVideoCapture()......");
        }

        private ASI_ERROR_CODE Initialization()
        {
            ASI_ERROR_CODE result;

            cameras = asicamera.ASIGetNumOfConnectedCameras();
            pASICameraInfo.NumOfCameras = cameras;
            if (cameras < 1)
            {
                MessageBox.Show("There's No Camera !!");
                Console.WriteLine("There's No Camera !!");
                return ASI_ERROR_CODE.ASI_ERROR_INVALID_INDEX;
            }
            // Device Info
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < cameras; i++)
            {
                result = ASIGetCameraProperty(out pASICameraInfo, 0);
                property_name = Encoding.Default.GetString(pASICameraInfo.name);
                //ID = pASICameraInfo.CameraID;
                Console.WriteLine($"Get Property CamID : [{ID}] {property_name} ");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");
                resolution = $"[{pASICameraInfo.MaxWidth}] x [{pASICameraInfo.MaxHeight}]";
                Console.WriteLine($"Get Property MaxWidth x MaxHeight :{resolution}");

                bayerPattern = pASICameraInfo.BayerPattern;
                Console.WriteLine($"Get Property ColorCam? : [{pASICameraInfo.IsColorCam is ASI_BOOL.ASI_TRUE}]");
                Console.WriteLine($"Get Property BayerPattern : [{bayerPattern}]");
                supportedBins = string.Join(" ", pASICameraInfo.SupportedBins);
                Console.WriteLine($"Get Property SupportedBins : {supportedBins}");//SupportedBins
                supportedVideoFormat = string.Join("  ", pASICameraInfo.SupportedVideoFormat);
                Console.WriteLine($"Get Property SupportedVideoFormat : {supportedVideoFormat}");//supportedVideoFormat
                Console.WriteLine($"Get Property Pixel Size  {pASICameraInfo.PixelSize}");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");
            }

            //#1 Open the Camera
            Console.WriteLine("\n");
            result = ASIOpenCamera(0);
            Console.WriteLine($"[INITIAL] ({result}) <--- ASIOpenCamera(0)");

            //#2 Init the Camera
            result = ASIInitCamera(0);
            Console.WriteLine($"[INITIAL] ({result}) <--- ASIInitCamera(0)");

            //#3 ASIGetNumOfControls
            result = ASIGetNumOfControls(0, out int numberOfControls);
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"[INITIAL] ({result}) <--- ASIGetNumOfControls().....[Number Of Controls] ({numberOfControls})");
            Console.WriteLine("----------------------------------------------------------------------------");
            //[CMD] (ASI_SUCCESS) ASIGetNumOfControls().......[Number Of Controls] (15)

            //#4
            int control_value;
            for (int num = 0; num < numberOfControls; num++)
            {
                result = ASIGetControlCaps(0, num, out caps);
                control_value = ASIGetControlValue(0, (ASI_CONTROL_TYPE)num);
                Console.WriteLine($"[INITIAL] ({result}) <--- ASIGetControlCaps().......[Control #{num}] ({caps.Description} : {control_value} ])     (** default : {caps.DefaultValue}   Range : {caps.MinValue} ~ {caps.MaxValue})");
            }
            //// Result ////
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #0] (Gain : [ 120 ])     (** default : 200   Range : 0 ~ 700)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #1] (Exposure Time(us) : [ 10000 ])     (** default : 10000   Range : 32 ~ 2000000000)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #2] (offset : [ 50 ])     (** default : 1   Range : 0 ~ 240)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #3] (The total data transfer rate percentage : [ 0 ])     (** default : 50   Range : 40 ~ 100)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #4] (Flip: 0->None 1->Horiz 2->Vert 3->Both : [ 0 ])     (** default : 0   Range : 0 ~ 3)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #5] (Auto exposure maximum gain value : [ 1 ])     (** default : 350   Range : 0 ~ 700)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #6] (Auto exposure maximum exposure value(unit ms) : [ 100 ])     (** default : 100   Range : 1 ~ 60000)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #7] (Auto exposure target brightness value : [ 0 ])     (** default : 100   Range : 50 ~ 160)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #8] (Is hardware bin2:0->No 1->Yes : [ 0 ])     (** default : 0   Range : 0 ~ 1)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #9] (Is high speed mode:0->No 1->Yes : [ 0 ])     (** default : 0   Range : 0 ~ 1)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #10] (Sensor temperature(degrees Celsius) : [ 350 ])     (** default : 20   Range : -500 ~ 1000)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #11] (Cooler power percent : [ 30000 ])     (** default : 0   Range : 0 ~ 100)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #12] (Target temperature(cool camera only) : [ 100 ])     (** default : 0   Range : -40 ~ 30)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #13] (turn on/off cooler(cool camera only) : [ 0 ])     (** default : 0   Range : 0 ~ 1)
            //[CMD] (ASI_SUCCESS) ASIGetControlCaps().......[Control #14] (turn on/off anti dew heater(cool camera only) : [ 0 ])     (** default : 0   Range : 0 ~ 1)

            //  Set image size and format --> ASISetROIFormat
            result = ASISetROIFormat(ID, ROI_X, ROI_Y, BIN, IMG_TYPE);
            Console.WriteLine($"[INITIAL] ({result}) <--- ASISetROIFormat().....");

            //  Set start position when ROI --> ASISetStartPos
            result = ASISetStartPos(ID, START_POS_X, START_POS_Y);
            Console.WriteLine($"[INITIAL] ({result}) <--- ASISetStartPos().....");

            return ASI_ERROR_CODE.ASI_SUCCESS;
        }


    }
}
