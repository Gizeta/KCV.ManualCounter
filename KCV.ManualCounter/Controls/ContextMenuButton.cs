using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Gizeta.KCV.ManualCounter.Controls
{
    [TemplatePart(Name = PART_MenuItem, Type = typeof(MenuItem))]
    [TemplatePart(Name = PART_Button, Type = typeof(Button))]
    public class ContextMenuButton : ButtonBase
    {
        private const string PART_MenuItem = "PART_MenuItem";
        private const string PART_Button = "PART_Button";
        private Menu menu;
        private MenuItem topLevelHeaderMenuItem;
        private Button button;

        static ContextMenuButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContextMenuButton), new FrameworkPropertyMetadata(typeof(ContextMenuButton)));
        }

        #region Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            topLevelHeaderMenuItem = GetTemplateChild(PART_MenuItem) as MenuItem;

            if (button != null)
            {
                button.Click -= button_Click;
            }
            button = GetTemplateChild(PART_Button) as Button;
            if (button != null)
            {
                button.Click += button_Click;
            }
        }

        #endregion

        #region Properties

        #region Menu

        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register("Menu", typeof(MenuItem), typeof(ContextMenuButton), new PropertyMetadata(null));

        public MenuItem Menu
        {
            get { return (MenuItem)GetValue(MenuProperty); }
            set
            {
                if (value == null)
                {
                    ClearValue(MenuProperty);
                }
                else
                {
                    SetValue(MenuProperty, value);
                }
            }
        }

        #endregion

        #endregion

        #region Event Handlers

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (topLevelHeaderMenuItem != null)
            {
                topLevelHeaderMenuItem.IsSubmenuOpen = false;
            }
        }

        #endregion
    }
}
