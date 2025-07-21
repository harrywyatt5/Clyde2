using Discord.WebSocket;

namespace Clyde2;

public class DiscordBotLifecycleController
{
    private DiscordSocketClient _client;

    public DiscordBotLifecycleController(DiscordSocketClient client)
    {
        _client = client;
    }

    public async Task StartEventLoopAndBlockAsync()
    {
        await _client.StartAsync();
        await Task.Delay(-1);
    }
}
