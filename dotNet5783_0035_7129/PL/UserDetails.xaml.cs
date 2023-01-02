using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window
    {
        IBl bl;
        BO.Cart Cart { get; set; }
        public UserDetails(IBl bl1)
        {
            Cart = new BO.Cart();
             bl = bl1;
            InitializeComponent();
        }

        private void OrderW_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Text.Length == 0 || UserEmail.Text.Length == 0 || UserAdress.Text.Length == 0)
            {
                MessageBox.Show("You have to input the all details");
                return;
            }
            bool isExistTab = UserEmail.Text.Contains(' ');//checks if email is correct and hasn't a tab there.
            if (isExistTab)//if the email has tab-throw exception
            {
                MessageBox.Show("The mail cannot contain a tab");
                return;
            }


            bool isExistShtrudel = UserEmail.Text.Contains('@');//checks if email is correct and has the @ in their.
            if (!isExistShtrudel)//if the email hasn't @-throw exception
            {
                MessageBox.Show("The mail must contain a @");
                return;
            }
            if(UserEmail.Text[0] == '@' || UserEmail.Text[UserEmail.Text.Length - 1] == '@')
            {
                MessageBox.Show("The @ must cannot be in the first or the last place");
                return;
            }
            BO.Cart c = new BO.Cart()
            {
                Items = new List<BO.OrderItem?>(),
                CustomerName = UserName.Text,
                CustomerAdress = UserAdress.Text,
                CustomerEmail = UserEmail.Text
            };
            Cart = c;
            NewOrder newOrder = new NewOrder(bl,Cart);
            this.Close();
            newOrder.ShowDialog();
        }
    }
}
