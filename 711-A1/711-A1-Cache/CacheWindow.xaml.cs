using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _711_A1_Cache.ServiceReference1;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using _711_A1;

namespace _711_A1
{
    /// <summary>
    /// Interaction logic for CacheWindow.xaml
    /// </summary>
    public partial class CacheWindow : Window
    {
        private ServerServiceClient client;

        public CacheWindow()
        {
            InitializeComponent();
            client = new ServerServiceClient();

            Uri baseAddress = new Uri("http://localhost:8082/711A1/Cache");

            ServiceHost selfHost = new ServiceHost(typeof(CacheService), baseAddress);

            try
            {
                BasicHttpBinding bsb = new BasicHttpBinding();
                bsb.TransferMode = TransferMode.StreamedResponse;
                ServiceEndpoint serverEndpoint = selfHost.AddServiceEndpoint(typeof(ICacheService), bsb, "CacheService");
                DispatcherSynchronizationBehavior dmb = new DispatcherSynchronizationBehavior();
                dmb.AsynchronousSendEnabled = true;
                serverEndpoint.EndpointBehaviors.Add(dmb);

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                selfHost.Open();
                //Console.WriteLine("The server service is ready");
                //Console.WriteLine("Press Enter to terminate service");
                //Console.WriteLine();
                //Console.ReadLine();

                //selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                //Console.Error.WriteLine("An exception occurred: {0}", ce.Message);
                MessageBox.Show(ce.Message, "Error deleting cached files", MessageBoxButton.OK, MessageBoxImage.Error);
                selfHost.Abort();
            }
        }

        private void listFilesButton_Click(object sender, RoutedEventArgs e)
        {
            logBox.Visibility = Visibility.Collapsed;
            logBox.IsEnabled = false;
            filesListView.ItemsSource = client.GetFileList();
            filesListView.IsEnabled = true;
            filesListView.Visibility = Visibility.Visible;
            //string[] serverFileList = client.GetFileList();
            //filesListView.ItemsSource = serverFileList;
            //filesListView.ItemsSource = client.GetFileList();
            //string[] cacheFileList = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\cache");
            //filesListView.ItemsSource = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\cache");
        }

        private void viewFilesButton_Click(object sender, RoutedEventArgs e)
        {
            filesListView.IsEnabled = true;
            filesListView.Visibility = Visibility.Visible;
            filesListView.ItemsSource = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\cache");
        }

        private void viewLogButton_Click(object sender, RoutedEventArgs e)
        {
            filesListView.Visibility = Visibility.Collapsed;
            filesListView.IsEnabled = false;
            logBox.Visibility = Visibility.Visible;
            logBox.IsEnabled = true;
            logBox.Text = File.ReadAllText(Directory.GetCurrentDirectory() + "CacheLog.txt");
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] fileList = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\cache\\");
                Parallel.ForEach(fileList, (fileName => File.Delete(fileName)));
                using (StreamWriter logout = File.AppendText(Directory.GetCurrentDirectory() + "CacheLog.txt"))
                {
                    logout.WriteLineAsync(string.Format("\nUser request: Delete cached files", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString()));
                    logout.WriteLineAsync(string.Format("Response: Deleted cached files"));
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Error deleting cached files", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
