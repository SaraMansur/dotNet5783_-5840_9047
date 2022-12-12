using DO;

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// the id of the order
    /// </summary>
    public int m_ID { get; set; }

    /// <summary>
    /// the status of the order
    /// </summary>
    public Enums.Status? m_Status { get; set; }

    /// <summary>
    /// list that includ all the data process and small discrabtion about the status
    /// </summary>
    public List<Tuple<string?, DateTime?>>? m_DescriptionProgress { get; set; }

    public override string ToString() => ToStringExtension.ToStringProperty(this);
    
}
