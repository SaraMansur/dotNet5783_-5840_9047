using BlApi;


namespace BlImplementation;

sealed public class Bl : IBl
{
    public IBoCart Cart => new BoCart();
    public IBoOrder  Order => new BoOrder();
    public IBoProduct Product => new BoProduct();
}
