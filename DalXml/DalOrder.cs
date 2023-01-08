using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class DalOrder : IOrder
{
    public int Add(DO.Order? entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int? ID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> Get(Func<DO.Order?, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.Order? GetSingle(Func<DO.Order?, bool>? func)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order? entity)
    {
        throw new NotImplementedException();
    }
}

