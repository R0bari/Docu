using Telegram.Bot;

namespace Docu.Host.Extensions;

public static class TelegramBotClientExtensions
{
    public static async Task<MemoryStream> DownloadFileToStream(
        this ITelegramBotClient client,
        string fileId,
        CancellationToken token)
    {
        var fileInfo = await client.GetFile(fileId, token);
        var docStream = new MemoryStream();
        await client.DownloadFile(fileInfo, docStream, token);

        return docStream;
    }
}