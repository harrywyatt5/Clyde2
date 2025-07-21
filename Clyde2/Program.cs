using Discord.WebSocket;

namespace Clyde2;

public class Program
{
    public static async Task Main(string[] args)
    {
        var discordBotConfig = new DiscordSocketConfig()
        {
            GatewayIntents = Discord.GatewayIntents.MessageContent
        };

        var discordBotBuilder = new DiscordSocketClientBuilder()
            .WithConfig(discordBotConfig)
            .WithLogger(Logger.Instance)
            .WithHook(new IncomingMessageHandler(255, new GPTAgent()))
            .WithToken(Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN"));
        var client = await discordBotBuilder.BuildAndLoginAsync();

        var lifecycle = new DiscordBotLifecycleController(client);
        await lifecycle.StartEventLoopAndBlockAsync();
    }
}
