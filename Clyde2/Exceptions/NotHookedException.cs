namespace Clyde2.Exceptions;

public class NotHookedException : Exception
{
    public NotHookedException(Type name) : base($"{name.Name} was not hooked before use") { }
}
