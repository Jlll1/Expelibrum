using Expelibrum.Services.Events;
using Expelibrum.UI.Events;
using System;
using System.Windows;

namespace Expelibrum.UI.ViewModels
{
    public class ProgressIndicatorViewModel : ViewModelBase, IProgressIndicatorViewModel
    {
        #region fields

        private IEventAggregator _ea;
        private Visibility _status = Visibility.Hidden;
        private string _statusDescription;
        private int _completed;
        private int _total;

        #endregion

        #region properties

        public Visibility Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        public string StatusDescription
        {
            get => _statusDescription;
            set
            {
                _statusDescription = value;
                OnPropertyChanged();
            }
        }
        public int Completed
        {
            get => _completed;
            set
            {
                _completed = value;
                OnPropertyChanged();
            }
        }
        public int Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region constructors

        public ProgressIndicatorViewModel(IEventAggregator ea)
        {
            _ea = ea;
            SubscribeToEvents();
        }

        #endregion

        #region methods

        private void SubscribeToEvents()
        {
            _ea.SubscribeToEvent("ProcessingProgressChanged", OnProcessingProgressChanged);
            _ea.SubscribeToEvent("ProcessingInitiated", OnProcessingInitiated);
        }
        private void OnProcessingInitiated(EventArgs e)
        {
            var args = e as ProcessingInitiatedEventArgs;
            Total = args.Total;
            ChangeStatus(true);

        }
        private void OnProcessingProgressChanged(EventArgs e)
        {
            var args = e as ProcessingProgressChangedEventArgs;
            Completed = args.Completed;

            if (Completed == Total)
            {
                ChangeStatus(false);
            }
        }
        private void ChangeStatus(bool inProgress)
        {
            if (inProgress)
            {
                Status = Visibility.Visible;
                StatusDescription = "In Progress";
            }
            else
            {
                StatusDescription = "Done";
            }
        }

        #endregion
    }
}
