using System.Windows;

namespace Gizeta.KCV.ManualCounter.Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            PluginLoader loader = new PluginLoader();
            
            InitializeComponent();

            root.Children.Add(loader.GetToolView() as UIElement);
        }
    }
}
