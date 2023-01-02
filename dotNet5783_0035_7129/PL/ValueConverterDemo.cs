using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using BlApi;
using BO;
using Tools;


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
    public class FullToVisibilityConverter:IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string boolValue= (string)value;
            if (boolValue.Length==0)
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
   
    
}
