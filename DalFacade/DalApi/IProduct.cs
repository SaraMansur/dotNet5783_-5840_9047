using DO;
namespace DalApi;

public interface IProduct:ICrud<Product>
{
    /// <summary>
    /// Gets the product by product id
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public Product GetbyID(int? ID);
}
