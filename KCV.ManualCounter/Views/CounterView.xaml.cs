using Gizeta.KCV.ManualCounter.ViewModels;
using System.Windows;
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

        private void CounterView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            /* 固定CounterContainer宽度。防止父元素宽度为auto时，不能换行。(针对yuyuvn版KCV等) */
            CounterContainer.ItemsSource = null;
            CounterContainer.Width = this.ActualWidth - 16;
            CounterContainer.ItemsSource = CounterViewModel.Instance.Counters;
        }
    }
}
