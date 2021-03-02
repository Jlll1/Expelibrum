using Expelibrum.Model;
using Expelibrum.Services;
using System;
using System.Collections.Generic;
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
        public IDirectoryTaggingViewModel DirectoryTaggingViewModel { get; }

        #endregion

        #region commands
        public ICommand ProcessFilesCommand { get; }

        #region commandmethods

        private async void OnProcessFiles(object param)
        {
            var directory = new DirectoryInfo(DirectorySettings.OriginDirectoryPath);
            var searchOption = (SearchOption)Convert.ToInt32(DirectorySettings.IncludeSubdirectories);

            foreach (var file in directory.GetFiles("*.pdf", searchOption))
            {
                try
                {
                    Book book = await GetBookFromFileAsync(file.FullName);

                    var selectedTitleTags = NameTaggingViewModel.SelectedTags;
                    var selectedDirectoryTags = DirectoryTaggingViewModel.SelectedTags;

                    string title = GetFileName(selectedTitleTags, book);
                    string path = GetPath(selectedDirectoryTags, book);

                    string fullPath = Path.Combine(DirectorySettings.TargetDirectoryPath, path);
                    Directory.CreateDirectory(fullPath);
                    string fullTitle = title + ".pdf";
                    Directory.Move(file.FullName, Path.Combine(fullPath, fullTitle));
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
            IDirectoryTaggingViewModel directoryTaggingViewModel,
            IPDFUtils pdfUtils,
            IIsbnService isbnService)
        {
            _pdfUtils = pdfUtils;
            _isbnService = isbnService;

            DirectorySettings = directorySettingsViewModel;
            NameTaggingViewModel = nameTaggingViewModel;
            DirectoryTaggingViewModel = directoryTaggingViewModel;

            ProcessFilesCommand = new RelayCommand(OnProcessFiles, CanProcessFiles);
        }

        #endregion

        #region methods

        private async Task<Book> GetBookFromFileAsync(string file)
        {
            string isbn = _pdfUtils.GetIsbn(file);
            return await _isbnService.GetBookFromIsbn(isbn);
        }

        private string GetFileName(IEnumerable<string> selectedTags, Book book)
        {
            var tags = GetTags(selectedTags, book);

            return String.Join("-", tags);
        }

        private string GetPath(IEnumerable<string> selectedTags, Book book)
        {
            var tags = GetTags(selectedTags, book);

            return Path.Combine(tags.ToArray());
        }

        private List<string> GetTags(IEnumerable<string> selectedTags, Book book)
        {
            List<string> title = new List<string>();

            foreach (var tag in selectedTags)
            {
                var selectedProperty = typeof(Book).GetProperty(tag).GetValue(book);

                if (selectedProperty.GetType().IsArray)
                {
                    var selectedArray = selectedProperty as dynamic[];
                    title.Add(selectedArray[0].name);
                }
                else
                {
                    title.Add(selectedProperty as String);
                }
            }

            return title;
        }

        #endregion
    }
}
