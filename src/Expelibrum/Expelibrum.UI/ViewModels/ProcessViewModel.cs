using Expelibrum.UI.ViewModels.Dialogs;
using System;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public class ProcessViewModel : ViewModelBase, IProcessViewModel
    {

        #region fields

        private IFolderBrowserDialog _folderBrowser;
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

        private void OnProcessFiles(object param)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region constructors
        public ProcessViewModel(IFolderBrowserDialog folderBrowser)
        {
            _folderBrowser = folderBrowser;
            ChangeOriginDirectoryPathCommand = new RelayCommand(OnChangeOriginDirectoryPath);
            ChangeTargetDirectoryPathCommand = new RelayCommand(OnChangeTargetDirectoryPath);
            ProcessFilesCommand = new RelayCommand(OnProcessFiles);
        }

        #endregion

    }
}
