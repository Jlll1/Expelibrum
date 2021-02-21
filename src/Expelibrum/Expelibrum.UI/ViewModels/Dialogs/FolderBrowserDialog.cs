using System.IO;
using Microsoft.Win32;

namespace Expelibrum.UI.ViewModels.Dialogs
{
    public class FolderBrowserDialog : IFolderBrowserDialog
    {
        public string GetDirectoryPathDialog()
        {
            var dialog = new SaveFileDialog();
            var path = string.Empty;
            dialog.Title = "Select a Directory";
            dialog.Filter = "Directory|*.this.directory";
            dialog.FileName = "select";

            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            return path;
        }
    }
}
