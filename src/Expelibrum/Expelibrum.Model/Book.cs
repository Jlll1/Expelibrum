namespace Expelibrum.Model
{

    public class Book
    {
        public string url { get; set; }
        public string key { get; set; }
        public string title { get; set; }
        public Author[] authors { get; set; }
        public int number_of_pages { get; set; }
        public Identifiers identifiers { get; set; }
        public Publisher[] publishers { get; set; }
        public string publish_date { get; set; }
        public Subject[] subjects { get; set; }
        public Cover cover { get; set; }
    }

    public class Identifiers
    {
        public string[] isbn_10 { get; set; }
        public string[] isbn_13 { get; set; }
        public string[] openlibrary { get; set; }
    }

    public class Cover
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
    }

    public class Author
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Publisher
    {
        public string name { get; set; }
    }

    public class Subject
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}
