using Expelibrum.Model;
using System.Collections.Generic;

namespace Expelibrum.UI.ViewModels
{
    public interface INameTaggingViewModel
    {
        Tag SelectedTag { get; set; }
        List<Tag> Tags { get; }
    }
}