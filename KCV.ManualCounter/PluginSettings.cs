using Gizeta.KCV.ManualCounter.Models;
using Grabacr07.KanColleViewer.Models.Data.Xml;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gizeta.KCV.ManualCounter
{
    [Serializable]
    public class PluginSettings
    {
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "grabacr.net",
            "KanColleViewer",
            "KCV.ManualCounter.xml");

        public static PluginSettings Current { get; set; }

        public static void Load()
        {
            try
            {
                Current = filePath.ReadXml<PluginSettings>();
            }
            catch
            {
                Current = GetInitialSettings();
            }
        }

        public void Save()
        {
            try
            {
                this.WriteXml(filePath);
            }
            catch { }
        }

        public static PluginSettings GetInitialSettings()
        {
            return new PluginSettings
            {
                Counters = new List<Counter>(),
                CounterWidth = 200,
                CounterHeight = 90,
                SettingsVersion = 0
            };
        }

        public List<Counter> Counters { get; set; }

        public double CounterWidth { get; set; }

        public double CounterHeight { get; set; }

        public int SettingsVersion { get; set; }
    }
}
