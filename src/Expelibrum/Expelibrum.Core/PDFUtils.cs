using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.Content;
using PdfSharpCore.Pdf.Content.Objects;
using PdfSharpCore.Pdf.IO;
using System;
using System.Linq;
using System.Text;

namespace Expelibrum.Services
{
    public class PDFUtils
    {
        public string GetIsbn(string file)
        {
            using (var _document = PdfReader.Open(file, PdfDocumentOpenMode.ReadOnly))
            {

                foreach (var page in _document.Pages.OfType<PdfPage>())
                {
                    var result = new StringBuilder();
                    ExtractText(ContentReader.ReadContent(page), result);

                    var isbn = FindIsbn(result.ToString());
                    if (isbn != string.Empty)
                    {
                        return isbn;
                    }
                }

                throw new InvalidOperationException("Isbn not found in the document");
            }
        }

        private string FindIsbn(string text)
        {
            var substrings = text.Split(" ");

            for (int i = 0; i < substrings.Length; i++)
            {
                if (substrings[i].ToLowerInvariant().Equals("isbn"))
                {
                    return substrings[i + 1];
                }
            }

            return string.Empty;
        }


        #region CObject Visitor
        private static void ExtractText(CObject obj, StringBuilder target)
        {
            if (obj is CArray)
                ExtractText((CArray)obj, target);
            else if (obj is CComment)
                ExtractText((CComment)obj, target);
            else if (obj is CInteger)
                ExtractText((CInteger)obj, target);
            else if (obj is CName)
                ExtractText((CName)obj, target);
            else if (obj is CNumber)
                ExtractText((CNumber)obj, target);
            else if (obj is COperator)
                ExtractText((COperator)obj, target);
            else if (obj is CReal)
                ExtractText((CReal)obj, target);
            else if (obj is CSequence)
                ExtractText((CSequence)obj, target);
            else if (obj is CString)
                ExtractText((CString)obj, target);
            else
                throw new NotImplementedException(obj.GetType().AssemblyQualifiedName);
        }

        private static void ExtractText(CArray obj, StringBuilder target)
        {
            foreach (var element in obj)
            {
                ExtractText(element, target);
            }
        }
        private static void ExtractText(CComment obj, StringBuilder target) { /* nothing */ }
        private static void ExtractText(CInteger obj, StringBuilder target) { /* nothing */ }
        private static void ExtractText(CName obj, StringBuilder target) { /* nothing */ }
        private static void ExtractText(CNumber obj, StringBuilder target) { /* nothing */ }
        private static void ExtractText(COperator obj, StringBuilder target)
        {
            if (obj.OpCode.OpCodeName == OpCodeName.Tj || obj.OpCode.OpCodeName == OpCodeName.TJ)
            {
                foreach (var element in obj.Operands)
                {
                    ExtractText(element, target);
                }
                target.Append(" ");
            }
        }
        private static void ExtractText(CReal obj, StringBuilder target) { /* nothing */ }
        private static void ExtractText(CSequence obj, StringBuilder target)
        {
            foreach (var element in obj)
            {
                ExtractText(element, target);
            }
        }
        private static void ExtractText(CString obj, StringBuilder target)
        {
            target.Append(obj.Value);
        }
        #endregion
    }
}
