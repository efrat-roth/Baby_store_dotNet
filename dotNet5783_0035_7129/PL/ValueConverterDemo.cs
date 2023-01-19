using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using BlApi;
using BO;


namespace ValueConverterDemo
{
    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (!boolValue)
            {

                return Visibility.Hidden; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {

                return Visibility.Hidden; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
    } 
    public class StatusToBackgroundConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BO.OrderStatus stingValue = (BO.OrderStatus)value;
            if (stingValue == BO.OrderStatus.ConfirmedOrder)
            {

                return "#FFDC1690";
            }
            else if (stingValue == BO.OrderStatus.DeliveredOrder)
            {
                return "#FFF74EB6";
            }
            else if (stingValue == BO.OrderStatus.ArrivedOrder)
            {
                return "#FFF3A2D4";
            }
            else
                return null;
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StatusToProgressBarConverter : IValueConverter
    {
        //convert from source property type to target property type
        private IBl bl = Factory.Get();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BO.Order order = new();
            int id = (int)value;
            try
            {
                order = bl.Order.GetDetailsOrderManager(id);
            }
            catch (BO.ObgectNullableException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            catch (BO.InvalidVariableException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (order.Status == BO.OrderStatus.ArrivedOrder)
                return 100; //the order is completed
                            //take the days that passed*10
            else if (order.Status == BO.OrderStatus.ConfirmedOrder)
                return 0;
            else
                return 50;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class FullToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string boolValue = (string)value;
            if (boolValue.Length == 0)
            {
                return Visibility.Hidden; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NullToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return Visibility.Visible; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NotNullToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return Visibility.Hidden; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NotNullAndDoneYetToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null ||
                (DateTime)value > DateTime.Today)
            {
                return Visibility.Hidden; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
    public class NullToAndDoNotDidYetVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value , Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null|| (DateTime)value > DateTime.Today)
            {
                return Visibility.Visible; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    } 
    
}

