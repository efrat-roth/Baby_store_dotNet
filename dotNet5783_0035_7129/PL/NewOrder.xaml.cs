using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        BlApi.IBl? _bl;
        Cart cart=new Cart();  
        public NewOrder(BlApi.IBl bl1)
        {
            InitializeComponent();
            _bl = bl1;
            CategorySelector.DataContext = Enum.GetValues(typeof(Category));
            ObservableCollection<ProductItem?> ProductsList =
            new ObservableCollection<ProductItem?>(from p in _bl.Product.GetListOfProduct()
                                                   let productItem = _bl.Product.GetProductCustomer(p.ID, cart)
                                                   select productItem);
              //convert to observel in order to update the details
            DataContext = ProductsList;//Resets the list by products in the store
            ObservableCollection<IGrouping<BO.Category?, ProductItem>> ByCategory =//divide to groups for categories view
                new ObservableCollection<IGrouping<BO.Category?, ProductItem?>>(collection: from p in ProductsList
                                                                               group p by p.Category into g
                                                                               select g);
            ClothesBy.DataContext = new ObservableCollection<ProductItem?>(from g in ByCategory
                                                                          where g.Key == BO.Category.Clothes
                                                                          from p in g
                                                                          select p);
            ToysBy.DataContext = new ObservableCollection<ProductItem?>(from g in ByCategory
                                                                       where g.Key == BO.Category.Toys
                                                                       from p in g
                                                                       select p);
            AcceoriesBy.DataContext = new ObservableCollection<ProductItem?>(from g in ByCategory
                                                                            where g.Key == BO.Category.Accessories
                                                                            from p in g
                                                                            select p);
            BabyCarrigesBy.DataContext = new ObservableCollection<ProductItem?>(from g in ByCategory
                                                                               where g.Key == BO.Category.BabyCarriages
                                                                               from p in g
                                                                               select p);
            BottlesBy.DataContext = new ObservableCollection<ProductItem?>(from g in ByCategory
                                                                          where g.Key == BO.Category.Bottles
                                                                          from p in g
                                                                          select p);
            SocksBy.DataContext = new ObservableCollection<ProductItem?>(from g in ByCategory
                                                                        where g.Key == BO.Category.Socks
                                                                        from p in g
                                                                        select p);
            cart.Items = new List<OrderItem?>();
        }
        /// <summary>
        /// Add product to the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                cart=_bl?.Cart.AddProductToCart(cart, int.Parse(idD.Text));
                cart=_bl?.Cart.UpdateProductAmount(cart, int.Parse(idD.Text), int.Parse(amountD.Text));
                
            }
            catch(FailedGet m)
            {
                MessageBox.Show("The product is not in the store, enter the ID again");
            }
            catch(InvalidVariableException m)
            {
                MessageBox.Show("The amount is bigger than the amount in stock");
            }
            catch (CanNotDOActionException m)
            {
                MessageBox.Show("The amount is bigger than the amount in stock");
            }
        }

        private void CategroyFilter(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<ProductItem?> productItems = from p in _bl.Product.GetListOfProduct()
                                                     let productItem = _bl.Product.GetProductCustomer(p.ID, cart)
                                                     select productItem;
            if ((PL.Category)CategorySelector.SelectedItem == Category.AllProducts)//if the selected item is all products
            {
                ObservableCollection<ProductItem?> ProductsList1 =
                new ObservableCollection<ProductItem?>(productItems); //convert to observel in order to update the details
                DataContext = ProductsList1;//Resets the list by products in the store
                return;
            }
            ObservableCollection<ProductItem?> ProductsList = //convert to observel in order to update the details
            new ObservableCollection<ProductItem?>
            ((from p in _bl.Product.GetListOfProduct()
                       where p.Category == (BO.Category)CategorySelector.SelectedItem
                       let productItem = _bl.Product.GetProductCustomer(p.ID, cart)
                       select productItem));
            DataContext = ProductsList;
        }

        private void DetailsOfProduct(object sender, MouseButtonEventArgs e)
        {
            ProductItem p = (ProductItem)ProductsListView.SelectedItem;
            ProductDataBiding.ProductItem product = new ProductDataBiding.ProductItem()
            {
                ID = p.ID,
                Category = p.Category,
                Name = p.Name,
                Price = p.Price,
                Amount= p.AmountInCart,
                InStock = p.InStock,
            };
            DetailsProductWindow details = new DetailsProductWindow(_bl,product);
            details.ShowDialog();
        }

        private void ShowCart(object sender, RoutedEventArgs e)
        {
            CartWindow c = new CartWindow();
            c.ShowDialog();
        }

        private void ShowByCategory(object sender, RoutedEventArgs e)
        {

        }
        private void IdIsNumber(object sender, KeyEventArgs e)
        {
            TextBox? text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt))) return;
            e.Handled = true;
            return;

        }
        private void AmountIsNumber(object sender, KeyEventArgs e)
        {
            TextBox? text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt))) return;
            e.Handled = true;
            return;

        }
    }
}
