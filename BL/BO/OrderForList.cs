using DO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BO;

public class OrderForList /*: INotifyPropertyChanged*/
{
    /// <summary>
    /// the id of the order
    /// </summary>
    public int m_Id { get; set; }
    /// <summary>
    /// the name of the customrt
    /// </summary>
    public string? m_CustomerName { get; set; }
    /// <summary>
    /// the status of the odder
    /// </summary>
    public Enums.Status? m_OrderStatus { get; set; }
        //set
        //{
        //    try { m_OrderStatus = value; }
        //    catch(Exception e) {  }
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs("Status")); 
        //} 
    //}
    
    /// <summary>
    /// the amount of the items in the order
    /// </summary>
    public int m_AmountItems { get; set; }
    /// <summary>
    /// the tota price of the order
    /// </summary>
    public double m_TotalPrice { get; set; }

    //public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// this function print all the attributes of the order to the manager screem
    /// </summary>
    /// <returns></returns>
    public override string ToString() => ToStringExtension.ToStringProperty(this);
}
