using System.Collections;
using Discord;
using Discord.WebSocket;

namespace Clyde2;

public class Logger : IHookProvider
{
    private Logger() { }
    private static readonly Lazy<Logger> _singleton = new(() => new Logger());
    public static Logger Instance => _singleton.Value;

    public void ProvideHook(DiscordSocketClient client)
    {
        client.Log += TriageLogMessage;
    }

    public Task DisplayException(Exception exception)
    {
        Console.WriteLine($"<!> {exception.Message} <!>");
        if (!string.IsNullOrEmpty(exception.StackTrace))
        {
            Console.WriteLine($"Stack Trace:\n{exception.StackTrace}");
        }
        return Task.CompletedTask;
    }

    public async Task TriageLogMessage(LogMessage logMessage)
    {
        Console.ResetColor();
        if (logMessage.Exception is not null)
        {
            Console.WriteLine($"<!> [{logMessage.Source}]: An exception occurred <!>");
            await DisplayException(logMessage.Exception);
            return;
        }

        var consoleColor = logMessage.Severity switch
        {
            LogSeverity.Critical => ConsoleColor.DarkRed,
            LogSeverity.Error => ConsoleColor.Red,
            LogSeverity.Warning => ConsoleColor.DarkYellow,
            _ => ConsoleColor.Black
        };

        Console.BackgroundColor = consoleColor;
        Console.WriteLine($"[{logMessage.Source}]: {logMessage.Message}");
    }
}
