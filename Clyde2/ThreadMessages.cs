using System.ClientModel.Primitives;
using Discord;

namespace Clyde2;

public class ThreadMessages
{
    private IList<IUserMessage> _messages;
    private IThreadChannel _channel;

    public ThreadMessages(IUserMessage initialMessage, IThreadChannel channel)
    {
        _messages = new List<IUserMessage>() { initialMessage };
        _channel = channel;
    }
    public ThreadMessages(IList<IUserMessage> messages, IThreadChannel channel)
    {
        _messages = messages;
        _channel = channel;
    }

    public void Add(IUserMessage message) => _messages.Add(message);

    public static ThreadMessages operator +(ThreadMessages messages, IUserMessage message)
    {
        messages.Add(message);
        return messages;
    }
}
