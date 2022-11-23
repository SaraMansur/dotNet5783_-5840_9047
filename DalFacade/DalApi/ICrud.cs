namespace DalApi;

public interface ICrud<T>
{
    int Add(T entity);
    void Delete(int ID);
    void Update(T entity);
    IEnumerable<T> Get();
}
