using System;
using System.Windows;
using System.Configuration;
using Maths.Olympiad.Dal;
using Maths.Olympiad.Dal.Interfaces;
using Maths.Olympiad.Host.Services;
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
                //var testDal = new DummyTestDal();

                var dialogService = new DialogService();
                dialogService.DisplayDialog += DialogServiceOnDisplayDialog;
                DataContext = new MathematicsScreenViewModel(testDal, dialogService);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void DialogServiceOnDisplayDialog(object sender, GenericEventArgs<object> genericEventArgs)
        {
            var dialogWindow = new DialogWindow() { DataContext = genericEventArgs.Data, Width=750, Height=600};

            dialogWindow.ShowDialog();
        }
    }
}
