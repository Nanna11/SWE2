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

public class SingletonInitializedTwiceException : Exception
{
    public SingletonInitializedTwiceException()
    {
    }

    public SingletonInitializedTwiceException(string message)
        : base(message)
    {
    }

    public SingletonInitializedTwiceException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class SingletonNotInitializedException : Exception
{
    public SingletonNotInitializedException()
    {
    }

    public SingletonNotInitializedException(string message)
        : base(message)
    {
    }

    public SingletonNotInitializedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}