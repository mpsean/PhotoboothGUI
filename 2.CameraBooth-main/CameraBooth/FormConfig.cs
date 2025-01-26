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
    public partial class FormConfig : Form
    {
        public FormConfig(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        Form1 form1;



        private void FormConfig_Load(object sender, EventArgs e)
        {
            using (var stream = new MemoryStream(File.ReadAllBytes(form1.configManager.Get("PathBackground"))))
            {
                pbBackground.Image = Image.FromStream(stream);
            }
            using (var stream = new MemoryStream(File.ReadAllBytes(form1.configManager.Get("PathEffect"))))
            {
                pbEffect.Image = Image.FromStream(stream);
            }
        }




        private void btBackground_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Path.GetFullPath(@"../../resource/background/");
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All Files (*.*)|*.*";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    using (var stream = new MemoryStream(File.ReadAllBytes(selectedFilePath)))
                    {
                        pbBackground.Image = Image.FromStream(stream);
                    }
                    form1.configManager.Set("PathBackground", openFileDialog.FileName);
                    form1.configManager.SaveConfig();
                }
            }
        }

        private void btEffect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Path.GetFullPath(@"../../resource/effect/");
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All Files (*.*)|*.*";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    using (var stream = new MemoryStream(File.ReadAllBytes(selectedFilePath)))
                    {
                        pbEffect.Image = Image.FromStream(stream);
                    }
                    form1.configManager.Set("PathEffect", openFileDialog.FileName);
                    form1.configManager.SaveConfig();
                }
            }
        }

        private void btPathResult_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Please select a folder.";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderDialog.SelectedPath;
                    form1.configManager.Set("PathOutputImage", selectedFolder);
                    form1.configManager.SaveConfig();
                    form1.destinationDirectory = form1.configManager.Get("PathOutputImage");
                    form1.flagNewFolder = true;
                }
            }
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
