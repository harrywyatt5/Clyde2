using System.Runtime.CompilerServices;
using Discord;
using Discord.WebSocket;

namespace Clyde2;

public class IncomingMessageHandler : IHookProvider
{
    private Cache<MappedThreads> _threadCache;

    public IncomingMessageHandler(int maxQueueSize)
    {
        _threadCache = new Cache<MappedThreads>(maxQueueSize);
    }

    public void ProvideHook(DiscordSocketClient client)
    {
        client.MessageReceived += HandleIncomingMessage;
    }

    public async Task HandleIncomingMessage(SocketMessage newMessage)
    {
        if (newMessage.Channel is not IThreadChannel && !newMessage.Author.IsBot)
        {
            var message = await newMessage.Channel.SendMessageAsync($"Hi, imma make a thread");
            var thread = await ((ITextChannel)newMessage.Channel).CreateThreadAsync(
                name: "Discussion Thread",
                message: message,
                autoArchiveDuration: ThreadArchiveDuration.OneHour,
                type: ThreadType.PublicThread
            );

            await thread.JoinAsync();
            await thread.SendMessageAsync("Welcome to the thread!");
        }
        else if (newMessage.Channel is IThreadChannel && !newMessage.Author.IsBot)
        {
            await newMessage.Channel.SendMessageAsync($"You just said {newMessage.Content}");
        }
    }
}