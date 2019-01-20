using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;

namespace Graph_Lab_3
{
    public class PointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if(((double[][])value)[0].Length == 3)
                return new PointCollection(((double[][])value).Select(p => new Point(p[0] + 300, 300 - p[1])));

            return new PointCollection(((double[][])value).Select(p => PointTransformation3D.IsometricProection(p))
                .Select(p => new Point(p[0] + 300, 300 - p[1])));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}