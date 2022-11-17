using DO;
namespace DalApi;

public interface IProduct:ICrud<Product>
{
    public Product GetbyID(int? ID);
}
