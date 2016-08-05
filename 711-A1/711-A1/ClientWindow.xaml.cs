using System;
using System.Collections.Generic;
using System.IO;
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

namespace _711_A1
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private CacheService.CacheServiceClient client;

        public ClientWindow()
        {
            InitializeComponent();
            client = new CacheService.CacheServiceClient();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            filesListView.ItemsSource = client.GetFileList();
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            /*ProcessStartInfo pi = new ProcessStartInfo(file);
pi.Arguments = Path.GetFileName(file);
pi.UseShellExecute = true;
pi.WorkingDirectory = Path.GetDirectoryName(file);
pi.FileName = file;
pi.Verb = "OPEN";
Process.Start(pi);

or

    Process.Start(file);

Apparently this is in System.Diagnostics.   See http://stackoverflow.com/questions/10174156/open-file-with-associated-application
 */
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedFile = filesListView.SelectedItem as ListViewItem;
            string selectedFileName = selectedFile.Content as string;
            using (FileStream downloadedFile = await client.GetFileAsync(selectedFileName) as FileStream)
            {
                using (FileStream saveFile = new FileStream("\\client\\" + selectedFileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    await downloadedFile.CopyToAsync(saveFile);
                    await saveFile.FlushAsync();
                    MessageBox.Show("File {0} downloaded.");

                }
            }
        }
    }
}
