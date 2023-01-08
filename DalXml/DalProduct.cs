using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class DalProduct : IProduct
{
    XElement productRoot;
    string productPath = @"Product.xml";

    public int P { get; private set; }

    public DalProduct()
    {
        if (!File.Exists(productPath))
            CreateFiles();
        else
            LoadData();
    }
    private void CreateFiles()
    {
        productRoot = new XElement("Product");
        productRoot.Save(productPath);
    }

    private void LoadData()
    {
        try
        {
            productRoot = XElement.Load(productPath);
        }
        catch
        {
            throw new Exception("File upload problem");
        }
    }
    public void SaveStudentListLinq(List<Product> productList)
    {
        var v = from p in productList
                select new XElement("Product",
                    new XElement("ID", p.m_ID),
                   new XElement("Name",p.m_Name),
                   new XElement("InStock", p.m_InStock),
                   new XElement("Price", p.m_Price),
                   new XElement("Category", p.m_Category)
                    );

        productRoot = new XElement("Product", v);

        productRoot.Save(productPath);
    }

    int Add(Product? P)
    {
        var p = Get().FirstOrDefault(x => x?.m_ID == P?.m_ID);
        if (p != null)
            throw new AlreadyExist();
        XElement ID = new XElement("ID", P?.m_ID);
        XElement Name = new XElement("Name", P?.m_Name);
        XElement Category = new XElement("Category", P?.m_Category);
        XElement InStock = new XElement("InStock", P?.m_InStock);
        XElement Price = new XElement("Price", P?.m_Price);
        productRoot.Add(new XElement("Product", ID, Name, Category, InStock, Price));
        productRoot.Save(productPath);
        return (int)P?.m_ID;
    }

    void Delete(int? ID)
    {
        var product = Get().FirstOrDefault(x => x?.m_ID == ID);
        if (product == null)
            throw new NotExist();
        XElement productElement;
        try
        {
            productElement = (from p in productRoot.Elements()
                              where Convert.ToInt32(p.Element("ID").Value) == ID
                              select p).FirstOrDefault();
            productElement.Remove();
            productRoot.Save(productPath);
            return;
        }
        catch
        {
            return;
        }
    }

    void Update(Product? P)
    {
        var product = Get().FirstOrDefault(x => x?.m_ID == P?.m_ID);
        if (product == null)
            throw new NotExist();
        XElement studentElement = (from p in productRoot.Elements()
                                   where Convert.ToInt32(p.Element("ID").Value) == P?.m_ID
                                   select p).FirstOrDefault();

        studentElement!.Element("Name")!.Value = P?.m_Name;
        studentElement!.Element("Category")!.Value = P?.m_Category.ToString();
        studentElement!.Element("InStock")!.Value = P?.m_InStock.ToString();
        studentElement!.Element("Price")!.Value = P?.m_Price.ToString();

        productRoot.Save(productPath);
    }

    IEnumerable<Product?> Get(Func<Product?, bool>? func)
    {
        LoadData();
        List<Product?> products;
        try
        {

            products = (from p in productRoot.Elements()
                        select new Product()
                        {
                            m_ID = Convert.ToInt32(p.Element("ID").Value),
                            m_Name = p.Element("Name").Value),
                            m_InStock = Convert.ToInt32(p.Element("InStock").Value),
                            m_Price = Convert.ToInt32(p.Element("Price").Value),
                            m_Category = (DO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), (string)p.Element("Category")!)

                        }).ToList();

            if (func != null)
                products.Where(func);
        }
        catch
        {
            products = null;
        }
        return products;
    }

    Product? GetSingle(Func<Product?, bool>? func)
    {
        LoadData();
        Product? product;
        try
        {
            product = (from p in productRoot.Elements()
                       select new Product()
                       {
                           m_ID = Convert.ToInt32(p.Element("ID").Value),
                           m_Name = p.Element("Name").Value),
                           m_InStock = Convert.ToInt32(p.Element("InStock").Value),
                           m_Price = Convert.ToInt32(p.Element("Price").Value),
                           m_Category = (DO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), (string)p.Element("Category")!)
                       }).FirstOrDefault(func);
        }
        catch
        {
            product = null;
        }
        product = product ?? throw new NotExist();
        return product;
    }
}
