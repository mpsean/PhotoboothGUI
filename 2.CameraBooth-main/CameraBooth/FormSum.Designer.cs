namespace CameraBooth
{
    partial class FormSum
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
            this.btBack = new System.Windows.Forms.Panel();
            this.btQRcode = new System.Windows.Forms.Panel();
            this.pbSum = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSum)).BeginInit();
            this.SuspendLayout();
            // 
            // btBack
            // 
            this.btBack.BackgroundImage = global::CameraBooth.Properties.Resources.reply;
            this.btBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btBack.Location = new System.Drawing.Point(12, 12);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(61, 58);
            this.btBack.TabIndex = 2;
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // btQRcode
            // 
            this.btQRcode.BackgroundImage = global::CameraBooth.Properties.Resources.qr_code;
            this.btQRcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btQRcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btQRcode.Location = new System.Drawing.Point(286, 12);
            this.btQRcode.Name = "btQRcode";
            this.btQRcode.Size = new System.Drawing.Size(61, 58);
            this.btQRcode.TabIndex = 1;
            this.btQRcode.Visible = false;
            this.btQRcode.Click += new System.EventHandler(this.btQRcode_Click);
            // 
            // pbSum
            // 
            this.pbSum.Location = new System.Drawing.Point(12, 76);
            this.pbSum.Name = "pbSum";
            this.pbSum.Size = new System.Drawing.Size(365, 518);
            this.pbSum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSum.TabIndex = 0;
            this.pbSum.TabStop = false;
            // 
            // FormSum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 606);
            this.Controls.Add(this.btBack);
            this.Controls.Add(this.btQRcode);
            this.Controls.Add(this.pbSum);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormSum";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPrinting";
            this.Load += new System.EventHandler(this.FormSum_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSum;
        private System.Windows.Forms.Panel btQRcode;
        private System.Windows.Forms.Panel btBack;
    }
}