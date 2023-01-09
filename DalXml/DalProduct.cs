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
    string dir = @"..\xml\";

    public int P { get; private set; }

    public DalProduct()
    {
        if (!File.Exists(dir+productPath))
            CreateFiles();
        else
            LoadData();
    }
    private void CreateFiles()
    {
        productRoot = new XElement("Product");
        productRoot.Save(dir+productPath);
    }

    private void LoadData()
    {
        try
        {
            productRoot = XElement.Load(dir + productPath);
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
                    new XElement("m_ID", p.m_ID),
                   new XElement("m_Name", p.m_Name),
                   new XElement("m_InStock", p.m_InStock),
                   new XElement("m_Price", p.m_Price),
                   new XElement("m_Category", p.m_Category)
                    );

        productRoot = new XElement("Product", v);

        productRoot.Save(dir+productPath);
    }
    public int Add(Product? P)
    {
        var p = Get().FirstOrDefault(x => x?.m_ID == P?.m_ID);
        if (p != null)
            throw new AlreadyExist();
        XElement ID = new XElement("m_ID", P?.m_ID);
        XElement Name = new XElement("m_Name", P?.m_Name);
        XElement Category = new XElement("m_Category", P?.m_Category);
        XElement InStock = new XElement("m_InStock", P?.m_InStock);
        XElement Price = new XElement("m_Price", P?.m_Price);
        productRoot.Add(new XElement("Product", ID, Name, Category, InStock, Price));
        productRoot.Save(dir + productPath);
        return (int)P?.m_ID; ;
    }

    public void Delete(int? ID)
    {
        var product = Get().FirstOrDefault(x => x?.m_ID == ID);
        if (product == null)
            throw new NotExist();
        XElement productElement;
        try
        {
            productElement = (from p in productRoot.Elements()
                              where Convert.ToInt32(p.Element("m_ID").Value) == ID
                              select p).FirstOrDefault();
            productElement.Remove();
            productRoot.Save(dir + productPath);
            return;
        }
        catch
        {
            return;
        }
    }

    public IEnumerable<Product?> Get(Func<Product?, bool>? func = null)
    {

        LoadData();
        List<Product> products=new List<Product>();
        try
        {

            products = (from p in productRoot.Elements()
                        select new Product()
                        {
                            m_ID = Convert.ToInt32(p.Element("m_ID").Value),
                            m_Name = p.Element("m_Name")!.Value,
                            m_InStock = Convert.ToInt32(p.Element("m_InStock").Value),
                            m_Price = Convert.ToInt32(p.Element("m_Price").Value),
                            m_Category = (DO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), (string)p.Element("m_Category")!)

                        }).ToList();

            if (func != null)
                products?.Where(p=>func(p)).ToList();
        }
        catch
        {
            products = null;
        }
        return products.Cast<DO.Product?>();
    
    }

    public Product? GetSingle(Func<Product?, bool>? func)
    {
        LoadData();
        Product product; Product? produ;
        try
        {
            product = (from p in productRoot.Elements()
                       select new Product()
                       {
                           m_ID = Convert.ToInt32(p.Element("m_ID").Value),
                           m_Name = p.Element("m_Name")!.Value,
                           m_InStock = Convert.ToInt32(p.Element("m_InStock").Value),
                           m_Price = Convert.ToInt32(p.Element("m_Price").Value),
                           m_Category = (DO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), (string)p.Element("m_Category")!)
                       }).FirstOrDefault(p=>func(p)) ;
            produ = (Product?)product;
        }

        catch
        {
            produ = null;
        }
        produ = produ ?? throw new NotExist();
        return produ;
    }

    public void Update(Product? P)
    {
        var product = Get().FirstOrDefault(x => x?.m_ID == P?.m_ID);
        if (product == null)
            throw new NotExist();
        XElement studentElement = (from p in productRoot.Elements()
                                   where Convert.ToInt32(p.Element("m_ID").Value) == P?.m_ID
                                   select p).FirstOrDefault();

        studentElement!.Element("m_Name")!.Value = P?.m_Name;
        studentElement!.Element("m_Category")!.Value = P?.m_Category.ToString();
        studentElement!.Element("m_InStock")!.Value = P?.m_InStock.ToString();
        studentElement!.Element("m_Price")!.Value = P?.m_Price.ToString();

        productRoot.Save(dir + productPath);
    }
 
}
