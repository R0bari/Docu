using Docu.Host.Command;
using Docu.Host.Settings;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Docu.Host;

public sealed class MainBot
{
    private readonly TelegramBotClient _client;
    private readonly CommandHandler _commandHandler;
    private readonly ReceiverOptions _receiverOptions = new() { AllowedUpdates = [] };

    public MainBot(AppSettings appSettings)
    {
        _client = new TelegramBotClient(appSettings.TelegramSettings.Token);
        var commandContext = new CommandContext(
            _client,
            _receiverOptions,
            appSettings.FileSettings.GetAllowedExtensions());

        _commandHandler = new CommandHandler(commandContext);
    }

    public void Start() =>
        _commandHandler.StartHandling();

    public Task<User> GetMe() =>
        _client.GetMe();
}