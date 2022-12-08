using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using BlImplementation; 
using BO;

namespace BlApi
{
   
    public static class Factory
    {
        public static IBl Get()
        {         
            return new Bl();
        }
    }
}
