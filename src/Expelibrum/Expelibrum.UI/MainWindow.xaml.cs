using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

namespace Expelibrum.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string originDirectory;
        private string targetDirectory;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseOrigin_Click(object sender, RoutedEventArgs e)
        {
            originDirectory = SelectDirectory();
            OriginTextBox.Text = originDirectory;
        }

        private void BrowseTarget_Click(object sender, RoutedEventArgs e)
        {
            targetDirectory = SelectDirectory();
            TargetTextBox.Text = targetDirectory;
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {

        }

        private string SelectDirectory()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }

            return string.Empty;
        }
    }
}
