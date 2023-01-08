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
    public int Add(Product? entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int? ID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product?> Get(Func<Product?, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public Product? GetSingle(Func<Product?, bool>? func)
    {
        throw new NotImplementedException();
    }

    public void Update(Product? entity)
    {
        throw new NotImplementedException();
    }
}
