using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Maths.Olympiad.Host.Views
{
    /// <summary>
    /// Interaction logic for OperationView.xaml
    /// </summary>
    public partial class OperationView : UserControl
    {
        public OperationView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
            {
                return;
            }

            FocusManager.SetFocusedElement(this, null);

            //button.MoveFocus();
        }
    }
}
