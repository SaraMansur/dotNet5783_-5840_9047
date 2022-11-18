using BlApi;
using BlImplementation;
using BO;


namespace BITest;

internal class Program
{
    static void Main(string[] args)
    {
        IBl bl = new Bl();
        BO.Cart cart = new BO.Cart();
        BO.Order order = new BO.Order();
        BO.Product product = new BO.Product();  
        int ans,ID;
        do
        {
            Console.WriteLine("To check the Cart entity, type-1");
            Console.WriteLine("To check the Order entity, type-2");
            Console.WriteLine("To check the Product entity, type-3");
            ans = int.Parse(Console.ReadLine());
            switch (ans)
            {
                case 0: 
                    break;
                case 1:
                    cartTesting();
                    break;
                case 2:
                    orderTesting();
                    break;
                case 3:
                    productTesting();
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;

            }
        }while (ans!=0);
    }
    static void cartTesting()
    {
        Console.WriteLine("a.Option to add an item to the cart");
        Console.WriteLine("b.Option to update the amount");
        Console.WriteLine("c.Option to confirm order");
        switch (Console.ReadLine())
        {
            //case "a":
            //    Console.WriteLine("Enter "
            //    Console.WriteLine(Bl.Cart.AddItemToCart(cart, ID));
            //    break;
            //case "b":
            //    Console.WriteLine(Bl.Cart.UpdateAmount(cart, ID, amount));
            //    break;
            //case "c":
            //    Bl.Cart.OrderConfirmation(cart, name, mailCustomer, address);
            //    break;
            //default:
            //    Console.WriteLine("ERROR");
            //    break;
        }
    }
    static void orderTesting()
    {
        Console.WriteLine("a.Option to receive a list of orders");
        Console.WriteLine("b.Option to receive order details");
        Console.WriteLine("c.Option to send an order");
        Console.WriteLine("d.Option to update order shipping");
        Console.WriteLine("e.Option to see Order tracking");
        switch (Console.ReadLine())
        {
            case "a":
                break;
            case "b":
                break;
            case "c":
                break;
            case "d":
                break;
            case "e":
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
    static void productTesting()
    {
        Console.WriteLine("a.Option to request a list of products to the manager");
        Console.WriteLine("b.Option to request product details to the manager");
        Console.WriteLine("c.Option to add a product");
        Console.WriteLine("d.Option to update a product");
        Console.WriteLine("e.Option to delete a product");
        Console.WriteLine("f.Option to receive a catalog for the customer");
        Console.WriteLine("g.Option to receive product details for the customer");
        switch (Console.ReadLine())
        {
            case "a":
                foreach (ProductForList item in Bl.Product.ProductList())
                {
                    Console.WriteLine(item + "\n");
                }
                break;
            case "b":
                Console.WriteLine(Bl.Product.ProductId(int.Parse(Console.ReadLine())));
                break;
            case "c":
                Console.WriteLine("Enter id,name, price,category,inStock ");
                product.m_ID = int.Parse(Console.ReadLine());
                product.m_Category = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                product.m_InStock = int.Parse(Console.ReadLine());
                product.m_Price = double.Parse(Console.ReadLine());
                product.m_Name = Console.ReadLine();
                Bl.Product.AddProduct(product);
                break;
            case "d":
                break;
            case "e":
                break;
            case "f":
                break;
            case "g":
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
}
 