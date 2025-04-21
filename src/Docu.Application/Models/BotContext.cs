using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Docu.Application.Models;

public sealed record BotContext
{
    public ITelegramBotClient Client  { get; }

    public ReceiverOptions ReceiverOptions  { get; }
    
    public string[] AllowedExtensions { get; }
    
    public BotContext(string telegramToken, string[] allowedExtensions)
    {
        Client = new TelegramBotClient(telegramToken);
        ReceiverOptions = new ReceiverOptions();
        AllowedExtensions = allowedExtensions;
    }

}