using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace CameraBooth
{
    public partial class FormSum : Form
    {
        public FormSum(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        Form1 form1;
        private Timer tmQrcode;





        private void FormSum_Load(object sender, EventArgs e)
        {
            using (var stream = new MemoryStream(File.ReadAllBytes(Path.Combine(form1.destinationDirectory, "printing.png"))))
            {
                Image image = Image.FromStream(stream);
                pbSum.Image = image;
            }

            tmQrcode = new Timer();
            tmQrcode.Interval = 100;
            tmQrcode.Tick += tmQrcode_Tick;
            tmQrcode.Start();
        }





        private void tmQrcode_Tick(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(form1.destinationDirectory, "qr.png")))
            {
                tmQrcode.Stop();
                btQRcode.Visible = true;
            }
        }






        private void btQRcode_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(form1.destinationDirectory, "qr.png"));
        }
        private void btBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
