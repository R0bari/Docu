using Docu.Host;
using Docu.Host.Settings;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
var appSettings = config.Get<AppSettings>();

var bot = new MainBot(appSettings);
bot.Start();

var user = await bot.GetMe();
Console.WriteLine($"Бот {user.Username} запущен...");
Console.ReadLine();