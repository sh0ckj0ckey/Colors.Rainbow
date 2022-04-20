using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace SeeColors
{
    internal class BackgroundToForegroundConverter : IValueConverter
    {
        public SolidColorBrush WhiteColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        public SolidColorBrush BlackColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string hex = value.ToString();
                var color = XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), hex);
                if (color is Color c)
                {
                    if ((c.R + c.G + c.B) / 3 > 150)
                    {
                        return BlackColor;
                    }
                }
            }
            catch (Exception)
            {
            }

            return WhiteColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
