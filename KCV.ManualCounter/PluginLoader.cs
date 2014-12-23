using Gizeta.KCV.ManualCounter.ViewModels;
using Gizeta.KCV.ManualCounter.Views;
using Grabacr07.KanColleViewer.Composition;
using System.ComponentModel.Composition;

namespace Gizeta.KCV.ManualCounter
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "KCV.ManualCounter")]
    [ExportMetadata("Description", "KanColleViewer手动计数插件。")]
    [ExportMetadata("Version", "1.0.0")]
    [ExportMetadata("Author", "@Gizeta")]
    public class PluginLoader : IToolPlugin
    {
        private static bool hasInitialized = false;

        public PluginLoader()
        {
            if (!hasInitialized)
            {
                hasInitialized = true;
                PluginSettings.Load();
                PluginSettings.Current.Save();
                CounterViewModel.Instance.Initialize();
            }
        }

        public string ToolName
        {
            get { return "计数"; }
        }

        public object GetSettingsView()
        {
            return null;
        }

        public object GetToolView()
        {
            return new CounterView { DataContext = CounterViewModel.Instance };
        }
    }
}
