﻿namespace Api.Injections.Filters;

[Serializable]
internal class UserAlreadyExistException : Exception
{
    public UserAlreadyExistException()
    {
    }

    public UserAlreadyExistException(string? message) : base(message)
    {
    }

    public UserAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected UserAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
