using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using MessageBox = System.Windows.MessageBox;


namespace SubMarine
{
    /// <summary>
    /// 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
 
        private string folder { get; set; }
        private void ChooseFolder(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            
        
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folder = dialog.SelectedPath;
                lblPath.Content = folder;
            }
            
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(folder))
            {
                lblError.Visibility = Visibility.Visible;
                lblError.Content = "Select a Folder";
            }


        }
    }
}
