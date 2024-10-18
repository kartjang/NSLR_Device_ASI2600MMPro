namespace Cam_ASI2600MMPro_Test
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            checkBox1 = new CheckBox();
            comboBox_Flip = new ComboBox();
            label2 = new Label();
            exposureTime = new NumericUpDown();
            checkBox2 = new CheckBox();
            label_exposureTime = new Label();
            label1 = new Label();
            gainControler = new NumericUpDown();
            richMsgBox = new RichTextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            treeView1 = new TreeView();
            panel1 = new Panel();
            groupBox3 = new GroupBox();
            label_tempSense = new Label();
            groupBox2 = new GroupBox();
            checkBox_Preview = new CheckBox();
            button_capture = new Button();
            label4 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            panel_image = new Panel();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)exposureTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gainControler).BeginInit();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.Appearance = Appearance.Button;
            checkBox1.BackColor = SystemColors.GradientInactiveCaption;
            checkBox1.FlatStyle = FlatStyle.Popup;
            checkBox1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox1.Location = new Point(36, 155);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(180, 48);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Cooler ON ";
            checkBox1.TextAlign = ContentAlignment.MiddleCenter;
            checkBox1.UseVisualStyleBackColor = false;
            checkBox1.CheckedChanged += Cooler_CheckedChanged;
            // 
            // comboBox_Flip
            // 
            comboBox_Flip.DropDownHeight = 300;
            comboBox_Flip.Font = new Font("맑은 고딕", 13F, FontStyle.Bold, GraphicsUnit.Point);
            comboBox_Flip.FormattingEnabled = true;
            comboBox_Flip.IntegralHeight = false;
            comboBox_Flip.Items.AddRange(new object[] { "0 NONE ", "1 Horizontal", "2 Vertical", "3 Both " });
            comboBox_Flip.Location = new Point(106, 212);
            comboBox_Flip.Name = "comboBox_Flip";
            comboBox_Flip.Size = new Size(121, 31);
            comboBox_Flip.TabIndex = 13;
            comboBox_Flip.SelectedIndexChanged += comboBox_Flip_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.InactiveCaptionText;
            label2.Location = new Point(36, 210);
            label2.Name = "label2";
            label2.Size = new Size(45, 28);
            label2.TabIndex = 10;
            label2.Text = "Flip";
            // 
            // exposureTime
            // 
            exposureTime.Font = new Font("맑은 고딕", 13F, FontStyle.Bold, GraphicsUnit.Point);
            exposureTime.Location = new Point(107, 145);
            exposureTime.Name = "exposureTime";
            exposureTime.Size = new Size(120, 31);
            exposureTime.TabIndex = 9;
            exposureTime.ValueChanged += exposureTime_ValueChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox2.Location = new Point(33, 45);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(188, 25);
            checkBox2.TabIndex = 1;
            checkBox2.Text = "CAM  OPEN / CLOSE";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // label_exposureTime
            // 
            label_exposureTime.AutoSize = true;
            label_exposureTime.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label_exposureTime.ForeColor = SystemColors.InactiveCaptionText;
            label_exposureTime.Location = new Point(35, 110);
            label_exposureTime.Name = "label_exposureTime";
            label_exposureTime.Size = new Size(187, 28);
            label_exposureTime.TabIndex = 8;
            label_exposureTime.Text = "Exposure Time(us)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.InactiveCaptionText;
            label1.Location = new Point(37, 51);
            label1.Name = "label1";
            label1.Size = new Size(61, 28);
            label1.TabIndex = 7;
            label1.Text = "Gain ";
            // 
            // gainControler
            // 
            gainControler.AllowDrop = true;
            gainControler.AutoSize = true;
            gainControler.Font = new Font("맑은 고딕", 13F, FontStyle.Bold, GraphicsUnit.Point);
            gainControler.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            gainControler.Location = new Point(129, 52);
            gainControler.Maximum = new decimal(new int[] { 700, 0, 0, 0 });
            gainControler.Name = "gainControler";
            gainControler.Size = new Size(94, 31);
            gainControler.TabIndex = 4;
            gainControler.TextAlign = HorizontalAlignment.Center;
            gainControler.ValueChanged += gainControler_ValueChanged;
            // 
            // richMsgBox
            // 
            richMsgBox.BackColor = Color.OldLace;
            richMsgBox.BorderStyle = BorderStyle.FixedSingle;
            richMsgBox.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            richMsgBox.Location = new Point(8, 5);
            richMsgBox.Margin = new Padding(5);
            richMsgBox.Name = "richMsgBox";
            richMsgBox.Size = new Size(1186, 380);
            richMsgBox.TabIndex = 3;
            richMsgBox.Text = "";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // treeView1
            // 
            treeView1.BackColor = Color.LightSteelBlue;
            treeView1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            treeView1.Indent = 30;
            treeView1.LineColor = Color.Silver;
            treeView1.Location = new Point(18, 17);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(1204, 285);
            treeView1.TabIndex = 14;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Silver;
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(groupBox3);
            panel1.Location = new Point(1238, 17);
            panel1.Name = "panel1";
            panel1.Size = new Size(299, 1312);
            panel1.TabIndex = 16;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label_tempSense);
            groupBox3.Controls.Add(checkBox2);
            groupBox3.Controls.Add(checkBox1);
            groupBox3.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.ForeColor = SystemColors.WindowFrame;
            groupBox3.Location = new Point(23, 65);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(255, 228);
            groupBox3.TabIndex = 22;
            groupBox3.TabStop = false;
            groupBox3.Text = "CAM Ctrl.";
            // 
            // label_tempSense
            // 
            label_tempSense.AutoSize = true;
            label_tempSense.Location = new Point(79, 104);
            label_tempSense.Name = "label_tempSense";
            label_tempSense.Size = new Size(0, 28);
            label_tempSense.TabIndex = 2;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkBox_Preview);
            groupBox2.Controls.Add(button_capture);
            groupBox2.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.ForeColor = SystemColors.WindowFrame;
            groupBox2.Location = new Point(23, 476);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(255, 237);
            groupBox2.TabIndex = 21;
            groupBox2.TabStop = false;
            groupBox2.Text = "Image";
            // 
            // checkBox_Preview
            // 
            checkBox_Preview.Appearance = Appearance.Button;
            checkBox_Preview.BackColor = Color.PeachPuff;
            checkBox_Preview.FlatAppearance.BorderColor = Color.FromArgb(128, 128, 255);
            checkBox_Preview.FlatAppearance.BorderSize = 3;
            checkBox_Preview.FlatAppearance.CheckedBackColor = Color.FromArgb(128, 128, 255);
            checkBox_Preview.FlatStyle = FlatStyle.Flat;
            checkBox_Preview.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox_Preview.Location = new Point(36, 49);
            checkBox_Preview.Name = "checkBox_Preview";
            checkBox_Preview.Size = new Size(180, 66);
            checkBox_Preview.TabIndex = 17;
            checkBox_Preview.Text = "PREVIEW";
            checkBox_Preview.TextAlign = ContentAlignment.MiddleCenter;
            checkBox_Preview.UseVisualStyleBackColor = false;
            checkBox_Preview.CheckedChanged += checkBoxPreview_CheckedChanged;
            // 
            // button_capture
            // 
            button_capture.BackColor = Color.BlanchedAlmond;
            button_capture.FlatAppearance.BorderColor = Color.BlueViolet;
            button_capture.FlatAppearance.BorderSize = 3;
            button_capture.FlatStyle = FlatStyle.Popup;
            button_capture.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button_capture.Location = new Point(36, 138);
            button_capture.Name = "button_capture";
            button_capture.Size = new Size(181, 64);
            button_capture.TabIndex = 18;
            button_capture.Text = "CAPTURE";
            button_capture.UseVisualStyleBackColor = false;
            button_capture.Click += button_capture_Click;
            // 
            // label4
            // 
            label4.BackColor = Color.BlanchedAlmond;
            label4.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.MidnightBlue;
            label4.Location = new Point(1, 1272);
            label4.Name = "label4";
            label4.Size = new Size(296, 37);
            label4.TabIndex = 19;
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.BlanchedAlmond;
            label3.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.MidnightBlue;
            label3.Location = new Point(2, 2);
            label3.Name = "label3";
            label3.Size = new Size(296, 45);
            label3.TabIndex = 16;
            label3.Text = "Camera Control";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label_exposureTime);
            groupBox1.Controls.Add(comboBox_Flip);
            groupBox1.Controls.Add(gainControler);
            groupBox1.Controls.Add(exposureTime);
            groupBox1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = SystemColors.WindowFrame;
            groupBox1.Location = new Point(23, 898);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(255, 338);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            groupBox1.Text = "Control";
            // 
            // panel_image
            // 
            panel_image.BackColor = SystemColors.GradientActiveCaption;
            panel_image.Location = new Point(17, 318);
            panel_image.Name = "panel_image";
            panel_image.Size = new Size(1204, 600);
            panel_image.TabIndex = 17;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(richMsgBox);
            panel2.Location = new Point(16, 935);
            panel2.Name = "panel2";
            panel2.Size = new Size(1206, 394);
            panel2.TabIndex = 18;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(192, 192, 255);
            ClientSize = new Size(1553, 1344);
            Controls.Add(panel2);
            Controls.Add(panel_image);
            Controls.Add(treeView1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "CAM_Tester";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)exposureTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)gainControler).EndInit();
            panel1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private RichTextBox richMsgBox;
        private Label label1;
        private NumericUpDown gainControler;
        private Label label_exposureTime;
        private NumericUpDown exposureTime;
        private Label label2;
        private ContextMenuStrip contextMenuStrip1;
        private ComboBox comboBox_Flip;
        private TreeView treeView1;
        private Panel panel1;
        private Label label3;
        private ucCamera caM_Control1;
        private Panel panel_image;
        private Button button_capture;
        private CheckBox checkBox_Preview;
        private Label label4;
        private Panel panel2;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Label label_tempSense;
    }
}