using System.Windows;

namespace Expelibrum.UI.ViewModels
{
    public interface IProgressIndicatorViewModel
    {
        int Completed { get; set; }
        Visibility Status { get; }
        int Total { get; set; }
    }
}