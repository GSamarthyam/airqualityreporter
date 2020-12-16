using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace AirQualityReport.After
{
    public class ReportGenerator
    {
        public void Generate(IFieldFormatter formatter, string path, string heading)
        {
            using (var document = new Document(PageSize.LETTER, 15, 15, 15, 15))
            {
                // instantiate the writer that will listen to the document
                PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
                document.Open();

                var htmlBuilder = BuildHTMLPage(formatter, heading);

                var elements = HTMLWorker.ParseToList(
                    new StringReader(htmlBuilder.GenerateHTMLString()), null);

                foreach (var element in elements)
                {
                    document.Add(element);
                }
            }
        }

        private static HTMLBuilder BuildHTMLPage(IFieldFormatter formatter, string heading)
        {
            var htmlBuilder = new HTMLBuilder();
            BuildHTMLHeader(formatter, heading, htmlBuilder);
            htmlBuilder.AddBodyStart();
            CreateTableEntries(formatter, htmlBuilder);
            htmlBuilder.CloseBody();
            htmlBuilder.CloseHTML();
            return htmlBuilder;
        }

        private static void CreateTableEntries(IFieldFormatter formatter, HTMLBuilder htmlBuilder)
        {
            foreach (var pair in formatter.Values)
            {
                htmlBuilder.AddLineBreak();
                htmlBuilder.AddHeading2($"{pair.Key}");
                htmlBuilder.AddLineBreak();
                htmlBuilder.AddTableHeader(formatter.Headers);
                foreach (var tableValues in pair.Value)
                {
                    htmlBuilder.AddTableValues(tableValues);
                }

                htmlBuilder.CloseTableHeader();
                htmlBuilder.AddLineBreak();
            }
        }

        private static void BuildHTMLHeader(IFieldFormatter formatter, string heading, HTMLBuilder htmlBuilder)
        {
            htmlBuilder.AddHeading1(heading);
            htmlBuilder.AddLineBreak().AddLineBreak();
            htmlBuilder.AddHeading2($"Generated on: {formatter.LastUpdatedTime}");
        }
    }
}