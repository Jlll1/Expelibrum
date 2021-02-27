using Expelibrum.Model;
using System.Collections.Generic;

namespace Expelibrum.UI.ViewModels
{
    public class NameTaggingViewModel
    {
        public List<Tag> Tags { get; }

        public NameTaggingViewModel()
        {
            Tags = new List<Tag>
            {
                new Tag("Title", "title"),
                new Tag("Author", "authors"),
                new Tag("PageCount", "number_of_pages"),
                new Tag("Publisher", "publishers"),
                new Tag("PublishDate", "publish_date"),
                new Tag("Subject", "subjects")
            };
        }
    }
}
