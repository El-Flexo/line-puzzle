using System;
using System.ComponentModel;

public class Preconditions
{
    public static T CheckNotNull<T>(T reference, string message = "Preconditions: Null reference exception!") where T : class
    {
        if (reference == null)
            throw new NullReferenceException(message);
        return reference;
    }

    public static bool CheckState(bool expression, string message = "Preconditions: Inconsistent state!")
    {
        if (!expression)
            throw new InvalidStateException(message);
        return expression;
    }

    public static bool CheckArgument(bool expression, string message = "Preconditions: Inconsistent argument!")
    {
        if (!expression)
            throw new InvalidEnumArgumentException(message);
        return expression;
    }
}

public class InvalidStateException : Exception
{
    public InvalidStateException(string message): base(message)
    {
    }
}
