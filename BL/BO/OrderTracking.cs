namespace BO;

public class OrderTracking
{
    /// <summary>
    /// the id of the order
    /// </summary>
    public int? m_ID;
    /// <summary>
    /// the status of the order
    /// </summary>
    public Enums.Status m_Status;
    /// <summary>
    /// list that includ all the data process and small discrabtion about the status
    /// </summary>
    public List<string>? m_DescriptionProgress;
    public override string ToString() => $@"
    order id: {m_ID}
    order status: {m_Status}
    Package progress: {string.Join('\n', m_DescriptionProgress)}   
    ";
}
