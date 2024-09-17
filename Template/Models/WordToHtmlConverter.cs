using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;
using System.Text;

namespace Template.Models
{
    public class WordToHtmlConverter
    {
        public string ConvertWordToHtml(string filePath)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
            {
                Body body = wordDoc.MainDocumentPart.Document.Body;

                htmlBuilder.Append("<html><head><style>");
                htmlBuilder.Append("table {border-collapse: collapse; width: 100%;}");
                htmlBuilder.Append("td, th {border: 1px solid black; padding: 8px;}");
                htmlBuilder.Append("p {margin: 0;}");
                htmlBuilder.Append("</style></head><body>");

                foreach (var element in body.Elements<Paragraph>())
                {
                    htmlBuilder.Append("<p style='" + GetParagraphStyle(element) + "'>");
                    foreach (var run in element.Elements<Run>())
                    {
                        htmlBuilder.Append("<span style='" + GetRunStyle(run) + "'>");
                        foreach (var text in run.Elements<Text>())
                        {
                            htmlBuilder.Append(System.Net.WebUtility.HtmlEncode(text.Text));
                        }
                        htmlBuilder.Append("</span>");
                    }
                    htmlBuilder.Append("</p>");
                }

                foreach (var table in body.Elements<Table>())
                {
                    htmlBuilder.Append("<table>");
                    foreach (var row in table.Elements<TableRow>())
                    {
                        htmlBuilder.Append("<tr>");
                        foreach (var cell in row.Elements<TableCell>())
                        {
                            htmlBuilder.Append("<td style='" + GetCellStyle(cell) + "'>");
                            foreach (var paragraph in cell.Elements<Paragraph>())
                            {
                                htmlBuilder.Append("<p style='" + GetParagraphStyle(paragraph) + "'>");
                                foreach (var run in paragraph.Elements<Run>())
                                {
                                    htmlBuilder.Append("<span style='" + GetRunStyle(run) + "'>");
                                    foreach (var text in run.Elements<Text>())
                                    {
                                        htmlBuilder.Append(System.Net.WebUtility.HtmlEncode(text.Text));
                                    }
                                    htmlBuilder.Append("</span>");
                                }
                                htmlBuilder.Append("</p>");
                            }
                            htmlBuilder.Append("</td>");
                        }
                        htmlBuilder.Append("</tr>");
                    }
                    htmlBuilder.Append("</table>");
                }

                htmlBuilder.Append("</body></html>");
            }

            return htmlBuilder.ToString();
        }

        private string GetParagraphStyle(Paragraph paragraph)
        {
            StringBuilder styleBuilder = new StringBuilder();
            var paragraphProperties = paragraph.ParagraphProperties;

            if (paragraphProperties != null)
            {
                var shading = paragraphProperties.Descendants<Shading>().FirstOrDefault();
                if (shading != null)
                {
                    styleBuilder.Append($"background-color: #{shading.Fill};");
                }
            }

            return styleBuilder.ToString();
        }

        private string GetRunStyle(Run run)
        {
            StringBuilder styleBuilder = new StringBuilder();
            var runProperties = run.RunProperties;

            if (runProperties != null)
            {
                var color = runProperties.Color;
                if (color != null)
                {
                    styleBuilder.Append($"color: #{color.Val};");
                }

                var runFonts = runProperties.RunFonts;
                if (runFonts != null)
                {
                    styleBuilder.Append($"font-family: '{runFonts.Ascii}';");
                }

                var fontSize = runProperties.FontSize;
                if (fontSize != null)
                {
                    styleBuilder.Append($"font-size: {int.Parse(fontSize.Val) / 2}pt;");
                }
            }

            return styleBuilder.ToString();
        }

        private string GetCellStyle(TableCell cell)
        {
            StringBuilder styleBuilder = new StringBuilder();
            var cellProperties = cell.TableCellProperties;

            if (cellProperties != null)
            {
                var shading = cellProperties.Descendants<Shading>().FirstOrDefault();
                if (shading != null)
                {
                    styleBuilder.Append($"background-color: #{shading.Fill};");
                }
            }

            return styleBuilder.ToString();
        }
    }
}
