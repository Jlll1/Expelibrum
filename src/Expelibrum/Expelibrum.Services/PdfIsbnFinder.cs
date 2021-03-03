using System;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;

namespace Expelibrum.Services
{
    public class PdfIsbnFinder : IPDFUtils
    {
        public string GetIsbn(string file)
        {
            using (var document = PdfDocument.Open(file))
            {
                foreach (var page in document.GetPages())
                {
                    var isbn = FindIsbn(page.Text);
                    if (isbn != string.Empty)
                    {
                        return isbn;
                    }
                }
                throw new InvalidOperationException("Isbn not found in the document");
            }
        }

        public string FindIsbn(string text)
        {
            var substrings = text.Split(" ");

            for (int i = 0; i < substrings.Length; i++)
            {
                if (substrings[i].ToLowerInvariant().Contains("isbn"))
                {
                    for (int j = 0; j < substrings.Length - i; j++)
                    {
                        string substring = substrings[i + j];
                        if (substring.Length >= 10)
                        {
                            substring = Regex.Replace(substring, @"[^0-9]", "");
                            if (Int64.TryParse(substring, out _) && substring.Length >= 10)
                            {
                                return substring;
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
