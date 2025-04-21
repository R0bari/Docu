using Telegram.Bot;
using Telegram.Bot.Types;

namespace Docu.Application.Experts;

public sealed class TelegramChatExpert(ITelegramBotClient client, long chatId, CancellationToken token)
{
    public Task SendMessage(string text) => client.SendMessage(chatId, text, cancellationToken: token);

    public Task SendDocument(InputFile file) => client.SendDocument(chatId, file, cancellationToken: token);
}