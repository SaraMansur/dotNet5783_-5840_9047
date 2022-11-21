
namespace BO;

public class OrderTracking
{
    public int? m_ID;
    public Enums.Status m_Status;
    public List<string>? m_DescriptionProgress;
    public override string ToString() => $@"
    order id: {m_ID}
    order status: {m_Status}
    Package progress: {string.Join('\n', m_DescriptionProgress)}   
    ";
}
