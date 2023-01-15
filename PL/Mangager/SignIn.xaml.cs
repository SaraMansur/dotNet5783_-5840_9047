using BlApi;
using BO;
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
        BO.Users user=new Users() { Name="",Password=""};

        public SignIn()
        {
            InitializeComponent();
            this.DataContext = user;
        }
 

        private void Signin_Cilck(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user.Name == "" || user.Password == "")
                    throw new Exception("Please enter missing data");
                Users u = new Users() { Name = user.Name, Password = user.Password };
                usersXml u2 = new usersXml();
                if (u2.isExsist(u))
                {
                    new Manager(user).Show(); this.Close();
                }
                else
                    throw new Exception("The username or password is incorrect!");
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
