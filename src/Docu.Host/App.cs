using Docu.Host.Command;
using Docu.Host.Settings;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Docu.Host;

public sealed class App
{
    private readonly TelegramBotClient _client;
    private readonly CommandHandler _commandHandler;
    private readonly ReceiverOptions _receiverOptions = new() { AllowedUpdates = [] };

    public App(AppSettings appSettings)
    {
        _client = new TelegramBotClient(appSettings.TelegramSettings.Token);
        var commandContext = new CommandContext(
            _client,
            _receiverOptions,
            appSettings.FileSettings.GetAllowedExtensions());

        _commandHandler = new CommandHandler(commandContext);
    }

    public async Task Run()
    {
        _commandHandler.StartHandling();
        
        var user = await GetMe();
        Console.WriteLine($"Бот {user.Username} запущен...");
        Console.ReadLine();
    }

    private Task<User> GetMe() =>
        _client.GetMe();
}