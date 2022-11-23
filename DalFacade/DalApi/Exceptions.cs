
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
public class DuplicateID : Exception
public class NotExist : Exception
{
    public override string Message => "not existing object";
    public override string ToString()
    {
        return Message;
    }
}