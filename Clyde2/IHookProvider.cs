using Discord.WebSocket;

namespace Clyde2;

public interface IHookProvider
{
    public void ProvideHook(DiscordSocketClient client);
}