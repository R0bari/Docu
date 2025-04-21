using Docu.Application.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Docu.Application.Experts;

public sealed class BotExpert(BotContext context)
{
    public async Task StartHandling()
    {
        context.Client.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            context.ReceiverOptions);
        
        var user = await context.Client.GetMe();
        Console.WriteLine($"Бот {user.Username} запущен...");
        Console.ReadLine();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message is not { } message)
            return;

        var chatExpert = new TelegramChatExpert(client, message.Chat.Id, token);
        
        if (message.Document is not { } document)
        {
            await chatExpert.SendMessage("Прикрепите файл");
            return;
        }

        if (!context.AllowedExtensions.Contains(Path.GetExtension(document.FileName)))
        {
            await chatExpert.SendMessage("Недопустимый формат документа");
            return;
        }

        var pdfExpert = new PdfExpert(client, token);
        var pdfFile = await pdfExpert.Convert(document.FileId, document.FileName ?? "converted");

        await chatExpert.SendDocument(pdfFile);
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