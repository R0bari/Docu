using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Docu.Host.Command;

public sealed record CommandContext(
    TelegramBotClient Client, 
    ReceiverOptions ReceiverOptions,
    string[] AllowedExtensions);