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

namespace _711_A1
{
    /// <summary>
    /// Interaction logic for CacheWindow.xaml
    /// </summary>
    public partial class CacheWindow : Window
    {
        ServerServiceClient client;

        public CacheWindow()
        {
            InitializeComponent();
            client = new ServerServiceClient();
        }

        private void listFilesButton_Click(object sender, RoutedEventArgs e)
        {
            string[] serverFileList = client.GetFileList();
            filesListView.ItemsSource = serverFileList;
            
        }
    }
}
