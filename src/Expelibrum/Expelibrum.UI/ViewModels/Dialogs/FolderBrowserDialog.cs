using System.IO;

namespace Expelibrum.UI.ViewModels.Dialogs
{
    public class FolderBrowserDialog : IFolderBrowserDialog
    {
        public string GetDirectoryPathDialog()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Select a Directory",
                UseDescriptionForTitle = true
            };
            var path = string.Empty;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = dialog.SelectedPath;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            return path;
        }
    }
}