﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>
    {
        int Add(T entity);
        void Delete(int ID);
        void Update(T entity);
        IEnumerable<T> Get();
    }
}