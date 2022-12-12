
namespace DalApi;
/// <summary>
/// Throw in case the ID number is wrong
/// </summary>
[Serializable]


public class AlreadyExist: Exception
{
    public override string Message => "already existing object";
    public override string ToString()
    {
        return Message;
    }
}

/// <summary>
/// Throw in case the object already exists
/// </summary>
public class NotExist : Exception
{
    public override string Message => "not existing object";
    public override string ToString()
    {
        return Message;
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

/// <summary>
/// 
/// </summary>
public class ArgumentNull : Exception
{
    public override string Message => "The input is empty";
    public override string ToString()
    {
        return Message;
    }
}