using DO;
using System.ComponentModel;

namespace BO;

public class Product : INotifyPropertyChanged
{
    /// <summary>
    /// the id of the product
    /// </summary>
    private int Id;
    public int m_Id 
    {
        get
        { return Id; }
        set
        {
            Id = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }

    }
    /// <summary>
    /// the name of the product
    /// </summary>
    private string? Name;
    public string? m_Name 
    {
        get
        { return Name; }
        set
        {
            Name = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
    }
    /// <summary>
    /// the price of the product
    /// </summary>
    private double Price;
    public double m_Price 
    {
        get
        { return Price; }
        set
        {
            Price = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Price "));
            }
        }
    }
    /// <summary>
    /// the category (type) of the product
    /// </summary>
    private Enums.Category? Category;
    public Enums.Category? m_Category 
    {
        get
        { return Category; }
        set
        {
            Category = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Category"));
            }
        }
    }
    /// <summary>
    /// the amount of the product in the stock
    /// </summary>
    private int InStock;
    public int m_InStock 
    {
        get
        { return InStock; }
        set
        {
            InStock = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("InStock"));
            }
        }
    }
    /// <summary>
    /// This function will print all the features of the product
    /// </summary>
    /// <returns></returns>
    private string imageSource;

    public string m_ImageSource
    {
        get { return imageSource; }
        set { imageSource = value; }
    }
    public override string ToString() => ToStringExtension.ToStringProperty(this);

    public event PropertyChangedEventHandler PropertyChanged;

}
