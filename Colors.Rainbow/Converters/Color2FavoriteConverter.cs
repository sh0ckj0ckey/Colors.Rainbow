using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
using Colors.Rainbow.ViewModel;

namespace Colors.Rainbow.Converters
{
    public class Color2FavoriteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string hex = value.ToString();

                if (parameter?.ToString() == "-")
                {
                    return RainbowViewModel.Instance.CheckFavorite(hex) ? Visibility.Collapsed : Visibility.Visible;
                }
                else
                {
                    return RainbowViewModel.Instance.CheckFavorite(hex) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            catch { }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
