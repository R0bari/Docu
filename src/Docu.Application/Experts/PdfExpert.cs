using Aspose.Words;
using Docu.Application.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Document = Aspose.Words.Document;

namespace Docu.Application.Experts;

public sealed class PdfExpert(ITelegramBotClient client, CancellationToken token) : IDisposable
{
    private MemoryStream? _pdfStream;
    
    public async Task<InputFileStream> Convert(string fileId, string fileName)
    {
        using var docStream = await client.DownloadFileToStream(fileId, token);
        _pdfStream = ToPdf(docStream);

        fileName = Path.GetFileNameWithoutExtension(fileName) + ".pdf";
        var pdfFile = InputFile.FromStream(_pdfStream, fileName);
        
        return pdfFile;
    }

    private static MemoryStream ToPdf(MemoryStream docStream)
    {
        var doc = new Document(docStream);
        var pdfStream = new MemoryStream();
        doc.Save(pdfStream, SaveFormat.Pdf);

        pdfStream.Position = 0;

        return pdfStream;
    }

    public void Dispose() =>
        _pdfStream?.Dispose();
}