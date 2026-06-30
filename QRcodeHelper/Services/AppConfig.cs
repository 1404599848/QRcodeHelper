using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QRcodeHelper.Services
{
    public static class AppConfig
    {
        private static string configPath = "./Config.xml";

        public static string SerialPort { get; set; } = "COM3";
        public static int BaudRate { get; set; } = 9600;
        public static bool AlertEnabled { get; set; } = true;
        public static int FlashIntervalMs { get; set; } = 500;
        public static int AlertDurationSec { get; set; } = 10;
        public static string ConfigPassword { get; set; } = "1234";

        public static void Load()
        {
            try
            {
                if (!File.Exists(configPath))
                {
                    Save();
                    return;
                }

                var doc = XDocument.Load(configPath);
                var root = doc.Root;

                SerialPort = root.Element("SerialPort")?.Value ?? "COM3";
                BaudRate = int.Parse(root.Element("BaudRate")?.Value ?? "9600");
                AlertEnabled = bool.Parse(root.Element("AlertEnabled")?.Value ?? "true");
                FlashIntervalMs = int.Parse(root.Element("FlashIntervalMs")?.Value ?? "500");
                AlertDurationSec = int.Parse(root.Element("AlertDurationSec")?.Value ?? "10");
                ConfigPassword = root.Element("ConfigPassword")?.Value ?? "1234";
            }
            catch
            {
                Save();
            }
        }

        public static void Save()
        {
            var doc = new XDocument(
                new XElement("Config",
                    new XElement("SerialPort", SerialPort),
                    new XElement("BaudRate", BaudRate),
                    new XElement("AlertEnabled", AlertEnabled),
                    new XElement("FlashIntervalMs", FlashIntervalMs),
                    new XElement("AlertDurationSec", AlertDurationSec),
                    new XElement("ConfigPassword", ConfigPassword)
                )
            );
            doc.Save(configPath);
        }
    }
}
