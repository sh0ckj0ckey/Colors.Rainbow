using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Colors.Rainbow.Converters
{
    public class Hex2ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string hex = value as string;
                return GetSolidColorBrush(hex);
            }
            catch { }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

        private static SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);

            bool existAlpha = hex.Length == 8 || hex.Length == 4;
            bool isDoubleHex = hex.Length == 8 || hex.Length == 6;

            if (!existAlpha && hex.Length != 6 && hex.Length != 3)
            {
                throw new ArgumentException();
            }

            int n = 0;
            byte a;
            int hexCount = isDoubleHex ? 2 : 1;
            if (existAlpha)
            {
                n = hexCount;
                a = (byte)ConvertHexToByte(hex, 0, hexCount);
                if (!isDoubleHex)
                {
                    a = (byte)(a * 16 + a);
                }
            }
            else
            {
                a = 0xFF;
            }

            var r = (byte)ConvertHexToByte(hex, n, hexCount);
            var g = (byte)ConvertHexToByte(hex, n + hexCount, hexCount);
            var b = (byte)ConvertHexToByte(hex, n + 2 * hexCount, hexCount);
            if (!isDoubleHex)
            {
                r = (byte)(r * 16 + r);
                g = (byte)(g * 16 + g);
                b = (byte)(b * 16 + b);
            }

            return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
        }

        private static uint ConvertHexToByte(string hex, int n, int count)
        {
            return System.Convert.ToUInt32(hex.Substring(n, count), 16);
        }
    }
}
