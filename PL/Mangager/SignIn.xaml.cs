using BlApi;
using PL.WpfNewOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Mangager
{ 
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        BO.Users user=new BO.Users() { Name="",Password=""};
        BO.Cart cart = new BO.Cart() { m_CustomerAdress = "", m_CustomerName = "", m_CustomerMail = "" };
        BO.Customer customer= new BO.Customer { Name="",Password=0 , m_Cart=new BO.Cart()};
        public IBl bl = Factory.Get();
        int n = 0;
        public SignIn(int num=0)
        {
            InitializeComponent();
            n = num;
            if (num == 0)
            {
                logIn.Visibility = Visibility.Collapsed; email.Visibility= Visibility.Collapsed;
                emailText.Visibility= Visibility.Collapsed; address.Visibility= Visibility.Collapsed;
                addressText.Visibility= Visibility.Collapsed; myLabel.Visibility= Visibility.Collapsed;
                this.DataContext = user;
            }
            else { this.DataContext = customer; }
        }
 

        private void Signin_Cilck(object sender, RoutedEventArgs e)
        {
            if(n==0)
            {
                try
                {
                    if (user.Name == "" || user.Password == "")
                        throw new Exception("Please enter missing data");
                    BO.Users u = new BO.Users() { Name = user.Name, Password = user.Password };
                    usersXml u2 = new usersXml();
                    if (u2.isExsist(u))
                    {
                        new Manager(user).Show(); this.Close();
                    }
                    else
                        throw new Exception("The username or password is incorrect!");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }

            else
            {
                try
                {
                    int x = customer.Password;
                    if (customer.Name == "" || !int.TryParse(userPassword.Text, out x))
                        throw new Exception("Please enter missing data");
                    customer = bl.Customer.CustomerId(customer.Name, customer.Password);
                    new createNewOrder(customer.m_Cart, customer).Show();
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

        private void logIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int x = customer.Password;
                if (customer.Name == "" || !int.TryParse(userPassword.Text, out x)|| emailText.Text==""||addressText.Text=="")
                    throw new Exception("Please enter missing data");
                customer = bl.Customer.AddCustomer(customer.Name, addressText.Text, emailText.Text, customer.Password);
                new createNewOrder(customer.m_Cart, customer).Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void Click_buttonBack(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show(); this.Close(); 
        }
    }
}
