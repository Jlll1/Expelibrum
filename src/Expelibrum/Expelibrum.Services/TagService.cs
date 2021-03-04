using Expelibrum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Expelibrum.Services
{
    public class TagService : ITagService
    {
        public string GetFullPath(IEnumerable<string> fileTags,
            IEnumerable<string> directoryTags,
            Book book)
        {
            return Path.Combine(
                GetDirectoryPath(directoryTags, book),
                GetFileName(fileTags, book)
                );
        }

        private string GetFileName(IEnumerable<string> tags, Book book)
        {
            if (!tags.Any())
            {
                throw new ArgumentException("fileTags cannot be empty");
            }

            var processedTags = GetTags(tags, book);

            return String.Join('-', processedTags);
        }

        private string GetDirectoryPath(IEnumerable<string> tags, Book book)
        {
            var processedTags = GetTags(tags, book);

            return Path.Combine(processedTags.ToArray());
        }

        private List<string> GetTags(IEnumerable<string> selectedTags, Book book)
        {
            List<string> tags = new List<string>();

            foreach (var tag in selectedTags)
            {
                object selectedProperty;

                try
                {
                    selectedProperty = typeof(Book).GetProperty(tag).GetValue(book);
                }
                catch(NullReferenceException)
                {
                    throw new ArgumentException("Provided tags don't match any property on the Book object");
                }

                if (selectedProperty.GetType().IsArray)
                {
                    var selectedArray = selectedProperty as dynamic[];
                    tags.Add(selectedArray[0].name);
                }
                else
                {
                    tags.Add(selectedProperty.ToString());
                }
            }

            return ValidateTags(tags);
        }

        private List<string> ValidateTags(List<string> tags)
        {
            List<string> result = new List<string>();
            foreach (var tag in tags)
            {
                result.Add(
                    String.Join("-", 
                    tag.Split(Path.GetInvalidFileNameChars()))
                    );
            }
            return result;
        }
    }
}
