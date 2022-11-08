// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using System.Collections;
using static DO.Enums;


namespace DalList
{
    partial class program
    {
        static void Main(string[] args)
        {

            DalOrder order = new DalOrder();
            DalOrderItem item = new DalOrderItem();
            DalProduct product = new DalProduct();
            Product p = new Product();
            Order O = new Order();
            OrderItem OI = new OrderItem();
            int ans;

            do
            {
                Console.WriteLine("To check the Product entity, type-1");
                Console.WriteLine("To check the Order entity, type-2");
                Console.WriteLine("To check the OrderItem entity, type-3");
                ans = int.Parse(Console.ReadLine());
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
                                    p.m_ID = int.Parse(Console.ReadLine());
                                    p.m_Category = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                                    p.m_InStock = int.Parse(Console.ReadLine());
                                    p.m_Price = double.Parse(Console.ReadLine());
                                    p.m_Name = Console.ReadLine();
                                    Console.WriteLine(product.Add(p));
                                    break;
                                case "b":
                                    Console.WriteLine("please enter ID of the product.");
                                    Console.WriteLine(product.GetbyID(int.Parse(Console.ReadLine())));
                                    break;
                                case "c":
                                    IEnumerable Ie = product.GetArray();
                                    IEnumerator enumerator = Ie.GetEnumerator();
                                    while (enumerator.MoveNext())
                                    {
                                        p = (Product)enumerator.Current;
                                        Console.WriteLine(p + "\n");
                                    }
                                    break;
                                case "d":
                                    Console.WriteLine("please enter id,category,inStock,price,name of the product.");
                                    p.m_ID = int.Parse(Console.ReadLine());
                                    p.m_Category = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                                    p.m_InStock = int.Parse(Console.ReadLine());
                                    p.m_Price = double.Parse(Console.ReadLine());
                                    p.m_Name = Console.ReadLine();
                                    product.Update(p);
                                    break;
                                case "e":
                                    Console.WriteLine("please enter ID of the product.");
                                    product.Delete(int.Parse(Console.ReadLine()));
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
                                    Console.WriteLine(order.Add(O));
                                    break;

                                case "b":
                                    Console.WriteLine("please enter ID of the order.");
                                    Console.WriteLine(order.GetbyID(int.Parse(Console.ReadLine())));
                                    break;

                                case "c":
                                    IEnumerable Ie = order.GetArray();
                                    IEnumerator enumerator = Ie.GetEnumerator();
                                    while (enumerator.MoveNext())
                                    {
                                        O = (Order)enumerator.Current;
                                        Console.WriteLine(O + "\n");
                                    }
                                    break;

                                case "d":
                                    Console.WriteLine("please enter id, your name, email,and adress.");
                                    O.m_ID = int.Parse(Console.ReadLine());
                                    O.m_CustomerName = Console.ReadLine();
                                    O.m_CustomerEmail = Console.ReadLine(); 
                                    O.m_CustomerAdress = Console.ReadLine(); 
                                    O.m_OrderTime = DateTime.Now;
                                    O.m_ShipDate = DateTime.MinValue;
                                    O.m_DeliveryrDate = DateTime.MinValue;
                                    order.Update(O);
                                    break;

                                case "e":
                                    Console.WriteLine("please enter ID of the order.");
                                    order.Delete(int.Parse(Console.ReadLine()));
                                    break;
                            }
                            break;

                        case 3:
                            switch (Console.ReadLine())
                            {
                                case "a":
                                    Console.WriteLine("please enter productId, orderId, price, amount.");
                                    OI.m_ProductId = int.Parse(Console.ReadLine());
                                    OI.m_OrderId = int.Parse(Console.ReadLine());
                                    OI.m_Price = double.Parse(Console.ReadLine());
                                    OI.m_amount = int.Parse(Console.ReadLine());
                                    Console.WriteLine(item.Add(OI));
                                    break;

                                case "b":
                                    Console.WriteLine("please enter ID of the orderItem.");
                                    Console.WriteLine(item.GetbyID(int.Parse(Console.ReadLine())));
                                    break;

                                case "c":
                                    IEnumerable Ie = item.GetArray();
                                    IEnumerator enumerator = Ie.GetEnumerator();
                                    while (enumerator.MoveNext())
                                    {
                                        OI = (OrderItem)enumerator.Current;
                                        Console.WriteLine(OI + "\n");
                                    }
                                    break;

                                case "d":
                                    Console.WriteLine("please enter orderItemId, productId, orderId, price, amount.");
                                    OI.m_ID= int.Parse(Console.ReadLine()); ;
                                    OI.m_ProductId = int.Parse(Console.ReadLine());
                                    OI.m_OrderId = int.Parse(Console.ReadLine());
                                    OI.m_Price = double.Parse(Console.ReadLine());
                                    OI.m_amount = int.Parse(Console.ReadLine());
                                    item.Update(OI);
                                    break;

                                case "e":
                                    Console.WriteLine("please enter ID of the orderItem.");
                                    item.Delete(int.Parse(Console.ReadLine()));
                                    break;

                                case "f":
                                    Console.WriteLine("please enter ID of the order.");
                                    IEnumerable Ien = item.GetOrderItems(int.Parse(Console.ReadLine()));
                                    IEnumerator enumerato = Ien.GetEnumerator();
                                    while (enumerato.MoveNext())
                                    {
                                        O = (Order)enumerato.Current;
                                        Console.WriteLine(O + "\n");
                                    }
                                    break;

                                case "g":
                                    Console.WriteLine("please enter ID of product and order.");
                                    Console.WriteLine(item.GetbyProductAndOrder(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
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
