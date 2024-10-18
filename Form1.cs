using static ZWO_ASI_Test.ASICameraDll2;
using ZWO_ASI_Test;
using System.Text;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.Metrics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;
using System.Threading.Channels;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.ComponentModel;
using System.IO;
using OpenCvSharp;
using System.Timers;
using Application = System.Windows.Forms.Application;

namespace Cam_ASI2600MMPro_Test
{
    public partial class Form1 : Form
    {
        const string THIS = "MainForm";
        ucCamera ucCAM;

        ASI_CAMERA_INFO ID;
        const int CAM_SATELLIT = 0;
        const int CAM_DEBRIESS = 1;

        VideoCapture video;
        Mat frame = new Mat();

        // //////////////////////////////////////////////
        private BackgroundWorker bgWorker;
        private BackgroundWorker bgWorkerPreview;
        // //////////////////////////////////////////////



        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            ucCAM = new ucCamera();
            ucCAM.Log += Uc_Logger;

            ID = ucCAM.GetCameraProperty();
            panel_image.Controls.Add(ucCAM);

            if (ID.NumOfCameras > 0)
            {
                this.Text += $"   > Supported Video Format : [{string.Join(", ", ID.SupportedVideoFormat)}]";
            }

            // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(BackgroundWorker1_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            bgWorker.RunWorkerAsync();

            bgWorkerPreview = new BackgroundWorker();
            bgWorkerPreview.WorkerReportsProgress = true;
            bgWorkerPreview.WorkerSupportsCancellation = true;
            bgWorkerPreview.DoWork += new DoWorkEventHandler(bgWorkerPreview_DoWork);
            bgWorkerPreview.ProgressChanged += bgWorkerPreview_ProgressChanged;
            bgWorkerPreview.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorkerPreview_RunWorkerCompleted);
            // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ImageList imgList = new ImageList();
            treeView1.ImageList = imgList;

            set_cooler_control();

            // 첫번째 TreeView 아이템 - 서버
            TreeNode camera1 = new TreeNode("Camera #1", 0, 0);
            camera1.Nodes.Add("해상도", $"해상도 [ {ID.MaxWidth} x {ID.MaxHeight} ]", 0, 0);
            camera1.Nodes.Add("BayerPattern", $"BayerPattern [ {ID.BayerPattern} ]", 0, 0);
            camera1.Nodes.Add("SupportedBin", $"SupportedBin [ {string.Join(" ", ID.SupportedBins)} ]", 0, 0);
            camera1.Nodes.Add("PixelSize", $"PixelSize [ {ID.PixelSize} ]", 0, 0);
            camera1.Nodes.Add("TargetTemperature", $"TargetTemperature [ {ucCAM.TargetTemperature}°C ]", 0, 0);
            camera1.Nodes.Add("SesorTemperature", $"SensorTemperature [ {ucCAM.SensorTemperature}°C ]", 0, 0);

            // 두번째 TreeView 아이템 - 네트웍
            TreeNode camera2 = new TreeNode("Camenra #2", 1, 1);
            camera2.Nodes.Add("해상도", $"해상도 [ {ID.MaxWidth} x {ID.MaxHeight} ]", 0, 0);
            camera2.Nodes.Add("BayerPattern", $"BayerPattern [ {ID.BayerPattern} ]", 0, 0);
            camera2.Nodes.Add("SupportedBin", $"SupportedBin [ {string.Join(" ", ID.SupportedBins)} ]", 0, 0);
            camera2.Nodes.Add("PixelSize", $"PixelSize [ {ID.PixelSize} ]", 0, 0);

            // 2개의 노드를 TreeView에 추가
            treeView1.Nodes.Add(camera1);
            treeView1.Nodes.Add(camera2);

