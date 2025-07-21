using Discord;

namespace Clyde2;

public class Message
{
    public string Content { get; private set; }
    public IUser Author { get; private set; }
    public bool IsBotAuthor { get; private set; }

    public Message(IUser author, string content, bool isBotAuthor)
    {
        Content = content;
        Author = author;
        IsBotAuthor = isBotAuthor;
    }
}
