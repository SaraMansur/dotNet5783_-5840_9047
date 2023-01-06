using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;

sealed  internal class DalXml :IDal
{
    public IProduct Product { get; } = new Dal.DalProduct();

    public IOrder Order { get; } = new Dal.Order();

    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();
}