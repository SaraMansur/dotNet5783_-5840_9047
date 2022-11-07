// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Net;

Console.WriteLine("Hello, World!");
static void Main(string[] args)
{

    DalOrder order = new DalOrder();
    DalOrderItem item = new DalOrderItem();
    DalProduct product= new DalProduct();
    string? anstr,ansBstr, str, name, address,email;
    int ans, id;
    char ansB;
    int? productId, orderId, InStock, amount; 
    double? price;
    Product p = new Product();
    Order O =new Order();
    OrderItem OI =new OrderItem();

    do
    {
        Console.WriteLine("To check the Product entity, type-1\n");
        Console.WriteLine("To check the Order entity, type-2\n");
        Console.WriteLine("To check the OrderItem entity, type-3\n");
        anstr = Console.ReadLine();
        ans = int.Parse(anstr);
        if (ans == 1 || ans == 2 || ans == 3)
        {
            Console.WriteLine("Which option do you want to check?\n");
            Console.WriteLine("a.Option to add an object to an entity's list.\n");
            Console.WriteLine("b.option to display Object by ID.\n");
            Console.WriteLine("c.Entity list view option.\n");
            Console.WriteLine("d.Option to update object data.\n");
            Console.WriteLine("e.Option to delete an object from an entity's list.\n");
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
                ansB = char.Parse(anstr);
                switch (ansB)
                {
                    case 'a':
                        Console.WriteLine("please enter id,InStock,price,name of the product.\n");
                        str = Console.ReadLine();  id = int.Parse(str);
                        str = Console.ReadLine();  InStock = int.Parse(str);
                        str = Console.ReadLine();  price=double.Parse(str);   
                        name = Console.ReadLine();
                        p.m_Price = price; p.m_ID=id; p.m_Name = name; p.m_InStock = InStock;
                        try { Console.WriteLine(product.Add(p)); }
                        catch(Exception e) { Console.WriteLine(e  +"\n"); }   
                        break;
                    case 'b':
                        Console.WriteLine("please enter ID of the product.\n");
                        str = Console.ReadLine(); id = int.Parse(str);
                        try { Console.WriteLine(product.GetbyID(id)); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;
                    case 'c':
                        IEnumerable Ie = product.GetArray();
                        IEnumerator enumerator = Ie.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            p=(Product)enumerator.Current;
                            Console.WriteLine(p);
                        }
                        break;
                    case 'd':
                        Console.WriteLine("please enter id,InStock,price,name of the product.\n");
                        str = Console.ReadLine(); id = int.Parse(str);
                        str = Console.ReadLine(); InStock = int.Parse(str);
                        str = Console.ReadLine(); price = double.Parse(str);
                        name = Console.ReadLine();
                        p.m_Price = price; p.m_ID = id; p.m_Name = name; p.m_InStock = InStock;
                        try { product.Update(p); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;
                    case 'e':
                        Console.WriteLine("please enter ID of the product.\n");
                        str = Console.ReadLine(); id = int.Parse(str);
                        try { product.Delete(id); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;
                }
                break;

            case 2:
                ansBstr = Console.ReadLine();
                ansB = char.Parse(anstr);
                switch (ansB)
                {
                    case 'a':
                        Console.WriteLine("please enter your name, email,and adress.\n");
                        name = Console.ReadLine();
                        email = Console.ReadLine();
                        address = Console.ReadLine();
                        O.m_CustomerName = name;
                        O.m_CustomerAdress = address;
                        O.m_CustomerEmail = email;
                        O.m_OrderTime = DateTime.Now;
                        O.m_ShipDate = DateTime.MinValue;
                        O.m_DeliveryrDate = DateTime.MinValue;
                        try { Console.WriteLine(order.Add(O)); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;

                    case 'b':
                        Console.WriteLine("please enter ID of the order.\n");
                        str = Console.ReadLine(); id = int.Parse(str);
                        try { Console.WriteLine(order.GetbyID(id)); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;

                    case 'c':
                        IEnumerable Ie = product.GetArray();
                        IEnumerator enumerator = Ie.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            O = (Order)enumerator.Current;
                            Console.WriteLine(O);
                        }
                        break;

                    case 'd':
                        Console.WriteLine("please enter id, your name, email,and adress.\n");
                        str = Console.ReadLine(); id = int.Parse(str);
                        name = Console.ReadLine();
                        email = Console.ReadLine();
                        address = Console.ReadLine();
                        O.m_ID = id;
                        O.m_CustomerName = name;
                        O.m_CustomerAdress = address;
                        O.m_CustomerEmail = email;
                        O.m_OrderTime = DateTime.Now;
                        O.m_ShipDate = DateTime.MinValue;
                        O.m_DeliveryrDate = DateTime.MinValue;
                        try { order.Update(O); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;

                    case 'e':
                        Console.WriteLine("please enter ID of the order.\n");
                        str = Console.ReadLine(); id = int.Parse(str);
                        try { order.Delete(id); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;
                }
                break;

            case 3:
                ansBstr = Console.ReadLine();
                ansB = char.Parse(anstr);
                switch (ansB)
                {
                    case 'a':
                        Console.WriteLine("please enter your name, email,and adress.\n");
                        str = Console.ReadLine();    productId=int.Parse(str);
                        str = Console.ReadLine();    orderId=int.Parse(str);
                        str = Console.ReadLine();    price=double.Parse(str);  
                        str = Console.ReadLine();    amount = int.Parse(str);
                        OI.m_Price = price; OI.m_ProductId=productId; OI.m_OrderId=orderId;   OI.m_amount = amount;
                        try { Console.WriteLine(item.Add(OI)); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;
                    case 'b':
                        Console.WriteLine("please enter ID of the orderItem.\n");
                        str = Console.ReadLine(); id = int.Parse(str);
                        try { Console.WriteLine(order.GetbyID(id)); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;
                    case 'c':
                        IEnumerable Ie = order.GetArray();
                        IEnumerator enumerator = Ie.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            OI = (OrderItem)enumerator.Current;
                            Console.WriteLine(OI);
                        }
                        break;
                    case 'd':
                        Console.WriteLine("please enter your name, email,and adress.\n");
                        str = Console.ReadLine(); productId = int.Parse(str);
                        str = Console.ReadLine(); orderId = int.Parse(str);
                        str = Console.ReadLine(); price = double.Parse(str);
                        str = Console.ReadLine(); amount = int.Parse(str);
                        OI.m_Price = price; OI.m_ProductId = productId; OI.m_OrderId = orderId; OI.m_amount = amount;
                        try { item.Update(OI); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;
                    case 'e':
                        Console.WriteLine("please enter ID of the order.\n");
                        str = Console.ReadLine(); id = int.Parse(str);
                        try { item.Delete(id); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;
                    case 'f':
                        break;
                    case 'g':
                        break;
                }
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        } 
    } while (ans!=0);
}
