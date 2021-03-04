using Expelibrum.Model;
using Expelibrum.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Expelibrum.Tests.Expelibrum.Services
{
    public class TagServiceShould
    {
        [Fact]
        public void ReturnFullPath()
        {
            var sut = new TagService();
            var book = new Book
            {
                title = "def"
            };
            IEnumerable<string> fileTags 
                = new List<string> { "title" };
            IEnumerable<string> directoryTags 
                = new List<string> { "title" };

            Assert.Equal(@"def\def",
                sut.GetFullPath(fileTags, directoryTags, book));
        }

        [Fact]
        public void JoinMultipleDirectoryTags()
        {
            var sut = new TagService();
            var book = new Book
            {
                number_of_pages = 10,
                title = "def"
            };
            IEnumerable<string> fileTags
                = new List<string> { "title" };
            IEnumerable<string> directoryTags
                = new List<string> { "title", "number_of_pages" };

            Assert.Equal(@"def\10\def",
                sut.GetFullPath(fileTags, directoryTags, book));
        }

        [Fact]
        public void JoinMultipleFileTags()
        {
            var sut = new TagService();
            var book = new Book
            {
                number_of_pages = 10,
                title = "def"
            };
            IEnumerable<string> fileTags
                = new List<string> { "title", "number_of_pages" };
            IEnumerable<string> directoryTags
                = new List<string> { "title" };

            Assert.Equal(@"def\def-10",
                sut.GetFullPath(fileTags, directoryTags, book));
        }

        [Fact]
        public void ReturnFullPathWithEmptyDirectoryTags()
        {
            var sut = new TagService();
            var book = new Book
            {
                title = "def"
            };
            IEnumerable<string> fileTags
                = new List<string> { "title" };
            IEnumerable<string> directoryTags
                = new List<string>();

            Assert.Equal(@"def",
                sut.GetFullPath(fileTags, directoryTags, book));
        }

        [Fact]
        public void ReplaceInvalidCharactersWithDash()
        {
            var sut = new TagService();
            var book = new Book
            {
                title = "abc<>:\"/\\|?*abc"
            };
            IEnumerable<string> fileTags
                = new List<string> { "title" };
            IEnumerable<string> directoryTags
                = new List<string>();

            Assert.Equal(@"abc---------abc",
                sut.GetFullPath(fileTags, directoryTags, book));
        }

        [Fact]
        public void ReturnCorrectTags()
        {
            var sut = new TagService();
            var book = new Book
            {
                title = "title",
                authors = new Author[] { new Author { name = "author" } },
                number_of_pages = 10,
                publishers = new Publisher[] { new Publisher { name = "publisher" } },
                publish_date = "publish_date",
                subjects = new Subject[] { new Subject { name = "subject" } }
            };
            IEnumerable<string> fileTags 
                = new List<string> 
                { 
                    "title",
                    "authors", 
                    "number_of_pages",
                    "publishers",
                    "publish_date",
                    "subjects"
                };
            IEnumerable<string> directoryTags
                = new List<string>();

            Assert.Equal(@"title-author-10-publisher-publish_date-subject",
                sut.GetFullPath(fileTags, directoryTags, book));
        }

        [Fact]
        public void ThrowAnExceptionWithEmptyFileTags()
        {
            var sut = new TagService();
            var book = new Book
            {
                title = "def"
            };
            IEnumerable<string> fileTags
                = new List<string>();
            IEnumerable<string> directoryTags
                = new List<string>();

            Assert.Throws<ArgumentException>(
                () => sut.GetFullPath(fileTags, directoryTags, book));
        }

        [Fact]
        public void ThrowAnExceptionWithIncorrectFileTags()
        {
            var sut = new TagService();
            var book = new Book
            {
                title = "def"
            };
            IEnumerable<string> fileTags
                = new List<string> { "aaaa" };
            IEnumerable<string> directoryTags
                = new List<string>() { "title" };

            Assert.Throws<ArgumentException>(
                () => sut.GetFullPath(fileTags, directoryTags, book));
        }

        [Fact]
        public void ThrowAnExceptionWithIncorrectDirectoryTags()
        {
            var sut = new TagService();
            var book = new Book
            {
                title = "def"
            };
            IEnumerable<string> fileTags
                = new List<string> { "title" };
            IEnumerable<string> directoryTags
                = new List<string>() { "aaaa" };

            Assert.Throws<ArgumentException>(
                () => sut.GetFullPath(fileTags, directoryTags, book));
        }
    }
}
