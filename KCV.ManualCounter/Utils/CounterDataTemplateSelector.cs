using Gizeta.KCV.ManualCounter.Models;
using System.Windows;
using System.Windows.Controls;

namespace Gizeta.KCV.ManualCounter.Utils
{
    public class CounterDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CounterDataTemplate { get; set; }
        public DataTemplate NullDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item as Counter == null)
                return this.NullDataTemplate;
            else
                return this.CounterDataTemplate;
        }
    }
}
