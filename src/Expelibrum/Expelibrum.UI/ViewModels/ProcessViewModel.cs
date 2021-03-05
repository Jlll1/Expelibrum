using Expelibrum.Model;
using Expelibrum.Services;
using Expelibrum.Services.Events;
using Expelibrum.UI.Events;
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
        private ITagService _tagSerivce;
        private IFileMoverService _fileMoverService;
        private IEventAggregator _ea;

        #endregion

        #region properties

        public IDirectorySettingsViewModel DirectorySettings { get; }
        public INameTaggingViewModel NameTaggingViewModel { get; }
        public IDirectoryTaggingViewModel DirectoryTaggingViewModel { get; }
        public IProgressIndicatorViewModel ProgressIndicatorViewModel { get; }

        #endregion

        #region commands
        public ICommand ProcessFilesCommand { get; }

        #region commandmethods

        private async void OnProcessFiles(object param)
        {
            var directory = new DirectoryInfo(DirectorySettings.OriginDirectoryPath);
            var targetPath = DirectorySettings.TargetDirectoryPath;
            var searchOption = (SearchOption)Convert.ToInt32(DirectorySettings.IncludeSubdirectories);
            var files = directory.GetFiles("*.pdf", searchOption);
            IEnumerable<string> selectedTitleTags = NameTaggingViewModel.SelectedTags;
            IEnumerable<string> selectedDirectoryTags = DirectoryTaggingViewModel.SelectedTags;

            int filesProcessed = 0;
            _ea.PublishEvent("ProcessingInitiated",
                new ProcessingInitiatedEventArgs
                {
                    Total = files.Length
                });
            foreach (var file in files)
            {
                try
                {
                    Book book = await GetBookFromFileAsync(file.FullName);
                    string fullPath = _tagSerivce.GetFullPath(selectedTitleTags, selectedDirectoryTags, book);
                    _fileMoverService.Move(file.FullName, new string[] { targetPath, fullPath });
                }
                catch 
                { }
                filesProcessed++;
                _ea.PublishEvent("ProcessingProgressChanged",
                    new ProcessingProgressChangedEventArgs
                    {
                        Completed = filesProcessed
                    });
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
            IProgressIndicatorViewModel progressIndicatorViewModel,
            IPDFUtils pdfUtils,
            IIsbnService isbnService,
            IFileMoverService fileMoverService,
            ITagService tagService,
            IEventAggregator ea)
        {
            _pdfUtils = pdfUtils;
            _isbnService = isbnService;
            _tagSerivce = tagService;
            _fileMoverService = fileMoverService;
            _ea = ea;

            DirectorySettings = directorySettingsViewModel;
            NameTaggingViewModel = nameTaggingViewModel;
            DirectoryTaggingViewModel = directoryTaggingViewModel;
            ProgressIndicatorViewModel = progressIndicatorViewModel;

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
