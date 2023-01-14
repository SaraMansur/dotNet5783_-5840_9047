
// See https://aka.ms/new-console-template for more information
using DalApi;
using DO;
using System;
using static DO.Enums;


namespace DalList
{
    partial class program
    {
        private static DalApi.IDal? d = DalApi.Factory.Get();
        static void Main(string[] args)
        {
            Product p = new Product();
            Order O = new Order();
            OrderItem OI = new OrderItem();
            int ans=0,x; bool f=true; double pri; string op;
            do
            {
                Console.WriteLine("To check the Product entity, type-1");
                Console.WriteLine("To check the Order entity, type-2");
                Console.WriteLine("To check the OrderItem entity, type-3");
                do
                {
                    try
                    {
                        f = int.TryParse(Console.ReadLine(), out ans);
                        if (!f)
                            throw new Exception("the input illegal");
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                } while (!f);
                if (ans == 1 || ans == 2 || ans == 3)
                {
                    Console.WriteLine("Which option do you want to check?");
                    Console.WriteLine("a.Option to add an object to an entity's list.");
                    Console.WriteLine("b.option to display Object by ID.");
                    Console.WriteLine("c.Entity list view option.");
                    Console.WriteLine("d.Option to update object data.");
                    Console.WriteLine("e.Option to delete an object from an entity's list.");
                    if (ans == 3)
                    {
                        Console.WriteLine("f.Option to receive a list of private orders based on the IDorder.");
                        Console.WriteLine("g.Option to display object by order and product.");
                    }

                }

               try
                {
                    switch (ans)
                    {
                        
                        case 0:
                            break;
                        case 1:
                            switch (Console.ReadLine())
                            {
                                case "a":
                                    Console.WriteLine("please enter id,category ,InStock,price,name of the product.");
                                    f = int.TryParse(Console.ReadLine()!,out x);
                                    if(!f)
                                        throw new Exception("the input illegal");
                                    p.m_ID = x;
                                    p.m_Category = (Category)Enum.Parse(typeof(Category), Console.ReadLine()!);
                                    f = int.TryParse(Console.ReadLine()!,out x);
                                        throw new Exception("the input illegal");
                                    p.m_InStock = x;
                                    f= double.TryParse(Console.ReadLine()!, out pri);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    p.m_Price = pri;
                                    p.m_Name = Console.ReadLine();
                                    Console.WriteLine(d?.Product.Add(p));
                                    break;
                                case "b":
                                    Console.WriteLine("please enter ID of the product.");
                                    f = int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");  
                                    Console.WriteLine(d?.Product.GetSingle(y => y?.m_ID == x));
                                    break;
                                case "c":
                                    IEnumerable<Product?> Ie = d.Product.Get();
                                    IEnumerator<Product?> enumerator = Ie.GetEnumerator();
                                    while (enumerator.MoveNext())
                                    {
                                        p = (Product)enumerator.Current;
                                        Console.WriteLine(p + "\n");
                                    }
                                    break;
                                case "d":
                                    Console.WriteLine("please enter id,category,inStock,price,name of the product.");
                                    f = int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    p.m_ID = x;
                                    p.m_Category = (Category)Enum.Parse(typeof(Category), Console.ReadLine()!);
                                    f = int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    p.m_InStock = x;
                                    f = double.TryParse(Console.ReadLine()!, out pri);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    p.m_Price = pri;
                                    p.m_Name = Console.ReadLine();
                                    d?.Product.Update(p);
                                    break;
                                case "e":
                                    Console.WriteLine("please enter ID of the product.");
                                    d?.Product.Delete(int.Parse(Console.ReadLine()!));
                                    break;
                            }
                            break;
                        case 2:
                            switch (Console.ReadLine())
                            {
                                case "a":
                                    Console.WriteLine("please enter your name, email,and adress.");
                                    O.m_CustomerName = Console.ReadLine();
                                    O.m_CustomerEmail = Console.ReadLine(); 
                                    O.m_CustomerAdress = Console.ReadLine(); 
                                    O.m_OrderTime = DateTime.Now;
                                    O.m_ShipDate = DateTime.MinValue;
                                    O.m_DeliveryrDate = DateTime.MinValue;
                                    Console.WriteLine(d.Order.Add(O));
                                    break;

                                case "b":
                                    Console.WriteLine("please enter ID of the order.");
                                    f= int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    Console.WriteLine(d?.Order.GetSingle(y => y?.m_ID == x));
                                    break;

                                case "c":
                                    IEnumerable<Order?> Ie = d.Order.Get();
                                    IEnumerator<Order?> enumerator = Ie.GetEnumerator();
                                    while (enumerator.MoveNext())
                                    {
                                        O = (Order)enumerator.Current;
                                        Console.WriteLine(O + "\n");
                                    }
                                    break;

                                case "d":
                                    Console.WriteLine("please enter id, your name, email,and adress.");
                                    f = int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    O.m_ID = x;
                                    O.m_CustomerName = Console.ReadLine();
                                    O.m_CustomerEmail = Console.ReadLine(); 
                                    O.m_CustomerAdress = Console.ReadLine(); 
                                    O.m_OrderTime = DateTime.Now;
                                    O.m_ShipDate = DateTime.MinValue;
                                    O.m_DeliveryrDate = DateTime.MinValue;
                                    d.Order.Update(O);
                                    break;

                                case "e":
                                    Console.WriteLine("please enter ID of the order.");
                                    d?.Order.Delete(int.Parse(Console.ReadLine()!));
                                    break;
                            }
                            break;

                        case 3:
                            switch (Console.ReadLine())
                            {
                                case "a":
                                    Console.WriteLine("please enter productId, orderId, price, amount.");
                                    f = int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    OI.m_ProductId = x;
                                    f = int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    OI.m_OrderId = x;
                                    f = double.TryParse(Console.ReadLine()!, out pri);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    OI.m_Price = pri;
                                    f = int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    OI.m_amount = x;
                                    Console.WriteLine(d?.OrderItem.Add(OI));
                                    break;

                                case "b":
                                    Console.WriteLine("please enter ID of the orderItem.");
                                    f = int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    Console.WriteLine(d?.OrderItem.GetSingle(y => y?.m_ID == x));
                                    break;

                                case "c":
                                    IEnumerable<OrderItem?> Ie = d.OrderItem.Get();
                                    IEnumerator<OrderItem?> enumerator = Ie.GetEnumerator();
                                    while (enumerator.MoveNext())
                                    {
                                        OI = (OrderItem)enumerator.Current;
                                        Console.WriteLine(OI + "\n");
                                    }
                                    break;

                                case "d":
                                    Console.WriteLine("please enter orderItemId, productId, orderId, price, amount.");

                                    d.OrderItem.Update(OI);
                                    break;

                                case "e":
                                    Console.WriteLine("please enter ID of the orderItem.");
                                    d.OrderItem.Delete(int.Parse(Console.ReadLine()!));
                                    break;

                                case "f":
                                    Console.WriteLine("please enter ID of the order.");
                                    f = int.TryParse(Console.ReadLine()!, out x);
                                    if (!f)
                                        throw new Exception("the input illegal");
                                    IEnumerable<OrderItem?> Ien = d.OrderItem.Get(y => y?.m_OrderId ==x);
                                    IEnumerator<OrderItem?> enumerato = Ien.GetEnumerator();
                                    while (enumerato.MoveNext())
                                    {
                                        OI = (OrderItem)enumerato.Current;
                                        Console.WriteLine(OI + "\n");
                                    }
                                    break;

                                case "g":
                                    Console.WriteLine("please enter ID of product and order.");
                                    int? idProduct = int.Parse(Console.ReadLine()!);
                                    int? idOrder = int.Parse(Console.ReadLine()!);
                                    Console.WriteLine(d.OrderItem.GetSingle(x => x?.m_ProductId== idProduct && x?.m_OrderId== idOrder));
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
                catch (Exception e) { Console.WriteLine(e + "\n"); }
            } while (ans != 0);
        }

    }
}
