using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

public class XMLTools
{
    #region SaveLoadWithXMLSerializer
    static string ConfigPath = @"Config.xml";

    static string dir = @"..\xml\";
    public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
    {
        try
        {
            FileStream file = new FileStream(filePath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
        }
    }
    public static List<T> LoadListFromXMLSerializer<T>(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(filePath, FileMode.Open);
                list = (List<T>)x.Deserialize(file);
                file.Close();
                return list;
            }
            else
                return new List<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);  // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
        }
        return null;
    }
    #endregion

    public static XElement LoadData(string filePath)
    {
        try
        {
            return XElement.Load(filePath);
        }
        catch
        {
            Console.WriteLine("File upload problem");
            return null;
        }
    }
    public static int configOrderId()
    {
        XElement configRoot= XElement.Load(dir + ConfigPath);
        int id = Convert.ToInt32(configRoot.Element("IdOrder").Value);
        id++;
        configRoot.Element("IdOrder")!.SetValue(id);
        configRoot.Save(dir + ConfigPath);
        return id;
    }
    public static int configOrderItemId()
    {
        XElement configRoot = XElement.Load(dir + ConfigPath);
        int id = Convert.ToInt32(configRoot.Element("IdOrderItem").Value);
        id++;
        configRoot.Element("IdOrderItem")!.SetValue(id);
        configRoot.Save(dir + ConfigPath);
        return id;
    }

}
