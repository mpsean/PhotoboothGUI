namespace CameraBooth
{
    partial class Form1
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
            this.btStart = new System.Windows.Forms.Button();
            this.lbStartCapture = new System.Windows.Forms.Label();
            this.btDelete = new System.Windows.Forms.Button();
            this.btContinue = new System.Windows.Forms.Button();
            this.lbScore = new System.Windows.Forms.Label();
            this.btConfig = new System.Windows.Forms.Panel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lbOpenCamera = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btStart.ForeColor = System.Drawing.Color.White;
            this.btStart.Location = new System.Drawing.Point(503, 12);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(171, 75);
            this.btStart.TabIndex = 3;
            this.btStart.Text = "Start Capture";
            this.btStart.UseVisualStyleBackColor = false;
            this.btStart.Visible = false;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // lbStartCapture
            // 
            this.lbStartCapture.AutoSize = true;
            this.lbStartCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbStartCapture.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbStartCapture.Location = new System.Drawing.Point(532, 0);
            this.lbStartCapture.Name = "lbStartCapture";
            this.lbStartCapture.Size = new System.Drawing.Size(99, 108);
            this.lbStartCapture.TabIndex = 6;
            this.lbStartCapture.Text = "3";
            this.lbStartCapture.Visible = false;
            this.lbStartCapture.Click += new System.EventHandler(this.lbStartCapture_Click);
            // 
            // btDelete
            // 
            this.btDelete.BackColor = System.Drawing.Color.Red;
            this.btDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btDelete.ForeColor = System.Drawing.Color.White;
            this.btDelete.Location = new System.Drawing.Point(901, 12);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(171, 75);
            this.btDelete.TabIndex = 7;
            this.btDelete.Text = "Delete";
            this.btDelete.UseVisualStyleBackColor = false;
            this.btDelete.Visible = false;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btContinue
            // 
            this.btContinue.BackColor = System.Drawing.Color.Lime;
            this.btContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btContinue.ForeColor = System.Drawing.Color.White;
            this.btContinue.Location = new System.Drawing.Point(503, 12);
            this.btContinue.Name = "btContinue";
            this.btContinue.Size = new System.Drawing.Size(171, 75);
            this.btContinue.TabIndex = 11;
            this.btContinue.Text = "Continue";
            this.btContinue.UseVisualStyleBackColor = false;
            this.btContinue.Visible = false;
            this.btContinue.Click += new System.EventHandler(this.btContinue_Click);
            // 
            // lbScore
            // 
            this.lbScore.AutoSize = true;
            this.lbScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbScore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbScore.Location = new System.Drawing.Point(3, 88);
            this.lbScore.Name = "lbScore";
            this.lbScore.Size = new System.Drawing.Size(181, 108);
            this.lbScore.TabIndex = 12;
            this.lbScore.Text = "0/3";
            this.lbScore.Visible = false;
            // 
            // btConfig
            // 
            this.btConfig.BackColor = System.Drawing.Color.White;
            this.btConfig.BackgroundImage = global::CameraBooth.Properties.Resources.config;
            this.btConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btConfig.ForeColor = System.Drawing.Color.White;
            this.btConfig.Location = new System.Drawing.Point(12, 12);
            this.btConfig.Name = "btConfig";
            this.btConfig.Size = new System.Drawing.Size(51, 55);
            this.btConfig.TabIndex = 13;
            this.btConfig.Click += new System.EventHandler(this.btConfig_Click);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(190, 111);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(836, 584);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // lbOpenCamera
            // 
            this.lbOpenCamera.AutoSize = true;
            this.lbOpenCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbOpenCamera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbOpenCamera.Location = new System.Drawing.Point(453, 303);
            this.lbOpenCamera.Name = "lbOpenCamera";
            this.lbOpenCamera.Size = new System.Drawing.Size(247, 110);
            this.lbOpenCamera.TabIndex = 16;
            this.lbOpenCamera.Text = "opening\r\ncamera ...";
            this.lbOpenCamera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 705);
            this.Controls.Add(this.lbStartCapture);
            this.Controls.Add(this.lbOpenCamera);
            this.Controls.Add(this.btConfig);
            this.Controls.Add(this.lbScore);
            this.Controls.Add(this.btContinue);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.pbImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Label lbStartCapture;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btContinue;
        private System.Windows.Forms.Label lbScore;
        private System.Windows.Forms.Panel btConfig;
        private System.Windows.Forms.Label lbOpenCamera;
    }
}

