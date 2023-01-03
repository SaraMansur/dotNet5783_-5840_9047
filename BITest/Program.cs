using BlApi;
using BlImplementation;
using BO;

namespace BITest;

internal class Program
{
    static void Main(string[] args)
    {
        BO.Cart C = new BO.Cart();
        C.m_orderItems = new List<BO.OrderItem>();
        Console.WriteLine("Are you a manager or customer?");
        if (Console.ReadLine() == "customer")
        {
            Console.WriteLine("Please enter name,adress and mail");
            C.m_CustomerName = Console.ReadLine();
            C.m_CustomerAdress = Console.ReadLine();
            C.m_CustomerMail = Console.ReadLine();
            C.m_orderItems = new List<BO.OrderItem>();
        }
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
                        C = cartTesting(C);
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

    /// <summary>
    /// the function tests the cart entity
    /// </summary>
    /// <param name="C"></param>
    /// <returns></returns>
    static BO.Cart cartTesting(BO.Cart C)
    {
        IBl bl = Factory.Get();
        //BO.Cart C = new BO.Cart();
        Console.WriteLine("a.Option to add an item to the cart");
        Console.WriteLine("b.Option to update the amount");
        Console.WriteLine("c.Option to confirm order");
        switch (Console.ReadLine())
        {
            case "a":
                Console.WriteLine("Enter please id of the product");
                Console.WriteLine(bl.Cart.AddItemToCart(C,int.Parse(Console.ReadLine())));
                break;
            case "b":
                Console.WriteLine("Enter please id,amount of the product");
                Console.WriteLine(bl.Cart.UpdateAmount(C, int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
                break;
            case "c":
                Console.WriteLine("Enter please name,mail,adress of the customer");
                bl.Cart.OrderConfirmation(C, Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
        return C;
    }

    /// <summary>
    /// the function tests the order entity
    /// </summary>
    static void orderTesting()
    {
        IBl bl = Factory.Get(); 
        BO.Order O= new BO.Order();
        Console.WriteLine("a.Option to receive a list of orders");
        Console.WriteLine("b.Option to receive order details");
        Console.WriteLine("c.Option to send an order");
        Console.WriteLine("d.Option to update order shipping");
        Console.WriteLine("e.Option to see Order tracking");
        switch (Console.ReadLine())
        {
            case "a":
                IEnumerable<BO.OrderForList> e = bl.Order.OrderList();
                foreach (var item in e)
                {
                    Console.WriteLine(item + "\n");
                }
                break;
            case "b":
                Console.WriteLine("Enter please id of the order");
                Console.WriteLine(bl.Order.orderDetails(int.Parse(Console.ReadLine())));
                //fv
                break;
            case "c":
                Console.WriteLine("Enter please id of the order");
                Console.WriteLine(bl.Order.sendingAnInvitation(int.Parse(Console.ReadLine())));
                break;
            case "d":
                Console.WriteLine("Enter please id of the order");
                Console.WriteLine(bl.Order.orderDelivery(int.Parse(Console.ReadLine())));
                break;
            case "e":
                Console.WriteLine("Enter please id of the order");
                Console.WriteLine(bl.Order.orderTracking(int.Parse(Console.ReadLine())));
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }

    /// <summary>
    /// the function tests the product entity
    /// </summary>
    static void productTesting()
    {
        IBl bl = Factory.Get();
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
                IEnumerable<BO.ProductForList> e = bl.Product.ProductList()!;
                foreach (var item in e)
                {
                    Console.WriteLine(item + "\n");
                }
                break;
            case "b":
                Console.WriteLine("Enter please id of the  product");
                Console.WriteLine(bl.Product.ProductId(int.Parse(Console.ReadLine()!)));
                break;
            case "c":
                Console.WriteLine("Enter please id,category,instock,price,name of the product");
                p.m_Id = int.Parse(Console.ReadLine()!);
                p.m_Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), Console.ReadLine()!);
                p.m_InStock = int.Parse(Console.ReadLine()!);
                p.m_Price = double.Parse(Console.ReadLine()!);
                p.m_Name = Console.ReadLine();
                bl.Product.AddProduct(p);
                break;
            case "d":
                Console.WriteLine("Enter please id,category,instock,price,name of the product");
                p.m_Id = int.Parse(Console.ReadLine()!);
                p.m_Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), Console.ReadLine()!);
                p.m_InStock = int.Parse(Console.ReadLine()!);
                p.m_Price = double.Parse(Console.ReadLine()!);
                p.m_Name = Console.ReadLine();
                bl.Product.UpdateProduct(p);
                break;
            case "e":
                Console.WriteLine("Enter id of the  product");
                bl.Product.DeleteProduct(int.Parse(Console.ReadLine()!));
                break;
            case "f":
                IEnumerable<BO.ProductItem> e2= bl.Product.CatalogList(null);
                foreach (var item in e2)
                {
                    Console.WriteLine(item + "\n");
                }
                break;
            case "g":
                Console.WriteLine("Enter id of the  product");
                Console.WriteLine(bl.Product.CatalogProductId(int.Parse(Console.ReadLine()!)));
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
}

