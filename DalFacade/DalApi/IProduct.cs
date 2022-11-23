using DO;
namespace DalApi;

public interface IProduct:ICrud<Product>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public Product GetbyID(int? ID);
}
