﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Ad2_Dashboard
{
    public class StringToLowercaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
