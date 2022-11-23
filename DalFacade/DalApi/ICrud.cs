using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface ICrud<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    int Add(T entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    void Delete(int ID);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    void Update(T entity);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> Get();
}
