using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL;
using BlApi;
    using BlImplementation; 
using BO;

namespace BlApi
{
    public static class BLFactory
    {
        public static IBl GetBL(string type)
        {
            switch (type)
            {
                case "Cart":
                    return new Bl.Cart();
                case "Order":
                    return new Bl();
                case "Product":
                    return new Bl();
                default:
                    return new Bl();
            }
        }
    }
}
