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
        }
    }
}
