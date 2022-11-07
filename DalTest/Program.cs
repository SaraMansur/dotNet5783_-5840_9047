// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using Microsoft.VisualBasic;
using System.Collections;

Console.WriteLine("Hello, World!");
static void Main(string[] args)
{

    DalOrder order = new DalOrder();
    DalOrderItem item = new DalOrderItem();
    DalProduct product = new DalProduct();
    string? anstr, ansBstr, name, email, address ,str;
    int ans,id; 
    char ansB;
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
                        str = Console.ReadLine(); int? InStock = int.Parse(str);
                        str = Console.ReadLine(); double price = double.Parse(str);
                        name = Console.ReadLine();
                        Product p = new Product();
                        p.m_Price = price; p.m_ID = id; p.m_Name = name; p.m_InStock = InStock;
                        product.Add(p);
                        break;
                    case 'b':
                        break;
                    case 'c':
                        break;
                    case 'd':
                        break;
                    case 'e':
                        break;
                }
                break;
            case 2:
                ansBstr = Console.ReadLine();
                ansB = char.Parse(anstr);
                Order O = new Order();
                switch (ansB)
                {
                    case 'a':
                        Console.WriteLine("please enter id, your name, email,and adress.\n");
                        str = Console.ReadLine();  id = int.Parse(str);
                        name = Console.ReadLine(); 
                        email = Console.ReadLine(); 
                        address = Console.ReadLine(); 
                        O.m_ID=id;
                        O.m_CustomerName=name;
                        O.m_CustomerAdress=address;
                        O.m_CustomerEmail=email;
                        O.m_OrderTime = DateTime.Now;
                        O.m_ShipDate = DateTime.MinValue;
                        O.m_DeliveryrDate = DateTime.MinValue;
                        try { Console.WriteLine(order.Add(O)); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;

                    case 'b':
                        Console.WriteLine("please enter id.\n");
                        str = Console.ReadLine();  id = int.Parse(str);
                        try { Console.WriteLine(order.GetbyID(id)); }
                        catch (Exception e) { Console.WriteLine(e + "\n"); }
                        break;

                    case 'c':
                        IEnumerable Ie = product.GetArray();
                        IEnumerator enumerator = Ie.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            O = (Order)enumerator.Current;
                            Console.WriteLine();
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
                        Console.WriteLine("please enter id.\n");
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

                        break;
                    case 'b':
                        break;
                    case 'c':
                        break;
                    case 'd':
                        break;
                    case 'e':
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
    } while (ans != 0);

}
