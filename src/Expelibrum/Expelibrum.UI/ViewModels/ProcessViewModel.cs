using Expelibrum.Model;
using Expelibrum.Services;
using Expelibrum.UI.ViewModels.Dialogs;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public class ProcessViewModel : ViewModelBase, IProcessViewModel
    {

        #region fields

        private IFolderBrowserDialog _folderBrowser;
        private IPDFUtils _pdfUtils;
        private IIsbnService _isbnService;

        private string _originDirectoryPath;
        private string _targetDirectoryPath;

        #endregion

        #region properties

        public string OriginDirectoryPath
        {
            get => _originDirectoryPath;
            set
            {
                _originDirectoryPath = value;
                OnPropertyChanged();
            }
        }
        public string TargetDirectoryPath
        {
            get => _targetDirectoryPath;
            set
            {
                _targetDirectoryPath = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region commands

        public ICommand ChangeOriginDirectoryPathCommand { get; }
        public ICommand ChangeTargetDirectoryPathCommand { get; }
        public ICommand ProcessFilesCommand { get; }

        #region commandmethods

        private void OnChangeOriginDirectoryPath(object param)
        {
            OriginDirectoryPath = _folderBrowser.GetDirectoryPathDialog();
        }

        private void OnChangeTargetDirectoryPath(object param)
        {
            TargetDirectoryPath = _folderBrowser.GetDirectoryPathDialog();
        }

        private async void OnProcessFiles(object param)
        {
            var directory = new DirectoryInfo(OriginDirectoryPath);

            foreach (var file in directory.GetFiles("*.pdf"))
            {
                try
                {
                    Book book = await GetBookFromFileAsync(file.FullName);
                    string newTitle = book.title + ".pdf";
                    Directory.Move(file.FullName, Path.Combine(TargetDirectoryPath, newTitle));
                }
                catch (InvalidOperationException)
                {
                    Directory.Move(file.FullName, Path.Combine(TargetDirectoryPath, file.Name));
                }
            }
        }

        #endregion

        #endregion

        #region constructors
        public ProcessViewModel(IFolderBrowserDialog folderBrowser,
            IPDFUtils pdfUtils, IIsbnService isbnService)
        {
            _folderBrowser = folderBrowser;
            _pdfUtils = pdfUtils;
            _isbnService = isbnService;

            ChangeOriginDirectoryPathCommand = new RelayCommand(OnChangeOriginDirectoryPath);
            ChangeTargetDirectoryPathCommand = new RelayCommand(OnChangeTargetDirectoryPath);
            ProcessFilesCommand = new RelayCommand(OnProcessFiles);

        }

        #endregion

        #region methods

        private async Task<Book> GetBookFromFileAsync(string file)
        {
                string isbn = _pdfUtils.GetIsbn(file);
                return await _isbnService.GetBookFromIsbn(isbn);
        }

        #endregion

    }
}
