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
   
    public static class BlFactory
    {
        public static IBl GetBL()
        {         
            return new Bl();
        }
    }
}
