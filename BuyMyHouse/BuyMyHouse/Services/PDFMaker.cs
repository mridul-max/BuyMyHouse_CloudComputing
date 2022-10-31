using BuyMyHouse.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace BuyMyHouse.Services
{
    public static class PDFMaker
    {
        public static byte[] CreatePDF(Mortgage mortgage)
        {
            Document document = new Document(PageSize.A4, 50, 50, 50, 50);
            using (MemoryStream output = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(document, output);
                document.Open();
                Paragraph header = new Paragraph("This is your mortgage pdf");
                Paragraph paragraph = new Paragraph($"your mortgage amount is {mortgage.Amount}.");
                document.Add(header);
                document.Add(paragraph);
                document.Close();
                return output.ToArray();
            }
        }
    }
}
