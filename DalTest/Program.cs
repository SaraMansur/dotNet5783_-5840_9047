// See https://aka.ms/new-console-template for more information
using Dal;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");
static void Main(string[] args) 
{ 
    DalOrder order = new DalOrder();
    DalOrderItem item = new DalOrderItem();
    DalProduct profuct= new DalProduct();
    string anstr,ansBstr;
    int ans;
    char ansB;
    do
    {
        Console.WriteLine("To check the Product entity, type-1\n");
        Console.WriteLine("To check the Order entity, type-2\n");
        Console.WriteLine("To check the OrderItem entity, type-3\n");
        anstr = Console.ReadLine();
        ans=int.Parse(anstr);
        if(ans==1||ans==2||ans==3)
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
    } while (ans!=0);

}
