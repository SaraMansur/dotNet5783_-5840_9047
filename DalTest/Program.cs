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
            string? anstr, ansBstr, str, name, address, email;
            int ans, id;
            char ansB;
            int? productId, orderId, InStock, amount;
            double? price;
            Category? category;
            Product p = new Product();
            Order O = new Order();
            OrderItem OI = new OrderItem();

            do
            {
                Console.WriteLine("To check the Product entity, type-1");
                Console.WriteLine("To check the Order entity, type-2");
                Console.WriteLine("To check the OrderItem entity, type-3");
                anstr = Console.ReadLine();
                ans = int.Parse(anstr);
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

                switch (ans)
                {

                    case 0:
                        break;
                    case 1:
                        ansBstr = Console.ReadLine();
                        switch (ansBstr)
                        {
                            case "a":
                                Console.WriteLine("please enter id,category ,InStock,price,name of the product.");
                                str = Console.ReadLine(); id = int.Parse(str);
                                str = Console.ReadLine(); category = (Category)Enum.Parse(typeof(Category), str);
                                str = Console.ReadLine(); InStock = int.Parse(str);
                                str = Console.ReadLine(); price = double.Parse(str);
                                name = Console.ReadLine();
                                p.m_Price = price;p.m_ID = id; p.m_Category = category; p.m_Name = name; p.m_InStock = InStock;
                                try { Console.WriteLine(product.Add(p)); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;
                            case "b":
                                Console.WriteLine("please enter ID of the product.");
                                str = Console.ReadLine(); id = int.Parse(str);
                                try { Console.WriteLine(product.GetbyID(id)); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
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
                                Console.WriteLine("please enter id,InStock,price,name of the product.");
                                str = Console.ReadLine(); id = int.Parse(str);
                                str = Console.ReadLine(); InStock = int.Parse(str);
                                str = Console.ReadLine(); price = double.Parse(str);
                                name = Console.ReadLine();
                                p.m_Price = price; p.m_ID = id; p.m_Name = name; p.m_InStock = InStock;
                                try { product.Update(p); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;
                            case "e":
                                Console.WriteLine("please enter ID of the product.");
                                str = Console.ReadLine(); id = int.Parse(str);
                                try { product.Delete(id); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;
                        }
                        break;
                    case 2:
                        ansBstr = Console.ReadLine();
                        switch (ansBstr)
                        {
                            case "a":
                                Console.WriteLine("please enter your name, email,and adress.");
                                name = Console.ReadLine(); O.m_CustomerName = name;
                                email = Console.ReadLine(); O.m_CustomerEmail = email;
                                address = Console.ReadLine(); O.m_CustomerAdress = address;
                                O.m_OrderTime = DateTime.Now;
                                O.m_ShipDate = DateTime.MinValue;
                                O.m_DeliveryrDate = DateTime.MinValue;
                                try { Console.WriteLine(order.Add(O)); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;

                            case "b":
                                Console.WriteLine("please enter ID of the order.");
                                str = Console.ReadLine(); id = int.Parse(str);
                                try { Console.WriteLine(order.GetbyID(id)); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
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
                                str = Console.ReadLine(); id = int.Parse(str); O.m_ID = id;
                                name = Console.ReadLine(); O.m_CustomerName = name;
                                email = Console.ReadLine(); O.m_CustomerEmail = email;
                                address = Console.ReadLine(); O.m_CustomerAdress = address;
                                O.m_OrderTime = DateTime.Now;
                                O.m_ShipDate = DateTime.MinValue;
                                O.m_DeliveryrDate = DateTime.MinValue;
                                try { order.Update(O); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;

                            case "e":
                                Console.WriteLine("please enter ID of the order.");
                                str = Console.ReadLine(); id = int.Parse(str);
                                try { order.Delete(id); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;
                        }
                        break;

                    case 3:
                        ansBstr = Console.ReadLine();
                        switch (ansBstr)
                        {
                            case "a":
                                Console.WriteLine("please enter productId, orderId, price, amount.");
                                str = Console.ReadLine(); productId = int.Parse(str);
                                str = Console.ReadLine(); orderId = int.Parse(str);
                                str = Console.ReadLine(); price = double.Parse(str);
                                str = Console.ReadLine(); amount = int.Parse(str);
                                OI.m_Price = price; OI.m_ProductId = productId; OI.m_OrderId = orderId; OI.m_amount = amount;
                                try { Console.WriteLine(item.Add(OI)); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;
                            case "b":
                                Console.WriteLine("please enter ID of the orderItem.");
                                str = Console.ReadLine(); id = int.Parse(str);
                                try { Console.WriteLine(item.GetbyID(id)); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
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
                                Console.WriteLine("please enter productId, orderId, price, amount.");
                                str = Console.ReadLine(); productId = int.Parse(str);
                                str = Console.ReadLine(); orderId = int.Parse(str);
                                str = Console.ReadLine(); price = double.Parse(str);
                                str = Console.ReadLine(); amount = int.Parse(str);
                                OI.m_Price = price; OI.m_ProductId = productId; OI.m_OrderId = orderId; OI.m_amount = amount;
                                try { item.Update(OI); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;
                            case "e":
                                Console.WriteLine("please enter ID of the orderItem.");
                                str = Console.ReadLine(); id = int.Parse(str);
                                try { item.Delete(id); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;
                            //Console.WriteLine("f.Option to receive a list of private orders based on the IDorder.");
                            //Console.WriteLine("g.Option to display object by order and product.");
                            case "f":
                                Console.WriteLine("please enter ID of the order.");
                                str = Console.ReadLine(); id = int.Parse(str);
                                IEnumerable Ien = item.GetOrderItems(id);
                                IEnumerator enumerato = Ien.GetEnumerator();
                                while (enumerato.MoveNext())
                                {
                                    O = (Order)enumerato.Current;
                                    Console.WriteLine(O + "\n");
                                }
                                break;
                            case "g":
                                Console.WriteLine("please enter ID of product and order.");
                                str = Console.ReadLine(); productId = int.Parse(str);
                                str = Console.ReadLine(); orderId = int.Parse(str);
                                try { item.GetbyProductAndOrder(productId, orderId); }
                                catch (Exception e) { Console.WriteLine(e + "\n"); }
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            } while (ans != 0);
        }

    }
}
