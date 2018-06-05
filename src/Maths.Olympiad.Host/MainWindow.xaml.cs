using System.Windows;
using Maths.Olympiad.Host.ViewModels;

namespace Maths.Olympiad.Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MathematicsScreenViewModel();
        }
    }
}
