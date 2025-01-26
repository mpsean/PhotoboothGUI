using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CameraBooth
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string pythonFilePath = Path.GetFullPath(@"../../pythonCamera/main.py");
            if (File.Exists(pythonFilePath))
            {
                string pythonExePath = Path.GetFullPath(@"../../pythonCamera/.venv/Scripts/python.exe");

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = pythonExePath;
                startInfo.Arguments = "\"" + pythonFilePath + "\"";
                startInfo.WorkingDirectory = Path.GetDirectoryName(pythonFilePath);
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                Process process = Process.Start(startInfo);
            }
            else
            {
                Console.WriteLine("Python script not found.");
            }

            try
            {
                File.ReadAllBytes(configManager.Get("PathBackground"));
            }
            catch
            {
                string openFileDialog = Path.GetFullPath(@"../../resource/background/bg.png");
                configManager.Set("PathBackground", openFileDialog);
                configManager.SaveConfig();
            }
            try
            {
                File.ReadAllBytes(configManager.Get("PathEffect"));
            }
            catch
            {
                string openFileDialog = Path.GetFullPath(@"../../resource/effect/overlay.png");
                configManager.Set("PathEffect", openFileDialog);
                configManager.SaveConfig();
            }
        }

        public TcpClient client;
        public NetworkStream stream;
        private System.Windows.Forms.Timer tmSteam;
        private System.Windows.Forms.Timer tmFormLoad;
        private System.Windows.Forms.Timer tmCountdown;
        private System.Windows.Forms.Timer tmWaitImagePython;
        public ConfigManager configManager = new ConfigManager("../../config.config");
        private int countdownValue;
        private string serverAddress;
        private int port;
        private int score = 0;//clear
        public string destinationDirectory;//clear
        private string sourceFilePath = @"../../pythonCamera/captured_image.jpg";
        private string destinationFilePath;
        public bool flagNewFolder = true;//clear
        public int numberBackground = 1;
        public List<string> pathPhotos = new List<string>();//clear
        public List<string> pathDrops = new List<string>();
        public int countDrop = 0;
        private bool flagBtStart = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            serverAddress = configManager.Get("Server");
            port = Convert.ToInt32(configManager.Get("Port"));
            destinationDirectory = configManager.Get("PathOutputImage");

            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            pathDrops = Directory.GetFiles("../../resource/drop")
                .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower()))
                .Select(file => Path.GetFullPath(file))
                .OrderBy(fileName => fileName)
                .ToList();

            tmFormLoad = new System.Windows.Forms.Timer();
            tmFormLoad.Interval = 500;
            tmFormLoad.Tick += tmFormLoad_Tick;
            tmFormLoad.Start();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sendCommand("exit");
        }







        private void btStart_Click(object sender, EventArgs e)
        {
            btStart.Visible = false;
            lbScore.Visible = true;
            try
            {
                tmCountdown.Dispose();
            }
            catch { }

            tmCountdown = new System.Windows.Forms.Timer();
            tmCountdown.Interval = 1000;
            tmCountdown.Tick += tmCountdown_Tick;

            countdownValue = 3;
            lbStartCapture.Text = countdownValue.ToString();
            lbStartCapture.Visible = true;
            tmCountdown.Start();
        }
        private void btContinue_Click(object sender, EventArgs e)
        {
            pathPhotos.Add(destinationFilePath);
            if (score == 3)
            {
                processSelectBackground();
                return;
            }

            btContinue.Visible = false;
            btStart.Visible = true;
            btDelete.Visible = false;
            tmSteam.Start();
        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            btContinue.Visible = false;
            btStart.Visible = true;
            btDelete.Visible = false;
            File.Delete(destinationFilePath);
            score--;
            lbScore.Text = $"{score}/3";
            tmSteam.Start();
        }
        private void btConfig_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormConfig FormConfig = new FormConfig(this);
            FormConfig.ShowDialog();
            this.Show();
        }







        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (client != null && client.Connected)
            {
                stream.Close();
                client.Close();
            }
        }






        private async void tmFormLoad_Tick(object sender, EventArgs e)
        {
            tmFormLoad.Stop();
            await ConnectToServerAsync("127.0.0.1", 12345, 10000);

            tmSteam = new System.Windows.Forms.Timer();
            tmSteam.Interval = 50;
            tmSteam.Tick += tmSteam_Tick;
            tmSteam.Start();
        }
        private void tmSteam_Tick(object sender, EventArgs e)
        {
            getImageCommand("getFrame");
            if (!flagBtStart)
            {
                flagBtStart = true;
                btStart.Visible = true;
            }
        }
        private void tmWaitImagePython_Tick(object sender, EventArgs e)
        {
            if (File.Exists(sourceFilePath))
            {
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                if (flagNewFolder)
                {
                    string[] existingDirectories = Directory.GetDirectories(destinationDirectory);
                    string currentDate = DateTime.Now.ToString("yyyyMMdd");

                    var folderNumbers = existingDirectories
                        .Select(dir => Path.GetFileName(dir))
                        .Where(name => name.StartsWith(currentDate))
                        .Select(name =>
                        {
                            var parts = name.Split('_');
                            return parts.Length == 2 && int.TryParse(parts[1], out int number) ? number : (int?)null;
                        })
                        .Where(number => number.HasValue)
                        .Select(number => number.Value)
                        .ToList();

                    int nextFolderNumber = folderNumbers.Any() ? folderNumbers.Max() + 1 : 1;
                    string newFolderName = $"{currentDate}_{nextFolderNumber}";
                    string newFolderPath = Path.Combine(destinationDirectory, newFolderName);
                    Directory.CreateDirectory(newFolderPath);
                    destinationDirectory = newFolderPath;
                    flagNewFolder = false;
                }

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = "image_" + timestamp + ".jpg";
                destinationFilePath = Path.Combine(destinationDirectory, fileName);
                try
                {
                    File.Move(sourceFilePath, destinationFilePath);
                    tmWaitImagePython.Stop();
                    tmSteam.Stop();
                    using (var stream = new MemoryStream(File.ReadAllBytes(destinationFilePath)))
                    {
                        Image image = Image.FromStream(stream);
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        pbImage.Image = image;
                    }
                    processAfterCapture();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred while moving the file: " + ex.Message);
                }
            }
        }
        private void tmCountdown_Tick(object sender, EventArgs e)
        {
            countdownValue--;
            lbStartCapture.Text = countdownValue.ToString();

            if (countdownValue == 0)
            {
                tmCountdown.Stop();
                lbStartCapture.Visible = false;

                if (client != null && client.Connected)
                {
                    string message = "capture";
                    byte[] data = Encoding.UTF8.GetBytes($"*cmd={message}");
                    stream.Write(data, 0, data.Length);

                    data = new byte[1024];
                    int bytes = stream.Read(data, 0, data.Length);
                    string response = Encoding.UTF8.GetString(data, 0, bytes);

                    tmWaitImagePython = new System.Windows.Forms.Timer();
                    tmWaitImagePython.Interval = 200;
                    tmWaitImagePython.Tick += tmWaitImagePython_Tick;
                    tmWaitImagePython.Start();
                }
                else
                {
                    MessageBox.Show("Client is not connected to server!");
                }
            }
        }





        public string sendCommand(string message)
        {
            if (client != null && client.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes($"*cmd={message}");
                stream.Write(data, 0, data.Length);

                data = new byte[1024];
                int bytes = stream.Read(data, 0, data.Length);
                return Encoding.UTF8.GetString(data, 0, bytes);
            }
            else
            {
                MessageBox.Show("Client is not connected to server!");
                return "Client is not connected to server!";
            }
        }
        public void getImageCommand(string message)
        {
            if (client != null && client.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes($"*cmd={message}");
                stream.Write(data, 0, data.Length);

                byte[] sizeInfo = new byte[4];
                while (true)
                {
                    int bytesRead = stream.Read(sizeInfo, 0, sizeInfo.Length);
                    if (bytesRead == 0)
                        break;

                    int dataSize = BitConverter.ToInt32(sizeInfo, 0);
                    byte[] data2 = new byte[dataSize];
                    int totalBytesRead = 0;
                    while (totalBytesRead < dataSize)
                    {
                        bytesRead = stream.Read(data2, totalBytesRead, dataSize - totalBytesRead);
                        totalBytesRead += bytesRead;
                    }

                    using (MemoryStream ms = new MemoryStream(data2))
                    {
                        Image image = Image.FromStream(ms);
                        pbImage.Image = image;
                        lbOpenCamera.Visible = false;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Client is not connected to server!");
            }
        }
        private void processAfterCapture()
        {
            btContinue.Visible = true;
            btDelete.Visible = true;
            score++;
            lbScore.Text = $"{score}/3";
        }
        private void processSelectBackground()
        {
            this.Hide();
            FormPreview FormPreview = new FormPreview(this);
            FormPreview.ShowDialog();
            this.Show();
            score = 0;
            destinationDirectory = configManager.Get("PathOutputImage");
            flagNewFolder = true;
            pathPhotos.Clear();
            btContinue.Visible= false;
            btDelete.Visible= false;
            lbScore.Visible= false;
            btStart.Visible = true;
            lbScore.Text = "0/3";
            tmSteam.Start();
        }
        public async Task ConnectToServerAsync(string serverAddress, int port, int timeoutMilliseconds)
        {
            DateTime startTime = DateTime.Now;
            bool isConnected = false;

            while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMilliseconds)
            {
                try
                {
                    client = new TcpClient();
                    await client.ConnectAsync(serverAddress, port);

                    // หากการเชื่อมต่อสำเร็จ
                    stream = client.GetStream();
                    isConnected = true;
                    break;
                }
                catch (SocketException)
                {
                    await Task.Delay(500);
                }
            }

            if (!isConnected)
            {
                MessageBox.Show("Error: Unable to connect to server within the specified timeout.");
            }
        }

        private void lbStartCapture_Click(object sender, EventArgs e)
        {

        }
    }
}
