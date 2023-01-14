using BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace PL;
public class usersXml
{
    XElement UsersRoot;
    static string UsersPath = @"Users.xml";
    static string dir = @"..\xml\";
    public usersXml()
    {
        if (!File.Exists(dir + UsersPath))
            CreateFiles();
        else
            LoadData();
    }
    private void CreateFiles()
    {
        UsersRoot = new XElement("Product");
        UsersRoot.Save(dir + UsersPath);
    }

    private void LoadData()
    {
        try
        {
            UsersRoot = XElement.Load(dir + UsersPath);
        }
        catch
        {
            throw new Exception("File upload problem");
        }
    }
    public bool isExsist(Users U)
    {
        XElement user = (from u in UsersRoot.Elements()
                         where (u.Element("Name").Value == U.Name && u.Element("Password").Value == U.Password)
                         select u).FirstOrDefault();
        if (user == null)
            return false;
        return true;
    }
}
