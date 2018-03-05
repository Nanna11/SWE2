using System;

public class ElementWithIdDoesNotExistException : Exception
{
    public ElementWithIdDoesNotExistException()
    {
    }

    public ElementWithIdDoesNotExistException(string message)
        : base(message)
    {
    }

    public ElementWithIdDoesNotExistException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class PathNotSetException : Exception
{
    public PathNotSetException()
    {
    }

    public PathNotSetException(string message)
        : base(message)
    {
    }

    public PathNotSetException(string message, Exception inner)
        : base(message, inner)
    {
    }
}