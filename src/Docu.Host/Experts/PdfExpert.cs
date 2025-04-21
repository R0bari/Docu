using Docu.Application;
using Docu.Host.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Docu.Host.Experts;

public sealed class PdfExpert(ITelegramBotClient client, CancellationToken token)
{
    public async Task<InputFile> Convert(string fileId, string fileName)
    {
        using var docStream = await client.DownloadFileToStream(fileId, token);
        using var stream = PdfConverter.ToPdf(docStream);

        fileName = Path.GetFileNameWithoutExtension(fileName) + ".pdf";
        var pdfFile = InputFile.FromStream(stream, fileName);
        
        return pdfFile;
    }
}