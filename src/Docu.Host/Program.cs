using Docu.Host;
using Docu.Host.Settings;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
var appSettings = config.Get<AppSettings>();

var app = new App(appSettings);
await app.Run();