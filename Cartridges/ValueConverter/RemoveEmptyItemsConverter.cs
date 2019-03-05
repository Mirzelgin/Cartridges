using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace CartridgesNS.ValueConverter
{
    class RemoveEmptyItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<Model.MainLog> collect = new ObservableCollection<Model.MainLog>();

            foreach (var item in ((ObservableCollection<Model.MainLog>)value))
            {
                if (!String.IsNullOrEmpty(item.Note)) collect.Add(item);
            }

            return collect;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
