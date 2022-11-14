using DO;
namespace DalApi;

public interface IProduct:ICrud<Product>
{
    Product GetbyID(int? ID);
}
