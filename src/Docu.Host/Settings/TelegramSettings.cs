using System.Runtime.InteropServices;

namespace Docu.Host.Settings;

[StructLayout(LayoutKind.Auto)]
public readonly struct TelegramSettings
{
    public string Token { get; init; }
}