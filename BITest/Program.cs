using BlApi;
using BlImplementation;

namespace BITest;

internal class Program
{
    static void Main(string[] args)
    {
        int ans;
        do
        {
            Console.WriteLine("To check the Cart entity, type-1");
            Console.WriteLine("To check the Order entity, type-2");
            Console.WriteLine("To check the Product entity, type-3");
            ans = int.Parse(Console.ReadLine());
            try
            {
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
            }
            catch (Exception e) { Console.WriteLine(e + "\n"); }
        } while (ans!=0);
    }

    // the function tests the cart entity
    static void cartTesting()
    {
        IBl bl = new Bl();
        BO.Cart C = new BO.Cart();
        Console.WriteLine("a.Option to add an item to the cart");
        Console.WriteLine("b.Option to update the amount");
        Console.WriteLine("c.Option to confirm order");
        switch (Console.ReadLine())
        {
            case "a":
                Console.WriteLine("Enter please name,mail,adress,id of the cart");
                C.m_CustomerName = Console.ReadLine();
                C.m_CustomerMail = Console.ReadLine();
                C.m_CustomerAdress= Console.ReadLine();
                bl.Cart.AddItemToCart(C,int.Parse(Console.ReadLine()));
                break;
            case "b":
                Console.WriteLine("Enter please id,amount of the cart");
                bl.Cart.UpdateAmount(C, int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
                break;
            case "c":
                Console.WriteLine("Enter please name,mail,adress of the cart");
                bl.Cart.OrderConfirmation(C, Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }

    // the function tests the order entity
    static void orderTesting()
    {
        IBl bl = new Bl();
        BO.Order O= new BO.Order();
        Console.WriteLine("a.Option to receive a list of orders");
        Console.WriteLine("b.Option to receive order details");
        Console.WriteLine("c.Option to send an order");
        Console.WriteLine("d.Option to update order shipping");
        Console.WriteLine("e.Option to see Order tracking");
        switch (Console.ReadLine())
        {
            case "a":
                foreach (var item in bl.Order.OrderList())
                {
                    Console.WriteLine(item + "\n");
                }
                break;
            case "b":
                Console.WriteLine("Enter please id of the order");
                bl.Order.orderDetails(int.Parse(Console.ReadLine()));
                break;
            case "c":
                Console.WriteLine("Enter please id of the order");
                bl.Order.sendingAnInvitation(int.Parse(Console.ReadLine()));
                break;
            case "d":
                Console.WriteLine("Enter please id of the order");
                bl.Order.orderDelivery(int.Parse(Console.ReadLine()));
                break;
            case "e":
                Console.WriteLine("Enter please id of the order");
                bl.Order.orderTracking(int.Parse(Console.ReadLine()));
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }

    // the function tests the product entity
    static void productTesting()
    {
        IBl bl = new Bl();
        BO.Product p = new BO.Product();
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
                foreach (var item in bl.Product.ProductList())
                {
                    Console.WriteLine(item + "\n");
                }
                break;
            case "b":
                Console.WriteLine("Enter please id of the  product");
                bl.Product.ProductId(int.Parse(Console.ReadLine()));
                break;
            case "c":
                Console.WriteLine("Enter please id,category,instock,price,name of the product");
                p.m_Id = int.Parse(Console.ReadLine());
                p.m_Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), Console.ReadLine());
                p.m_InStock = int.Parse(Console.ReadLine());
                p.m_Price = double.Parse(Console.ReadLine());
                p.m_Name = Console.ReadLine();
                bl.Product.AddProduct(p);
                break;
            case "d":
                Console.WriteLine("Enter please id,category,instock,price,name of the product");
                p.m_Id = int.Parse(Console.ReadLine());
                p.m_Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), Console.ReadLine());
                p.m_InStock = int.Parse(Console.ReadLine());
                p.m_Price = double.Parse(Console.ReadLine());
                p.m_Name = Console.ReadLine();
                bl.Product.UpdateProduct(p);
                break;
            case "e":
                Console.WriteLine("Enter id of the  product");
                bl.Product.DeleteProduct(int.Parse(Console.ReadLine()));
                break;
            case "f":
                foreach (var item in bl.Product.CatalogList())
                {
                    Console.WriteLine(item + "\n");
                }
                break;
            case "g":
                Console.WriteLine("Enter id of the  product");
                bl.Product.CatalogProductId(int.Parse(Console.ReadLine()));
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
}

