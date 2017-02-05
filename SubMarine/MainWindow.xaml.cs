using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using SubMarine.Managers;


namespace SubMarine
{
    /// <summary>
    /// 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private readonly FileInfo fileInfo;

      private readonly OpenSubConnector connector;
        public MainWindow()
        {
            this.fileInfo = new FileInfo();
            InitializeComponent();
            connector = new OpenSubConnector();
            connector.Login();

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

            var extensionAllowed = new List<string> { ".mkv", ".mp4" };

            var filteredFiles = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)
            .Where(s => extensionAllowed.Contains(Path.GetExtension(s)));

           foreach (var file in filteredFiles)
           {
               fileInfo.Filename = file;
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(file), "Subs"));
                ProcessFile(file);
            }
        }

        private void ProcessFile(string fileName)
        {
            var di = new DirectoryInfo(fileName);
            di.Attributes &= ~FileAttributes.ReadOnly;

            var movieHashCalculator = new MovieHashCalculator();

            fileInfo.Hash = movieHashCalculator.ComputeMovieHash(fileName);
            fileInfo.Size = movieHashCalculator.Streamsize;

             connector.SearchSubtitles("es", fileInfo);
        }

    }
}
