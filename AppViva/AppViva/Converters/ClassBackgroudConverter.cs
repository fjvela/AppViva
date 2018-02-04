using AppViva.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AppViva.Converters
{
    public class ClassBackgroudConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /* ClassModel classModel = (ClassModel)value;


             Color result;
             if (classModel.IsBooked)
             {
                 result = Color.Red;
             }
             else result = Color.White;


             return result;
         */
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