            // 모든 트리 노드를 보여준다
            treeView1.ExpandAll();
        }


        private void set_cooler_control()
        {
            if (ucCAM.SensorTemperature > ucCAM.TargetTemperature)
                ucCAM.Control_CoolerOnOff = 1;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ASI_ERROR_CODE result;

            // [ 5 ] < Close Camera >
            result = ASICloseCamera(0);
            Log(LOG.D, $"[CMD] ASICloseCamera(0)....{result}...");
        }



        // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Make a work delay 
            Thread.Sleep(5000);
            //e.Result = "This text was set safely by BackgroundWorker.";
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Console.WriteLine($"BackGrndWork_RunWorkerCompleted()...{e.Result.ToString()}");
            var control_value = ucCAM.SensorTemperature;
            var control_value1 = ucCAM.TargetTemperature;

            if (ucCAM.SensorTemperature > ucCAM.TargetTemperature + 3)
                ucCAM.Control_CoolerOnOff = 1;
            else if (ucCAM.SensorTemperature - 3 < ucCAM.TargetTemperature)
                ucCAM.Control_CoolerOnOff = 0;

            //Log(LOG.I, $"{THIS} Check the Sensor temperature : {control_value / 10}°C   TargetTemperature : {control_value1}°C");
            label_tempSense.Text = $"{control_value / 10}°C";

            bgWorker.RunWorkerAsync();
        }
        // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void bgWorkerPreview_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;

            //Make a work delay
            Thread.Sleep(50);

            while (true)//for (int i = 0; i <= 100; i++)
            {
                if (bgWorkerPreview.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                bgWorkerPreview.ReportProgress(i++);
                System.Threading.Thread.Sleep(300);
                if (i > 100)
                    i = 0;
            }
            //e.Result = 42;
            //Log(LOG.D, $"backgroundWorker_preview_DoWork()   CheckBox Preview [ {checkBox_Preview.Checked} ].....WorkCancel {e.Cancel}");
        }

        void bgWorkerPreview_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ucCAM.Preview();
            //Log(LOG.D, $"bgWorkerPreview_ProgressChanged()  Working {e.ProgressPercentage}%");
        }

        private void bgWorkerPreview_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log(LOG.D, $"bgWorkerPreview_RunWorkerCompleted()  Preview [ {checkBox_Preview.Checked} ] / Worker Cancelation {e.Cancelled}");
        }
        // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        // On/Off Cooler 
        //
        private void Cooler_CheckedChanged(object sender, EventArgs e)
        {
            int control_value;

            if (checkBox1.Checked == true)
            {
                // Connect         
                checkBox1.Text = "Cooler ON";
                control_value = ucCAM.Control_CoolerOnOff = 1;
            }
            else
            {
                // Disconnect
                checkBox1.Text = "Cooler Off ";
                control_value = ucCAM.Control_CoolerOnOff = 0;
            }
            Log(LOG.D, $"Cooler [ {control_value} ]");
        }

        //
        // On/Off Camera 
        //
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ASI_ERROR_CODE result;

            if (checkBox2.Checked == true)
            {
                //checkBox2.Text = "Open";
                result = ASIOpenCamera(0);
                Log(LOG.D, $"[CMD] ASIOpenCamera(0)....{result}...");
                result = ASIInitCamera(0);
                Log(LOG.D, $"[CMD] ASIInitCamera(0)....{result}...");
            }
            else
            {
                // Disconnect
                //checkBox2.Text = "Close";
                result = ASICloseCamera(0);
                Log(LOG.D, $"[CMD] ASICloseCamera(0)....{result}...");
            }
        }

        #region Log OverLoading
        private void Uc_Logger(LOG eLevel, object sender, string strLog)
        {
            Log(eLevel, $"[{sender.ToString()}]    {strLog}");
        }

        private void Log(LOG eLevel, string LogDesc)
        {
            Brush msgBrush;

            DateTime dTime = DateTime.Now;
            string LogInfo = $" {dTime:yyyy-MM-dd hh:mm:ss.fff}   [ {eLevel.ToString()} ]   {LogDesc} {Environment.NewLine}";
            if (eLevel == LOG.E)
                richMsgBox.SelectionColor = Color.Red;
            else if (eLevel == LOG.W)
                richMsgBox.SelectionColor = Color.Green;
            else
                richMsgBox.SelectionColor = Color.Black;
            richMsgBox.AppendText(LogInfo);
            richMsgBox.ScrollToCaret();
        }


        private void Log(DateTime dTime, LOG eLevel, string LogDesc)
        {
            string LogInfo = $" {dTime:yyyy-MM-dd hh:mm:ss.fff}  [ {eLevel.ToString()} ]   {LogDesc}";
            //richMsgBox.Items.Insert(0, LogInfo);
        }
        #endregion

        private void gainControler_ValueChanged(object sender, EventArgs e)
        {
            int value;

            value = ucCAM.Control_Gain = (int)gainControler.Value;
            Log(LOG.D, $"Set Gain ------>  [ {value} ]");
        }


        private void exposureTime_ValueChanged(object sender, EventArgs e)
        {
            int value;
            value = ucCAM.Control_ExposureTime = (int)exposureTime.Value;
            Log(LOG.D, $"Set Exposure Time(us)  ------>  [ {value} ]");
            label_exposureTime.Text = $"ExposureTime(us) : {value.ToString()}";
        }



        private void comboBox_Flip_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value;

            value = ucCAM.Control_Flip = comboBox_Flip.SelectedIndex;
            Log(LOG.D, $"Set Flip  ------>  [ {value} ]");

        }

        private void checkBoxPreview_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Preview.Checked)
            {
                ucCAM.Prepate_Preview();
                bgWorkerPreview.RunWorkerAsync();
            }
            else
            {
                bgWorkerPreview.CancelAsync();
                ucCAM.Stop_Preview();
            }
            Log(LOG.D, $"CheckBox Preview [ {checkBox_Preview.Checked} ]");
        }

        private void button_capture_Click(object sender, EventArgs e)
        {
            if (checkBox_Preview.Checked)
            {
                
                var file = ucCAM.DoCapture();

                System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath + file);
            }
            else
            {
                MessageBox.Show("PREVIEW 중일 때 사용할 수 있습니다. ");
            }
        }
    }

}