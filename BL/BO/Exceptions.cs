
namespace BO;

[Serializable]
public class IncorrectDetails : Exception //פרטי הקונה שגויים
{
    public override string Message => "The product missing in stock";
    public override string ToString()
    {
        return Message;
    }
}

public class MissingInOrderItems : Exception
{
    public override string Message => "The object product already exist";
    public override string ToString()
    {
        return Message;
    }
}

public class IncorrectQuantity : Exception //כמותת שלילית של המלאי או שאין מספיק מוצרים במלאי כלומר שגיאה בכמות
{
    public override string Message => "The object product already exist";
    public override string ToString()
    {
        return Message;
    }
}