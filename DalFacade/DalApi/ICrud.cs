using System;

namespace DalApi;

public interface ICrud<T>
{
    /// <summary>
    /// Adds an object to an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    int Add(T entity);

    /// <summary>
    /// Delete an object of an entity
    /// </summary>
    /// <param name="ID"></param>
    void Delete(int ID);

    /// <summary>
    /// update an object to an entity
    /// </summary>
    /// <param name="entity"></param>
    void Update(T entity);

    /// <summary>
    /// get an object to an entity
    /// </summary>
    /// <returns></returns>
    IEnumerable<T?> Get(Func <T?,bool>? func);
}
