using Clyde2.Exceptions;
using Discord;
using Discord.WebSocket;

namespace Clyde2;

public class IncomingMessageHandler : IHookProvider
{
    private Cache<ThreadMessages> _threadCache;
    private IAIAgent _aiAgent;
    private DiscordSocketClient? _client;

    public IncomingMessageHandler(int maxQueueSize, IAIAgent aiAgent)
    {
        _threadCache = new Cache<ThreadMessages>(maxQueueSize);
        _aiAgent = aiAgent;
        _client = null;
    }

    public void ProvideHook(DiscordSocketClient client)
    {
        _client = client;
        client.MessageReceived += HandleIncomingMessage;
    }

    private bool IsMessageFromBot(IUserMessage message)
    {
        if (_client is null) throw new NotHookedException(GetType());
        ArgumentNullException.ThrowIfNull(message);

        return message.Author.Id == _client.CurrentUser.Id;
    }

    public async Task HandleIncomingMessage(SocketMessage newMessage)
    {
        if (_client == null) throw new NotHookedException(GetType());

        if (newMessage.Channel is ITextChannel textChannel && newMessage is IUserMessage userMessage && IsMessageFromBot(userMessage))
        {
            if (newMessage.Channel is not IThreadChannel && newMessage.MentionedUsers.Contains(_client.CurrentUser))
            {
                // TODO: take content and send it to LLM. Put response here
                var responseMessage = await newMessage.Channel.SendMessageAsync("Hello world!");
                var thread = await textChannel!.CreateThreadAsync(
                    name: "Placeholder name",
                    message: responseMessage,
                    autoArchiveDuration: ThreadArchiveDuration.ThreeDays,
                    type: ThreadType.PublicThread
                );

            }
        }
    }
}
