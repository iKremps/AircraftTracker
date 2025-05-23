
using System;
using System.IO;

namespace AirplaneTracker
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.showCallsignsBox = new System.Windows.Forms.CheckBox();
            this.showPrivateBox = new System.Windows.Forms.CheckBox();
            this.newYorkRadioButton = new System.Windows.Forms.RadioButton();
            this.homeRadioButton = new System.Windows.Forms.RadioButton();
            this.RangeDropDown = new System.Windows.Forms.ComboBox();
            this.rangeLabel = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridPlanes = new System.Windows.Forms.DataGridView();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.homeCloseRadioButton = new System.Windows.Forms.RadioButton();
            this.orlandoRadioButton = new System.Windows.Forms.RadioButton();
            this.lavalletteRadioButton = new System.Windows.Forms.RadioButton();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCallsign = new System.Windows.Forms.TextBox();
            this.showOriginAndDestinationBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPlanes)).BeginInit();
            this.SuspendLayout();
            // 
            // showCallsignsBox
            // 
            this.showCallsignsBox.AutoSize = true;
            this.showCallsignsBox.Checked = true;
            this.showCallsignsBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showCallsignsBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.showCallsignsBox.Location = new System.Drawing.Point(926, 929);
            this.showCallsignsBox.Name = "showCallsignsBox";
            this.showCallsignsBox.Size = new System.Drawing.Size(134, 25);
            this.showCallsignsBox.TabIndex = 0;
            this.showCallsignsBox.Text = "Show Callsigns";
            this.showCallsignsBox.UseVisualStyleBackColor = true;
            this.showCallsignsBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // showPrivateBox
            // 
            this.showPrivateBox.AutoSize = true;
            this.showPrivateBox.Checked = true;
            this.showPrivateBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showPrivateBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.showPrivateBox.Location = new System.Drawing.Point(926, 952);
            this.showPrivateBox.Name = "showPrivateBox";
            this.showPrivateBox.Size = new System.Drawing.Size(120, 25);
            this.showPrivateBox.TabIndex = 1;
            this.showPrivateBox.Text = "Show Private";
            this.showPrivateBox.UseVisualStyleBackColor = true;
            this.showPrivateBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // newYorkRadioButton
            // 
            this.newYorkRadioButton.AutoSize = true;
            this.newYorkRadioButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.newYorkRadioButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.newYorkRadioButton.Location = new System.Drawing.Point(16, 805);
            this.newYorkRadioButton.Name = "newYorkRadioButton";
            this.newYorkRadioButton.Size = new System.Drawing.Size(95, 25);
            this.newYorkRadioButton.TabIndex = 2;
            this.newYorkRadioButton.Text = "New York";
            this.newYorkRadioButton.UseVisualStyleBackColor = false;
            this.newYorkRadioButton.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // homeRadioButton
            // 
            this.homeRadioButton.AutoSize = true;
            this.homeRadioButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.homeRadioButton.Location = new System.Drawing.Point(17, 711);
            this.homeRadioButton.Name = "homeRadioButton";
            this.homeRadioButton.Size = new System.Drawing.Size(70, 25);
            this.homeRadioButton.TabIndex = 3;
            this.homeRadioButton.Text = "Home";
            this.homeRadioButton.UseVisualStyleBackColor = true;
            this.homeRadioButton.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // RangeDropDown
            // 
            this.RangeDropDown.BackColor = System.Drawing.SystemColors.Window;
            this.RangeDropDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RangeDropDown.FormattingEnabled = true;
            this.RangeDropDown.Items.AddRange(new object[] {
            "5",
            "10",
            "20",
            "30",
            "50"});
            this.RangeDropDown.Location = new System.Drawing.Point(80, 682);
            this.RangeDropDown.Name = "RangeDropDown";
            this.RangeDropDown.Size = new System.Drawing.Size(54, 23);
            this.RangeDropDown.TabIndex = 4;
            this.RangeDropDown.Tag = "";
            this.RangeDropDown.SelectedIndexChanged += new System.EventHandler(this.RangeDropDown_SelectedIndexChanged);
            // 
            // rangeLabel
            // 
            this.rangeLabel.AutoSize = true;
            this.rangeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeLabel.Location = new System.Drawing.Point(17, 682);
            this.rangeLabel.Name = "rangeLabel";
            this.rangeLabel.Size = new System.Drawing.Size(57, 21);
            this.rangeLabel.TabIndex = 5;
            this.rangeLabel.Text = "Range:";
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exitButton.Location = new System.Drawing.Point(1125, 994);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(104, 41);
            this.exitButton.TabIndex = 8;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(49, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1141, 652);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(61, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "label2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 1021);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "label4";
            // 
            // dataGridPlanes
            // 
            this.dataGridPlanes.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridPlanes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPlanes.Location = new System.Drawing.Point(166, 682);
            this.dataGridPlanes.Name = "dataGridPlanes";
            this.dataGridPlanes.RowTemplate.Height = 25;
            this.dataGridPlanes.Size = new System.Drawing.Size(735, 331);
            this.dataGridPlanes.TabIndex = 14;
            this.dataGridPlanes.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 1000;
            // 
            // homeCloseRadioButton
            // 
            this.homeCloseRadioButton.AutoSize = true;
            this.homeCloseRadioButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.homeCloseRadioButton.Location = new System.Drawing.Point(17, 743);
            this.homeCloseRadioButton.Name = "homeCloseRadioButton";
            this.homeCloseRadioButton.Size = new System.Drawing.Size(122, 25);
            this.homeCloseRadioButton.TabIndex = 15;
            this.homeCloseRadioButton.TabStop = true;
            this.homeCloseRadioButton.Text = "Home (Close)";
            this.homeCloseRadioButton.UseVisualStyleBackColor = true;
            this.homeCloseRadioButton.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // orlandoRadioButton
            // 
            this.orlandoRadioButton.AutoSize = true;
            this.orlandoRadioButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.orlandoRadioButton.Location = new System.Drawing.Point(16, 836);
            this.orlandoRadioButton.Name = "orlandoRadioButton";
            this.orlandoRadioButton.Size = new System.Drawing.Size(85, 25);
            this.orlandoRadioButton.TabIndex = 16;
            this.orlandoRadioButton.Text = "Orlando";
            this.orlandoRadioButton.UseVisualStyleBackColor = true;
            this.orlandoRadioButton.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // lavalletteRadioButton
            // 
            this.lavalletteRadioButton.AutoSize = true;
            this.lavalletteRadioButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lavalletteRadioButton.Location = new System.Drawing.Point(17, 774);
            this.lavalletteRadioButton.Name = "lavalletteRadioButton";
            this.lavalletteRadioButton.Size = new System.Drawing.Size(94, 25);
            this.lavalletteRadioButton.TabIndex = 17;
            this.lavalletteRadioButton.Text = "Lavallette";
            this.lavalletteRadioButton.UseVisualStyleBackColor = true;
            this.lavalletteRadioButton.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(926, 766);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(135, 23);
            this.txtCompany.TabIndex = 18;
            this.txtCompany.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(926, 742);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Company Filter:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(926, 682);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Callsign Filter:";
            // 
            // txtCallsign
            // 
            this.txtCallsign.Location = new System.Drawing.Point(926, 706);
            this.txtCallsign.Name = "txtCallsign";
            this.txtCallsign.Size = new System.Drawing.Size(135, 23);
            this.txtCallsign.TabIndex = 20;
            this.txtCallsign.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // showOriginAndDestinationBox
            // 
            this.showOriginAndDestinationBox.AutoSize = true;
            this.showOriginAndDestinationBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.showOriginAndDestinationBox.Location = new System.Drawing.Point(926, 974);
            this.showOriginAndDestinationBox.Name = "showOriginAndDestinationBox";
            this.showOriginAndDestinationBox.Size = new System.Drawing.Size(158, 25);
            this.showOriginAndDestinationBox.TabIndex = 22;
            this.showOriginAndDestinationBox.Text = "Origin/Destination";
            this.showOriginAndDestinationBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1241, 1047);
            this.Controls.Add(this.showOriginAndDestinationBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCallsign);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.lavalletteRadioButton);
            this.Controls.Add(this.orlandoRadioButton);
            this.Controls.Add(this.homeCloseRadioButton);
            this.Controls.Add(this.dataGridPlanes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.rangeLabel);
            this.Controls.Add(this.RangeDropDown);
            this.Controls.Add(this.homeRadioButton);
            this.Controls.Add(this.newYorkRadioButton);
            this.Controls.Add(this.showPrivateBox);
            this.Controls.Add(this.showCallsignsBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Airplane Tracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPlanes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        //Fetching & Initializing Graphics related variables that are not dependant on the UI being visible
        private void InitializeAppGraphics()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            string imagesPath = Path.Combine(baseDir, "Images\\");
            string[] paths = Directory.GetFiles(imagesPath, "*.jpg");

            foreach (string path in paths)
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(path);

                AppGraphics appG = new AppGraphics(
                    imagesPath,
                    fileNameWithoutExt,
                    fileNameWithoutExt + ".jpg",
                    fileNameWithoutExt + ".kml"
                );

                ag.Add(appG);
            }
        }

        #endregion

        private System.Windows.Forms.CheckBox showCallsignsBox;
        private System.Windows.Forms.CheckBox showPrivateBox;
        private System.Windows.Forms.RadioButton newYorkRadioButton;
        private System.Windows.Forms.RadioButton io;
        private System.Windows.Forms.ComboBox RangeDropDown;
        private System.Windows.Forms.Label rangeLabel;
        private System.Windows.Forms.RadioButton homeRadioButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridPlanes;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.RadioButton homeCloseRadioButton;
        private System.Windows.Forms.RadioButton orlandoRadioButton;
        private System.Windows.Forms.RadioButton lavalletteRadioButton;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCallsign;
        private System.Windows.Forms.CheckBox showOriginAndDestinationBox;
    }
}

