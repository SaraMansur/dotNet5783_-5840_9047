
namespace BO;

public class incorrectData:Exception
{
    public override string Message => "The data is incorrect";
    public override string ToString()
    {
        return Message;
    }
}
public class MissingID : Exception
{
    public override string Message => "The object does not exist";
    public override string ToString()
    {
        return Message;
    }
}
public class DuplicateID : Exception
{
    public override string Message => "The object already exist";
    public override string ToString()
    {
        return Message;
    }
}