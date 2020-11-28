namespace DVRailDriver
{
    partial class frmCalibrate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbReverser = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.tbThrottle = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAirBrake = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.tbIndBrake = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tbWiper = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.tbLights = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRevPosition = new System.Windows.Forms.Label();
            this.tbBailOff = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSetRevCenter = new System.Windows.Forms.Button();
            this.btnCalInd = new System.Windows.Forms.Button();
            this.btnCalBailoff = new System.Windows.Forms.Button();
            this.btnCalWiper = new System.Windows.Forms.Button();
            this.btnCalLights = new System.Windows.Forms.Button();
            this.lblIndPos = new System.Windows.Forms.Label();
            this.lblBailoffStatus = new System.Windows.Forms.Label();
            this.lblWiperPos = new System.Windows.Forms.Label();
            this.lblLightsPos = new System.Windows.Forms.Label();
            this.btnCalAutoBrake = new System.Windows.Forms.Button();
            this.lblAutoBrakePos = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCalThrottle = new System.Windows.Forms.Button();
            this.lblThrottlePos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbReverser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbThrottle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAirBrake)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIndBrake)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbWiper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBailOff)).BeginInit();
            this.SuspendLayout();
            // 
            // tbReverser
            // 
            this.tbReverser.Enabled = false;
            this.tbReverser.Location = new System.Drawing.Point(86, 37);
            this.tbReverser.Maximum = 100;
            this.tbReverser.Name = "tbReverser";
            this.tbReverser.Size = new System.Drawing.Size(353, 48);
            this.tbReverser.TabIndex = 0;
            this.tbReverser.TickFrequency = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Reverser";
            // 
            // tbThrottle
            // 
            this.tbThrottle.Enabled = false;
            this.tbThrottle.Location = new System.Drawing.Point(86, 91);
            this.tbThrottle.Maximum = 100;
            this.tbThrottle.Name = "tbThrottle";
            this.tbThrottle.Size = new System.Drawing.Size(353, 48);
            this.tbThrottle.TabIndex = 0;
            this.tbThrottle.TickFrequency = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Throttle";
            // 
            // tbAirBrake
            // 
            this.tbAirBrake.Enabled = false;
            this.tbAirBrake.Location = new System.Drawing.Point(86, 145);
            this.tbAirBrake.Maximum = 100;
            this.tbAirBrake.Name = "tbAirBrake";
            this.tbAirBrake.Size = new System.Drawing.Size(353, 48);
            this.tbAirBrake.TabIndex = 0;
            this.tbAirBrake.TickFrequency = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Auto Brake";
            // 
            // tbIndBrake
            // 
            this.tbIndBrake.Enabled = false;
            this.tbIndBrake.Location = new System.Drawing.Point(86, 199);
            this.tbIndBrake.Maximum = 100;
            this.tbIndBrake.Name = "tbIndBrake";
            this.tbIndBrake.Size = new System.Drawing.Size(353, 48);
            this.tbIndBrake.TabIndex = 0;
            this.tbIndBrake.TickFrequency = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ind Brake";
            // 
            // tbWiper
            // 
            this.tbWiper.Enabled = false;
            this.tbWiper.Location = new System.Drawing.Point(86, 307);
            this.tbWiper.Maximum = 100;
            this.tbWiper.Name = "tbWiper";
            this.tbWiper.Size = new System.Drawing.Size(353, 48);
            this.tbWiper.TabIndex = 0;
            this.tbWiper.TickFrequency = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 316);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Wiper";
            // 
            // tbLights
            // 
            this.tbLights.Enabled = false;
            this.tbLights.Location = new System.Drawing.Point(86, 361);
            this.tbLights.Maximum = 100;
            this.tbLights.Name = "tbLights";
            this.tbLights.Size = new System.Drawing.Size(353, 48);
            this.tbLights.TabIndex = 0;
            this.tbLights.TickFrequency = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 370);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Lights";
            // 
            // lblRevPosition
            // 
            this.lblRevPosition.AutoSize = true;
            this.lblRevPosition.Location = new System.Drawing.Point(445, 46);
            this.lblRevPosition.Name = "lblRevPosition";
            this.lblRevPosition.Size = new System.Drawing.Size(41, 13);
            this.lblRevPosition.TabIndex = 2;
            this.lblRevPosition.Text = "Neutral";
            // 
            // tbBailOff
            // 
            this.tbBailOff.Enabled = false;
            this.tbBailOff.Location = new System.Drawing.Point(86, 253);
            this.tbBailOff.Maximum = 100;
            this.tbBailOff.Name = "tbBailOff";
            this.tbBailOff.Size = new System.Drawing.Size(353, 48);
            this.tbBailOff.TabIndex = 0;
            this.tbBailOff.TickFrequency = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "BailOff";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(83, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(394, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "To calibrate. click on \"Calibrate...\" next to each control and follow the instruc" +
    "tions.";
            // 
            // btnSetRevCenter
            // 
            this.btnSetRevCenter.Location = new System.Drawing.Point(521, 41);
            this.btnSetRevCenter.Name = "btnSetRevCenter";
            this.btnSetRevCenter.Size = new System.Drawing.Size(109, 23);
            this.btnSetRevCenter.TabIndex = 5;
            this.btnSetRevCenter.Text = "Calibrate...";
            this.btnSetRevCenter.UseVisualStyleBackColor = true;
            this.btnSetRevCenter.Click += new System.EventHandler(this.btnSetRevCenter_Click);
            // 
            // btnCalInd
            // 
            this.btnCalInd.Location = new System.Drawing.Point(521, 203);
            this.btnCalInd.Name = "btnCalInd";
            this.btnCalInd.Size = new System.Drawing.Size(109, 23);
            this.btnCalInd.TabIndex = 6;
            this.btnCalInd.Text = "Calibrate...";
            this.btnCalInd.UseVisualStyleBackColor = true;
            this.btnCalInd.Click += new System.EventHandler(this.autoCal_Click);
            // 
            // btnCalBailoff
            // 
            this.btnCalBailoff.Location = new System.Drawing.Point(521, 257);
            this.btnCalBailoff.Name = "btnCalBailoff";
            this.btnCalBailoff.Size = new System.Drawing.Size(109, 23);
            this.btnCalBailoff.TabIndex = 6;
            this.btnCalBailoff.Text = "Calibrate...";
            this.btnCalBailoff.UseVisualStyleBackColor = true;
            this.btnCalBailoff.Click += new System.EventHandler(this.btnCalBailoff_Click);
            // 
            // btnCalWiper
            // 
            this.btnCalWiper.Location = new System.Drawing.Point(521, 311);
            this.btnCalWiper.Name = "btnCalWiper";
            this.btnCalWiper.Size = new System.Drawing.Size(109, 23);
            this.btnCalWiper.TabIndex = 6;
            this.btnCalWiper.Text = "Calibrate...";
            this.btnCalWiper.UseVisualStyleBackColor = true;
            this.btnCalWiper.Click += new System.EventHandler(this.autoCal_Click);
            // 
            // btnCalLights
            // 
            this.btnCalLights.Location = new System.Drawing.Point(521, 365);
            this.btnCalLights.Name = "btnCalLights";
            this.btnCalLights.Size = new System.Drawing.Size(109, 23);
            this.btnCalLights.TabIndex = 6;
            this.btnCalLights.Text = "Calibrate...";
            this.btnCalLights.UseVisualStyleBackColor = true;
            this.btnCalLights.Click += new System.EventHandler(this.autoCal_Click);
            // 
            // lblIndPos
            // 
            this.lblIndPos.AutoSize = true;
            this.lblIndPos.Location = new System.Drawing.Point(445, 208);
            this.lblIndPos.Name = "lblIndPos";
            this.lblIndPos.Size = new System.Drawing.Size(21, 13);
            this.lblIndPos.TabIndex = 7;
            this.lblIndPos.Text = "0%";
            // 
            // lblBailoffStatus
            // 
            this.lblBailoffStatus.AutoSize = true;
            this.lblBailoffStatus.Location = new System.Drawing.Point(445, 262);
            this.lblBailoffStatus.Name = "lblBailoffStatus";
            this.lblBailoffStatus.Size = new System.Drawing.Size(21, 13);
            this.lblBailoffStatus.TabIndex = 7;
            this.lblBailoffStatus.Text = "No";
            // 
            // lblWiperPos
            // 
            this.lblWiperPos.AutoSize = true;
            this.lblWiperPos.Location = new System.Drawing.Point(445, 316);
            this.lblWiperPos.Name = "lblWiperPos";
            this.lblWiperPos.Size = new System.Drawing.Size(21, 13);
            this.lblWiperPos.TabIndex = 7;
            this.lblWiperPos.Text = "Off";
            // 
            // lblLightsPos
            // 
            this.lblLightsPos.AutoSize = true;
            this.lblLightsPos.Location = new System.Drawing.Point(445, 370);
            this.lblLightsPos.Name = "lblLightsPos";
            this.lblLightsPos.Size = new System.Drawing.Size(21, 13);
            this.lblLightsPos.TabIndex = 7;
            this.lblLightsPos.Text = "Off";
            // 
            // btnCalAutoBrake
            // 
            this.btnCalAutoBrake.Location = new System.Drawing.Point(521, 149);
            this.btnCalAutoBrake.Name = "btnCalAutoBrake";
            this.btnCalAutoBrake.Size = new System.Drawing.Size(109, 23);
            this.btnCalAutoBrake.TabIndex = 5;
            this.btnCalAutoBrake.Text = "Calibrate...";
            this.btnCalAutoBrake.UseVisualStyleBackColor = true;
            this.btnCalAutoBrake.Click += new System.EventHandler(this.btnCalibrateAutoBrake_Click);
            // 
            // lblAutoBrakePos
            // 
            this.lblAutoBrakePos.AutoSize = true;
            this.lblAutoBrakePos.Location = new System.Drawing.Point(445, 154);
            this.lblAutoBrakePos.Name = "lblAutoBrakePos";
            this.lblAutoBrakePos.Size = new System.Drawing.Size(21, 13);
            this.lblAutoBrakePos.TabIndex = 7;
            this.lblAutoBrakePos.Text = "0%";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(555, 432);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(474, 432);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCalThrottle
            // 
            this.btnCalThrottle.Location = new System.Drawing.Point(521, 95);
            this.btnCalThrottle.Name = "btnCalThrottle";
            this.btnCalThrottle.Size = new System.Drawing.Size(109, 23);
            this.btnCalThrottle.TabIndex = 5;
            this.btnCalThrottle.Text = "Calibrate...";
            this.btnCalThrottle.UseVisualStyleBackColor = true;
            this.btnCalThrottle.Click += new System.EventHandler(this.btnCalThrottle_Click);
            // 
            // lblThrottlePos
            // 
            this.lblThrottlePos.AutoSize = true;
            this.lblThrottlePos.Location = new System.Drawing.Point(445, 100);
            this.lblThrottlePos.Name = "lblThrottlePos";
            this.lblThrottlePos.Size = new System.Drawing.Size(21, 13);
            this.lblThrottlePos.TabIndex = 7;
            this.lblThrottlePos.Text = "0%";
            // 
            // frmCalibrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 467);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblLightsPos);
            this.Controls.Add(this.lblWiperPos);
            this.Controls.Add(this.lblBailoffStatus);
            this.Controls.Add(this.lblThrottlePos);
            this.Controls.Add(this.lblAutoBrakePos);
            this.Controls.Add(this.lblIndPos);
            this.Controls.Add(this.btnCalLights);
            this.Controls.Add(this.btnCalWiper);
            this.Controls.Add(this.btnCalBailoff);
            this.Controls.Add(this.btnCalInd);
            this.Controls.Add(this.btnCalThrottle);
            this.Controls.Add(this.btnCalAutoBrake);
            this.Controls.Add(this.btnSetRevCenter);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblRevPosition);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbLights);
            this.Controls.Add(this.tbWiper);
            this.Controls.Add(this.tbBailOff);
            this.Controls.Add(this.tbIndBrake);
            this.Controls.Add(this.tbAirBrake);
            this.Controls.Add(this.tbThrottle);
            this.Controls.Add(this.tbReverser);
            this.Name = "frmCalibrate";
            this.Text = "DV RailDriver Adapter";
            ((System.ComponentModel.ISupportInitialize)(this.tbReverser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbThrottle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAirBrake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIndBrake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbWiper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBailOff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbReverser;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar tbThrottle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbAirBrake;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbIndBrake;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbWiper;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbLights;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRevPosition;
        private System.Windows.Forms.TrackBar tbBailOff;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSetRevCenter;
        private System.Windows.Forms.Button btnCalInd;
        private System.Windows.Forms.Button btnCalBailoff;
        private System.Windows.Forms.Button btnCalWiper;
        private System.Windows.Forms.Button btnCalLights;
        private System.Windows.Forms.Label lblIndPos;
        private System.Windows.Forms.Label lblBailoffStatus;
        private System.Windows.Forms.Label lblWiperPos;
        private System.Windows.Forms.Label lblLightsPos;
        private System.Windows.Forms.Button btnCalAutoBrake;
        private System.Windows.Forms.Label lblAutoBrakePos;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCalThrottle;
        private System.Windows.Forms.Label lblThrottlePos;
    }
}

