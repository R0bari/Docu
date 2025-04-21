using System.Runtime.InteropServices;

namespace Docu.Host.Settings;

[StructLayout(LayoutKind.Auto)]
public readonly struct FileSettings
{
    public readonly string AllowedExtensions { get; init; }

    public string[] GetAllowedExtensions() =>
        AllowedExtensions
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(ext => ext.StartsWith('.') ? ext.ToLower() : "." + ext.ToLower())
            .ToArray();
}