using DalApi;

namespace BlApi;

public interface IBI
{
    public IBoCart Cart { get; }
    public IBoOrder Order { get; }
    public IOrderItem orderItem { get; }
}
