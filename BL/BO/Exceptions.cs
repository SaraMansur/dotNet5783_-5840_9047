
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
/// A shot in case the ID number is wrong
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

public class FaildGetting : Exception
{
    public FaildGetting(Exception inner) : base("faild getting", inner) { }

    public object Massage { get;}

    public override string ToString() => $@"{Massage} - {this.InnerException}";
    
}
public class FaildAdding : Exception
{
    public FaildAdding(Exception inner):base("faild adding", inner) { }
    public object Massage { get; }
    public override string ToString() => $@"{Massage} - {this.InnerException}";
 
}

public class FaildDelete : Exception
{
    public FaildDelete(Exception inner) : base("faild delete", inner) { }
    public object Massage { get; }
    public override string ToString() => $@"{Massage} - {this.InnerException}";

}
public class FaildUpdating : Exception
{
    public FaildUpdating(Exception inner) : base("faild updating", inner) { }
    public object Massage { get; }
    public override string ToString() => $@"{Massage} - {this.InnerException}";


}