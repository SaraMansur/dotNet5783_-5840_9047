
namespace BO;
/// <summary>
/// A throw in the event that one or more of the data is incorrect
/// </summary>
public class incorrectData:Exception
{
    public override string Message => "The data is incorrect";
    public override string ToString()
    {
        return Message;
    }
}

/// <summary>
/// A shot in case the ID number is wrong
/// </summary>
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

/// <summary>
/// Throw in case there is not enough of an object in the inventory
/// </summary>
public class MissingInStock : Exception
{
    public override string Message => "There is not enough of the object in stock";
    public override string ToString()
    {
        return Message;
    }
}