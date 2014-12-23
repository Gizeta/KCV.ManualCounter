using Gizeta.KCV.ManualCounter.ViewModels;
using System.Windows.Controls;
using KCVApp = Grabacr07.KanColleViewer.App;

namespace Gizeta.KCV.ManualCounter.Views
{
    public partial class CounterView : UserControl
    {
        public CounterView()
        {
            InitializeComponent();

            this.Resources.MergedDictionaries.Add(KCVApp.Current.Resources);
        }

        private void CounterView_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            CounterViewModel.Instance.UpdateCounter();
        }
    }
}
