using Docu.Application.Experts;
using Docu.Application.Models;
using Docu.Host.Settings;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
var appSettings = config.Get<AppSettings>();

var botContext = new BotContext(
    appSettings.TelegramSettings.Token,
    appSettings.FileSettings.GetAllowedExtensions());
var botExpert = new BotExpert(botContext);

await botExpert.StartHandling();