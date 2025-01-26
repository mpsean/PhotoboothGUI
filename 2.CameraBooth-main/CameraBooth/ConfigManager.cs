using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CameraBooth
{
    public class ConfigManager
    {
        private string configFilePath;
        private Dictionary<string, string> configData;

        public ConfigManager(string filePath)
        {
            configFilePath = filePath;
            configData = new Dictionary<string, string>();

            if (File.Exists(configFilePath))
            {
                LoadConfig();
            }
        }

        // อ่านไฟล์คอนฟิก
        public void LoadConfig()
        {
            var lines = File.ReadAllLines(configFilePath);
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    var parts = line.Split(new[] { '=' }, 2);
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();

                    if (!configData.ContainsKey(key))
                    {
                        configData[key] = value;
                    }
                }
            }
        }

        // เขียนไฟล์คอนฟิก
        public void SaveConfig()
        {
            var lines = new List<string>();
            foreach (var kvp in configData)
            {
                lines.Add($"{kvp.Key}={kvp.Value}");
            }
            File.WriteAllLines(configFilePath, lines);
        }

        // อ่านค่าจากคอนฟิก
        public string Get(string key, string defaultValue = null)
        {
            return configData.ContainsKey(key) ? configData[key] : defaultValue;
        }

        // เขียนค่าลงคอนฟิก
        public void Set(string key, string value)
        {
            configData[key] = value;
        }
    }
}
