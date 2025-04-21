using Docu.Application;
using Docu.Host.Extensions;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Docu.Host.Command;

public sealed class CommandHandler(CommandContext context)
{
    private readonly TelegramBotClient _client = context.Client;
    private readonly ReceiverOptions _receiverOptions = context.ReceiverOptions;
    private readonly string[] _allowedExtensions = context.AllowedExtensions;

    public void StartHandling() =>
        _client.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            _receiverOptions);

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        if (update.Message is not { } message)
            return;

        var chatId = message.Chat.Id;

        if (message.Document is not { } document)
        {
            await _client.SendMessage(
                chatId: chatId,
                text: "Прикрепите файл",
                cancellationToken: token);

            return;
        }

        if (!_allowedExtensions.Contains(Path.GetExtension(document.FileName)))
        {
            await _client.SendMessage(
                chatId: chatId,
                text: "Недопустимый формат документа",
                cancellationToken: token);

            return;
        }

        using var docStream = await _client.DownloadFileToStream(document.FileId, token);
        using var stream = PdfConverter.ToPdf(docStream);

        var fileName = Path.GetFileNameWithoutExtension(document.FileName) + ".pdf";
        var inputFile = InputFile.FromStream(stream, fileName);

        await _client.SendDocument(chatId, inputFile, cancellationToken: token);
    }

    private static Task HandleErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine($"Ошибка: {exception.Message}");

        return Task.CompletedTask;
    }
}