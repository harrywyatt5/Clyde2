namespace Clyde2;

public class Program
{
    public static async Task Main(string[] args)
    {
        var bot = new DiscordBot();

        await bot.StartAndBlock();
    }
}
