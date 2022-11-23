
namespace DalApi;
[Serializable]


public class AlreadyExist: Exception
{
    public override string Message => "already existing object";
    public override string ToString()
    {
        return Message;
    }
}
public class NotExist : Exception
{
    public override string Message => "not existing object";
    public override string ToString()
    {
        return Message;
    }
}