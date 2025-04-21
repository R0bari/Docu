using Aspose.Words;

namespace Docu.Application;

public static class PdfConverter
{
    public static MemoryStream ToPdf(MemoryStream docStream)
    {
        var doc = new Document(docStream);
        var pdfStream = new MemoryStream();
        doc.Save(pdfStream, SaveFormat.Pdf);

        pdfStream.Position = 0;

        return pdfStream;
    }
}