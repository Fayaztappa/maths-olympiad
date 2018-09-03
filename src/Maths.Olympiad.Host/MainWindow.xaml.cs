using System;
using System.Windows;
using System.Configuration;
using Maths.Olympiad.Dal;
using Maths.Olympiad.Dal.Interfaces;
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

            try
            {
                var connection = ConfigurationManager.AppSettings["Connection"];
                var keySpace = ConfigurationManager.AppSettings["KeySpace"];
                var SqlConnectionString = ConfigurationManager.AppSettings["SqlConnection"];

                //var testDal = new TestDal(connection, keySpace, new JSonSerializer());
                var testDal = new SqlServerTestDal(SqlConnectionString, new JSonSerializer());
                DataContext = new MathematicsScreenViewModel(testDal);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
