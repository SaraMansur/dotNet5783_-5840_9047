using BlApi;
using DalApi;

namespace BlImplementation;

internal class Product:IProduct
{
    private IDal Dal = new DalList();
}
