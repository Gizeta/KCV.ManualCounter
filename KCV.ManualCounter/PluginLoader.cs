using Gizeta.KCV.ManualCounter.Utils;
using Gizeta.KCV.ManualCounter.ViewModels;
using Gizeta.KCV.ManualCounter.Views;
using Grabacr07.KanColleViewer.Composition;
using System;
using System.ComponentModel.Composition;

namespace Gizeta.KCV.ManualCounter
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "KCV.ManualCounter")]
    [ExportMetadata("Description", "KanColleViewer手动计数插件。")]
    [ExportMetadata("Version", "1.1.0")]
    [ExportMetadata("Author", "@Gizeta")]
    public class PluginLoader : IToolPlugin
    {
        private static bool hasInitialized = false;

        public PluginLoader()
        {
            if (!hasInitialized)
            {
                hasInitialized = true;
                loadAssembly();
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

        private void loadAssembly()
        {
            EmbeddedAssemblyLoader.Load("Gizeta.KCV.ManualCounter.Libraries.IronRuby.Microsoft.Dynamic.dll", "Microsoft.Dynamic.dll");
            EmbeddedAssemblyLoader.Load("Gizeta.KCV.ManualCounter.Libraries.IronRuby.Microsoft.Scripting.dll", "Microsoft.Scripting.dll");
            EmbeddedAssemblyLoader.Load("Gizeta.KCV.ManualCounter.Libraries.IronRuby.Microsoft.Scripting.Metadata.dll", "Microsoft.Scripting.Metadata.dll");
            EmbeddedAssemblyLoader.Load("Gizeta.KCV.ManualCounter.Libraries.IronRuby.IronRuby.dll", "IronRuby.dll");
            EmbeddedAssemblyLoader.Load("Gizeta.KCV.ManualCounter.Libraries.IronRuby.IronRuby.Libraries.dll", "IronRuby.Libraries.dll");

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                return EmbeddedAssemblyLoader.Get(args.Name);
            };
        }
    }
}
