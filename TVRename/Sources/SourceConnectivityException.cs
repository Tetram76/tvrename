namespace TVRename;

public class SourceConnectivityException : Exception
{
    public SourceConnectivityException(string message, Exception e) : base(message, e)
    {
    }

    public SourceConnectivityException(Exception ex) : this(ex.ErrorText(), ex)
    {
    }

    public SourceConnectivityException(string? message) : base(message)
    {
    }
}
