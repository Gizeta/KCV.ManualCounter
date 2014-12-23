using System.Windows;
using System.Windows.Controls;

namespace Gizeta.KCV.ManualCounter.Controls
{
    public class SubMenu : MenuItem
    {
        static SubMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SubMenu), new FrameworkPropertyMetadata(typeof(SubMenu)));
        }
    }
}
