using System;
using System.Collections.Generic;
using System.Text;

namespace Expelibrum.Core
{
    public class AuthorsItem
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Identifiers
    {
        public List<string> isbn_10 { get; set; }
        public List<string> isbn_13 { get; set; }
        public List<string> openlibrary { get; set; }
    }

    public class PublishersItem
    {
        public string name { get; set; }
    }

    public class SubjectsItem
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Cover
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
    }

    public class Book
{
        public string url { get; set; }
        public string key { get; set; }
        public string title { get; set; }
        public List<AuthorsItem> authors { get; set; }
        public int number_of_pages { get; set; }
        public Identifiers identifiers { get; set; }
        public List<PublishersItem> publishers { get; set; }
        public string publish_date { get; set; }
        public List<SubjectsItem> subjects { get; set; }
        public Cover cover { get; set; }
    }
}
