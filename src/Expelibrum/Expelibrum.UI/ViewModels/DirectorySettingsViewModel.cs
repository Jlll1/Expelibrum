using Expelibrum.UI.ViewModels.Dialogs;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public class DirectorySettingsViewModel : ViewModelBase, IDirectorySettingsViewModel
    {
        #region fields

        private IFolderBrowserDialog _folderBrowser;

        private string _originDirectoryPath;
        private string _targetDirectoryPath;
        private bool _includeSubdirectories = true;

        #endregion

        #region properties

        public string OriginDirectoryPath
        {
            get => _originDirectoryPath;
            set
            {
                _originDirectoryPath = value;
                OnPropertyChanged();
                ValidateProperty();
            }
        }
        public string TargetDirectoryPath
        {
            get => _targetDirectoryPath;
            set
            {
                _targetDirectoryPath = value;
                OnPropertyChanged();
                ValidateProperty();
            }
        }

        public bool IncludeSubdirectories
        {
            get => _includeSubdirectories;
            set
            {
                _includeSubdirectories = value;
                OnPropertyChanged();
                ValidateProperty();
            }
        }

        #endregion

        #region commands

        public ICommand ChangeOriginDirectoryPathCommand { get; }
        public ICommand ChangeTargetDirectoryPathCommand { get; }

        #region commandmethods

        private void OnChangeOriginDirectoryPath(object param)
        {
            OriginDirectoryPath = _folderBrowser.GetDirectoryPathDialog();
        }

        private void OnChangeTargetDirectoryPath(object param)
        {
            TargetDirectoryPath = _folderBrowser.GetDirectoryPathDialog();
        }

        #endregion

        #endregion

        #region constructors
        public DirectorySettingsViewModel(IFolderBrowserDialog folderBrowser)
        {
            _folderBrowser = folderBrowser;

            ChangeOriginDirectoryPathCommand = new RelayCommand(OnChangeOriginDirectoryPath);
            ChangeTargetDirectoryPathCommand = new RelayCommand(OnChangeTargetDirectoryPath);
        }

        #endregion

        #region methods

        protected override IEnumerable<string> GetCustomErrors(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(OriginDirectoryPath):
                    if(!Directory.Exists(OriginDirectoryPath))
                    {
                        yield return "Not a valid path";
                    }
                    break;

                case nameof(TargetDirectoryPath):
                    if (!Directory.Exists(TargetDirectoryPath))
                    {
                        yield return "Not a valid path";
                    }
                    break;
            }
        }

        #endregion
    }
}
