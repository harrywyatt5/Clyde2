using Discord;
using Discord.WebSocket;

namespace Clyde2;

public class DiscordSocketClientBuilder
{
    private DiscordSocketConfig? _config;
    private string? _auth;
    private List<IHookProvider> _hooks;
    private IHookProvider? _logger;

    public DiscordSocketClientBuilder()
    {
        _hooks = new List<IHookProvider>();
    }

    public DiscordSocketClientBuilder WithLogger(IHookProvider logger)
    {
        _logger = logger;
        return this;
    }

    public DiscordSocketClientBuilder WithHook(IHookProvider hook)
    {
        _hooks.Add(hook);
        return this;
    }

    public DiscordSocketClientBuilder WithToken(string? token)
    {
        _auth = token;
        return this;
    }

    public DiscordSocketClientBuilder WithConfig(DiscordSocketConfig? config)
    {
        _config = config;
        return this;
    }

    private void ValidateParams()
    {
        if (_logger is null) throw new ArgumentNullException(nameof(_logger));
        if (string.IsNullOrEmpty(_auth)) throw new ArgumentNullException(nameof(_auth));
    }

    public async Task<DiscordSocketClient> BuildAndLoginAsync()
    {
        ValidateParams();
        var discordClient = new DiscordSocketClient(_config ?? new DiscordSocketConfig());

        _logger!.ProvideHook(discordClient);
        foreach (IHookProvider provider in _hooks)
        {
            provider.ProvideHook(discordClient);
        }

        await discordClient.LoginAsync(TokenType.Bot, _auth);
        return discordClient;
    }
}
