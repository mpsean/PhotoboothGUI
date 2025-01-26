using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraBooth
{
    public partial class FormPreview : Form
    {
        public FormPreview(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        Form1 form1;
        private Timer tmFormLoad;
        private Timer tmWaitResult;
        private Timer tmToPrinting;

        private void FormPreview_Load(object sender, EventArgs e)
        {
            tmFormLoad = new Timer();
            tmFormLoad.Interval = 100;
            tmFormLoad.Tick += tmFormLoad_Tick;
            tmFormLoad.Start();
        }






        private void tmFormLoad_Tick(object sender, EventArgs e)
        {
            tmFormLoad.Stop();
            form1.numberBackground = 1;
            lbBackground.Text = $"Background {form1.numberBackground}";

            getImageCommand($"background,{form1.pathPhotos[0]},{form1.pathDrops[form1.countDrop]}");
        }
        private void tmWaitResult_Tick(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(form1.destinationDirectory, "printing.png")))
            {
                tmWaitResult.Stop();

                tmToPrinting = new Timer();
                tmToPrinting.Interval = 1000;
                tmToPrinting.Tick += tmToPrinting_Tick;
                tmToPrinting.Start();
            }
        }
        private void tmToPrinting_Tick(object sender, EventArgs e)
        {
            tmToPrinting.Stop();
            this.Hide();
            FormSum FormSum = new FormSum(form1);
            FormSum.ShowDialog();
            this.Close();
        }







        private void btBackgroundLeft_Click(object sender, EventArgs e)
        {
            form1.countDrop--;
            if (form1.countDrop == -1)
            {
                btBackgroundLeft.Visible = false;
                getImageCommand($"background,{form1.pathPhotos[0]},none");
                lbBackground.Text = $"Background none";
                form1.countDrop = 0;
            }
            else
            {
                getImageCommand($"background,{form1.pathPhotos[0]},{form1.pathDrops[form1.countDrop]}");
            }
            btBackgroundRight.Visible = true;
        }
        private void btBackgroundRight_Click(object sender, EventArgs e)
        {
            form1.countDrop++;
            if (form1.countDrop == form1.pathDrops.Count() - 1)
            {
                btBackgroundRight.Visible = false;
                form1.countDrop = 0;
            }
            else
            {
                getImageCommand($"background,{form1.pathPhotos[0]},{form1.pathDrops[form1.countDrop]}");
            }
            btBackgroundLeft.Visible = true;
        }
        private void btContinue_Click(object sender, EventArgs e)
        {
            form1.sendCommand($"assemble,{form1.destinationDirectory},{Path.GetFileName(form1.pathPhotos[0])}," +
                $"{Path.GetFileName(form1.pathPhotos[1])},{Path.GetFileName(form1.pathPhotos[2])}," +
                $"{form1.pathDrops[form1.countDrop]},{form1.configManager.Get("PathBackground")}");

            tmWaitResult = new Timer();
            tmWaitResult.Interval = 100;
            tmWaitResult.Tick += tmWaitResult_Tick;
            tmWaitResult.Start();
        }







        public void getImageCommand(string message)
        {
            if (form1.client != null && form1.client.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes($"*cmd={message}");
                form1.stream.Write(data, 0, data.Length);

                byte[] sizeInfo = new byte[4];
                while (true)
                {
                    int bytesRead = form1.stream.Read(sizeInfo, 0, sizeInfo.Length);
                    if (bytesRead == 0)
                        break;

                    int dataSize = BitConverter.ToInt32(sizeInfo, 0);
                    byte[] data2 = new byte[dataSize];
                    int totalBytesRead = 0;
                    while (totalBytesRead < dataSize)
                    {
                        bytesRead = form1.stream.Read(data2, totalBytesRead, dataSize - totalBytesRead);
                        totalBytesRead += bytesRead;
                    }

                    using (MemoryStream ms = new MemoryStream(data2))
                    {
                        Image image = Image.FromStream(ms);
                        pbImage.Image = image;
                        lbBackground.Text = $"Background {form1.countDrop + 1}";
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Client is not connected to server!");
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
