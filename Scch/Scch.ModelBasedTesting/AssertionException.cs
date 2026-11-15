using System;
using System.Runtime.Serialization;

namespace Scch.ModelBasedTesting;

public class AssertionException : Exception
{
    public AssertionException()
    {
    }

    public AssertionException(string message)
    : base(message)
    {
    }

    public AssertionException(string message, Exception innerException)
    : base(message, innerException)
    {
    }

    [Obsolete("Formatter-based serialization is obsolete on .NET and is kept for API compatibility only.", DiagnosticId = "SYSLIB0051")]
    protected AssertionException(SerializationInfo info, StreamingContext context)
    : base(info, context)
    {
    }
}
