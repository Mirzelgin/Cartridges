using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace CartridgesNS.ValueConverter
{
    class IsNotEmptyStringsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> str = new ObservableCollection<string>();
            ObservableCollection<Model.MainLog> collect = ((ObservableCollection<Model.MainLog>)value);

            foreach (var item in collect)
            {
                if (item.Note == String.Empty) str.Add(item.Note);
            }

            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
