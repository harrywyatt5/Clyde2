using Discord;
using Discord.WebSocket;

namespace Clyde2;

public class DiscordBot
{
    private DiscordSocketClient _client;
    private Logger _logger;
    private IncomingMessageHandler _incomingMessageHandler;

    public DiscordBot()
    {
        _client = new DiscordSocketClient();
        _logger = new Logger();
        _incomingMessageHandler = new IncomingMessageHandler(255);
        _logger.ProvideHook(_client);
        _incomingMessageHandler.ProvideHook(_client);
    }

    public async Task StartAndBlock()
    {
        Console.WriteLine(Environment.GetEnvironmentVariable("DISCORD_BOT"));
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_BOT"));
        await _client.StartAsync();

        await Task.Delay(-1);
    }
}