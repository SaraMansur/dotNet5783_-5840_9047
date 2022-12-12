
namespace BO;
/// <summary>
/// A throw in the event that one or more of the data is incorrect
/// </summary>
public class IlegalInput : Exception
{
    public override string Message => "The input is ilegal";
    public override string ToString()
    {
        return Message;
    }
}

/// <summary>
/// A throw in case the object not existing
/// </summary>
public class NotExist : Exception
{
    public override string Message => "not existing object";
    public override string ToString()
    {
        return Message;
    }
}

/// <summary>
/// Throw in case the object already exists
/// </summary>

public class AlreadyExist : Exception
{
    public override string Message => "already existing object";
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

/// <summary>
/// Throwing in the state of Faild Getting
/// </summary>
public class FaildGetting : Exception
{
    public FaildGetting(Exception inner) : base("faild getting", inner) { }
    
    public object Massage { get; }

    public override string ToString() => $@"Faild getting - {this.InnerException}";
    
}

/// <summary>
/// Throwing in the state of Faild Adding
/// </summary>
public class FaildAdding : Exception
{
    public FaildAdding(Exception inner):base("faild adding", inner) { }
    public object Massage { get; }
    public override string ToString() => $@"Faild adding - {this.InnerException}";
 
}

/// <summary>
/// Throwing in the state of Faild Delete
/// </summary>
public class FaildDelete : Exception
{
    public FaildDelete(Exception inner) : base("faild delete", inner) { }
    public object Massage { get; }
    public override string ToString() => $@"Faild delete - {this.InnerException}";

}

/// <summary>
/// Throwing in the state of Faild Updating
/// </summary>
public class FaildUpdating : Exception
{
    public FaildUpdating(Exception inner) : base("faild updating", inner) { }
    public object Massage { get; }
    public override string ToString() => $@"Faild updating - {this.InnerException}";
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