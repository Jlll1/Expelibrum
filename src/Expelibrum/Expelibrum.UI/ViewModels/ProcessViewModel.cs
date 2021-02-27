using Expelibrum.Model;
using Expelibrum.Services;
using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public class ProcessViewModel : ViewModelBase, IProcessViewModel
    {

        #region fields

        private IPDFUtils _pdfUtils;
        private IIsbnService _isbnService;

        #endregion

        #region properties

        public IDirectorySettingsViewModel DirectorySettings { get; }
        public INameTaggingViewModel NameTaggingViewModel { get; }

        #endregion

        #region commands
        public ICommand ProcessFilesCommand { get; }

        #region commandmethods

        private async void OnProcessFiles(object param)
        {
            var directory = new DirectoryInfo(DirectorySettings.OriginDirectoryPath);
            var searchOption = (SearchOption)Convert.ToInt32(DirectorySettings.IncludeSubdirectories);
            var selectedTag = NameTaggingViewModel.SelectedTag.PropertyName;

            foreach (var file in directory.GetFiles("*.pdf", searchOption))
            {
                try
                {
                    Book book = await GetBookFromFileAsync(file.FullName);

                    var selectedProperty = typeof(Book).GetProperty(selectedTag).GetValue(book);
                    string tagValue;

                    if (selectedProperty.GetType().IsArray)
                    {
                        var selectedArray = selectedProperty as dynamic[];
                        tagValue = selectedArray[0].name;
                    }
                    else
                    {
                        tagValue = selectedProperty as String;
                    }

                    string newTitle = tagValue + ".pdf";  
                    Directory.Move(file.FullName, Path.Combine(DirectorySettings.TargetDirectoryPath, newTitle));
                }
                catch (InvalidOperationException)
                {
                    Directory.Move(file.FullName, Path.Combine(DirectorySettings.TargetDirectoryPath, file.Name));
                }
            }
        }

        private bool CanProcessFiles(object param)
        {
            return !DirectorySettings.HasErrors
                && !string.IsNullOrEmpty(DirectorySettings.OriginDirectoryPath)
                && !string.IsNullOrEmpty(DirectorySettings.TargetDirectoryPath);
        }

        #endregion

        #endregion

        #region constructors
        public ProcessViewModel(IDirectorySettingsViewModel directorySettingsViewModel,
            INameTaggingViewModel nameTaggingViewModel,
            IPDFUtils pdfUtils,
            IIsbnService isbnService)
        {
            _pdfUtils = pdfUtils;
            _isbnService = isbnService;

            DirectorySettings = directorySettingsViewModel;
            NameTaggingViewModel = nameTaggingViewModel;

            ProcessFilesCommand = new RelayCommand(OnProcessFiles, CanProcessFiles);

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
