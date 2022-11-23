
namespace DalApi;
/// <summary>
/// Throw in case the ID number is wrong
/// </summary>
[Serializable]
public class MissingID : Exception
{
    public override string Message => "The object does not exist";
    public override string ToString()
    {
        return Message;
    }
}

/// <summary>
/// Throw in case the object already exists
/// </summary>
public class DuplicateID : Exception
{
    public override string Message => "The object already exist";
    public override string ToString()
    {
        return Message;
    }
}
