using Expelibrum.Model;
using System.Collections.Generic;

namespace Expelibrum.Services
{
    public interface ITagService
    {
        string GetFullPath(IEnumerable<string> fileTags, IEnumerable<string> directoryTags, Book book);
    }
}