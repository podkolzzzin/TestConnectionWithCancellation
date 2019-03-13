using System.Data.SqlClient;
using System.Threading;
using System.Windows;

namespace TestConnection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private CancellationTokenSource tokenSource;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void XConnect_OnClick(object sender, RoutedEventArgs e)
        {
            var connectionString = $"Server={XHost.Text};User Id={XUser.Text};Password={XPassword.Password}";
            System.Data.SqlClient.SqlConnection connection = new SqlConnection {ConnectionString = connectionString};
            XConnect.IsEnabled = false;
            XCancel.IsEnabled = true;
            try
            {
                tokenSource = new CancellationTokenSource();
                await connection.OpenAsync(tokenSource.Token);
            }
            finally
            {
                XConnect.IsEnabled = true;
                XCancel.IsEnabled = false;
            }
        }

        private void XCancel_OnClick(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}