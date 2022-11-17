
namespace DalApi;
[Serializable]
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
