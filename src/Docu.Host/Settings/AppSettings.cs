using System.Runtime.InteropServices;

namespace Docu.Host.Settings;

[StructLayout(LayoutKind.Auto)]
public struct AppSettings
{
    public FileSettings FileSettings { get; init; }
    
    public TelegramSettings TelegramSettings { get; init; }
}