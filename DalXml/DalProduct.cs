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


}
