namespace NAudioTest {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.playButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.openButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.TrackBar();
            this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
            this.volumeMeter1 = new NAudio.Gui.VolumeMeter();
            this.volumeMeter2 = new NAudio.Gui.VolumeMeter();
            this.waveformPainter1 = new NAudio.Gui.WaveformPainter();
            this.waveformPainter2 = new NAudio.Gui.WaveformPainter();
            this.fft = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar)).BeginInit();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.Location = new System.Drawing.Point(12, 42);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(96, 23);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.play);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(12, 71);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(96, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stop);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "0:0:0";
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(12, 13);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(96, 23);
            this.openButton.TabIndex = 8;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.open);
            // 
            // progressBar
            // 
            this.progressBar.LargeChange = 1000000;
            this.progressBar.Location = new System.Drawing.Point(12, 175);
            this.progressBar.Maximum = 100;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(501, 45);
            this.progressBar.SmallChange = 1000000;
            this.progressBar.TabIndex = 9;
            this.progressBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.progressBar.Scroll += new System.EventHandler(this.scrub);
            // 
            // volumeSlider1
            // 
            this.volumeSlider1.Location = new System.Drawing.Point(12, 100);
            this.volumeSlider1.Name = "volumeSlider1";
            this.volumeSlider1.Size = new System.Drawing.Size(96, 16);
            this.volumeSlider1.TabIndex = 11;
            this.volumeSlider1.VolumeChanged += new System.EventHandler(this.volumeSlider1_VolumeChanged);
            // 
            // volumeMeter1
            // 
            this.volumeMeter1.Amplitude = 0F;
            this.volumeMeter1.Location = new System.Drawing.Point(138, 13);
            this.volumeMeter1.MaxDb = 0F;
            this.volumeMeter1.MinDb = -30F;
            this.volumeMeter1.Name = "volumeMeter1";
            this.volumeMeter1.Size = new System.Drawing.Size(17, 104);
            this.volumeMeter1.TabIndex = 12;
            this.volumeMeter1.Text = "volumeMeter1";
            // 
            // volumeMeter2
            // 
            this.volumeMeter2.Amplitude = 0F;
            this.volumeMeter2.Location = new System.Drawing.Point(161, 13);
            this.volumeMeter2.MaxDb = 0F;
            this.volumeMeter2.MinDb = -30F;
            this.volumeMeter2.Name = "volumeMeter2";
            this.volumeMeter2.Size = new System.Drawing.Size(17, 104);
            this.volumeMeter2.TabIndex = 14;
            this.volumeMeter2.Text = "volumeMeter2";
            // 
            // waveformPainter1
            // 
            this.waveformPainter1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.waveformPainter1.Location = new System.Drawing.Point(184, 13);
            this.waveformPainter1.Name = "waveformPainter1";
            this.waveformPainter1.Size = new System.Drawing.Size(329, 45);
            this.waveformPainter1.TabIndex = 15;
            this.waveformPainter1.Text = "waveformPainter1";
            // 
            // waveformPainter2
            // 
            this.waveformPainter2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.waveformPainter2.Location = new System.Drawing.Point(184, 72);
            this.waveformPainter2.Name = "waveformPainter2";
            this.waveformPainter2.Size = new System.Drawing.Size(329, 45);
            this.waveformPainter2.TabIndex = 16;
            this.waveformPainter2.Text = "waveformPainter2";
            // 
            // fft
            // 
            this.fft.Location = new System.Drawing.Point(15, 199);
            this.fft.Name = "fft";
            this.fft.Size = new System.Drawing.Size(498, 108);
            this.fft.TabIndex = 17;
            this.fft.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 319);
            this.Controls.Add(this.fft);
            this.Controls.Add(this.waveformPainter2);
            this.Controls.Add(this.waveformPainter1);
            this.Controls.Add(this.volumeMeter2);
            this.Controls.Add(this.volumeMeter1);
            this.Controls.Add(this.volumeSlider1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.playButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.progressBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.TrackBar progressBar;
        private NAudio.Gui.VolumeSlider volumeSlider1;
        private NAudio.Gui.VolumeMeter volumeMeter1;
        private NAudio.Gui.VolumeMeter volumeMeter2;
        private NAudio.Gui.WaveformPainter waveformPainter1;
        private NAudio.Gui.WaveformPainter waveformPainter2;
        private System.Windows.Forms.RichTextBox fft;
    }
}

