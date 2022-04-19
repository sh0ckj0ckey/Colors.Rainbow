using SeeColors_UWP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace SeeColors.ViewModel
{
    public class RainbowViewModel
    {
        private static readonly Lazy<RainbowViewModel> lazy = new Lazy<RainbowViewModel>(() => new RainbowViewModel());
        public static RainbowViewModel Instance { get { return lazy.Value; } }

        ObservableCollection<WindowsColor> vWindowsColors = new ObservableCollection<WindowsColor>();
        ObservableCollection<List<AndroidColor>> vAndroidColors = new ObservableCollection<List<AndroidColor>>();

        public RainbowViewModel()
        {
            try
            {
                InitWindowsRainbow();
                InitAndroidRainbow();
            }
            catch { }
        }

        public void InitWindowsRainbow()
        {
            try
            {
                Type t = typeof(Colors);
                PropertyInfo[] properties = t.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    string method = "get_" + property.Name;
                    MethodInfo get_method = t.GetMethod(method);
                    Color getColor = (Color)get_method.Invoke(this, null);
                    WindowsColor ec = new WindowsColor
                    {
                        Name = property.Name,
                        RValue = getColor.R,
                        GValue = getColor.G,
                        BValue = getColor.B,
                        Hex = getColor.ToString(),
                        VisualColor = getColor
                    };
                    vWindowsColors.Add(ec);
                }
            }
            catch { }
        }

        public void InitAndroidRainbow()
        {
            try
            {
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Red", "#F44336"),
                    new AndroidColor("500", "#F44336"),
                    new AndroidColor("50", "#FFEBEE"),
                    new AndroidColor("100", "#FFCDD2"),
                    new AndroidColor("200", "#EF9A9A"),
                    new AndroidColor("300", "#E57373"),
                    new AndroidColor("400", "#EF5350"),
                    new AndroidColor("500", "#F44336"),
                    new AndroidColor("600", "#E53935"),
                    new AndroidColor("700", "#D32F2F"),
                    new AndroidColor("800", "#C62828"),
                    new AndroidColor("900", "#B71C1C"),
                    new AndroidColor("A100", "#FF8A80"),
                    new AndroidColor("A200", "#FF5252"),
                    new AndroidColor("A400", "#FF1744"),
                    new AndroidColor("A700", "#D50000") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Pink", "#E91E63"),
                    new AndroidColor("500", "#E91E63"),
                    new AndroidColor("50", "#FCE4EC"),
                    new AndroidColor("100", "#F8BBD0"),
                    new AndroidColor("200", "#F48FB1"),
                    new AndroidColor("300", "#F06292"),
                    new AndroidColor("400", "#EC407A"),
                    new AndroidColor("500", "#E91E63"),
                    new AndroidColor("600", "#D81B60"),
                    new AndroidColor("700", "#C2185B"),
                    new AndroidColor("800", "#AD1457"),
                    new AndroidColor("900", "#880E4F"),
                    new AndroidColor("A100", "#FF80AB"),
                    new AndroidColor("A200", "#FF4081"),
                    new AndroidColor("A400", "#F50057"),
                    new AndroidColor("A700", "#C51162") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Purple", "#9C27B0"),
                    new AndroidColor("500", "#9C27B0"),
                    new AndroidColor("50", "#F3E5F5"),
                    new AndroidColor("100", "#E1BEE7"),
                    new AndroidColor("200", "#CE93D8"),
                    new AndroidColor("300", "#BA68C8"),
                    new AndroidColor("400", "#AB47BC"),
                    new AndroidColor("500", "#9C27B0"),
                    new AndroidColor("600", "#8E24AA"),
                    new AndroidColor("700", "#7B1FA2"),
                    new AndroidColor("800", "#6A1B9A"),
                    new AndroidColor("900", "#4A148C"),
                    new AndroidColor("A100", "#EA80FC"),
                    new AndroidColor("A200", "#E040FB"),
                    new AndroidColor("A400", "#D500F9"),
                    new AndroidColor("A700", "#AA00FF") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Deep Purple", "#673AB7"),
                    new AndroidColor("500", "#673AB7"),
                    new AndroidColor("50", "#EDE7F6"),
                    new AndroidColor("100", "#D1C4E9"),
                    new AndroidColor("200", "#B39DDB"),
                    new AndroidColor("300", "#9575CD"),
                    new AndroidColor("400", "#7E57C2"),
                    new AndroidColor("500", "#673AB7"),
                    new AndroidColor("600", "#5E35B1"),
                    new AndroidColor("700", "#512DA8"),
                    new AndroidColor("800", "#4527A0"),
                    new AndroidColor("900", "#311B92"),
                    new AndroidColor("A100", "#B388FF"),
                    new AndroidColor("A200", "#7C4DFF"),
                    new AndroidColor("A400", "#651FFF"),
                    new AndroidColor("A700", "#6200EA") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Indigo", "#3F51B5"),
                    new AndroidColor("500", "#3F51B5"),
                    new AndroidColor("50", "#E8EAF6"),
                    new AndroidColor("100", "#C5CAE9"),
                    new AndroidColor("200", "#9FA8DA"),
                    new AndroidColor("300", "#7986CB"),
                    new AndroidColor("400", "#5C6BC0"),
                    new AndroidColor("500", "#3F51B5"),
                    new AndroidColor("600", "#3949AB"),
                    new AndroidColor("700", "#303F9F"),
                    new AndroidColor("800", "#283593"),
                    new AndroidColor("900", "#1A237E"),
                    new AndroidColor("A100", "#8C9EFF"),
                    new AndroidColor("A200", "#536DFE"),
                    new AndroidColor("A400", "#3D5AFE"),
                    new AndroidColor("A700", "#304FFE")});
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Blue", "#2196F3"),
                    new AndroidColor("500", "#2196F3"),
                    new AndroidColor("50", "#E3F2FD"),
                    new AndroidColor("100", "#BBDEFB"),
                    new AndroidColor("200", "#90CAF9"),
                    new AndroidColor("300", "#64B5F6"),
                    new AndroidColor("400", "#42A5F5"),
                    new AndroidColor("500", "#2196F3"),
                    new AndroidColor("600", "#1E88E5"),
                    new AndroidColor("700", "#1976D2"),
                    new AndroidColor("800", "#1565C0"),
                    new AndroidColor("900", "#0D47A1"),
                    new AndroidColor("A100", "#82B1FF"),
                    new AndroidColor("A200", "#448AFF"),
                    new AndroidColor("A400", "#2979FF"),
                    new AndroidColor("A700", "#2962FF") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Light Blue", "#03A9F4"),
                    new AndroidColor("500", "#03A9F4"),
                    new AndroidColor("50", "#E1F5FE"),
                    new AndroidColor("100", "#B3E5FC"),
                    new AndroidColor("200", "#81D4FA"),
                    new AndroidColor("300", "#4FC3F7"),
                    new AndroidColor("400", "#29B6F6"),
                    new AndroidColor("500", "#03A9F4"),
                    new AndroidColor("600", "#039BE5"),
                    new AndroidColor("700", "#0288D1"),
                    new AndroidColor("800", "#0277BD"),
                    new AndroidColor("900", "#01579B"),
                    new AndroidColor("A100", "#80D8FF"),
                    new AndroidColor("A200", "#40C4FF"),
                    new AndroidColor("A400", "#00B0FF"),
                    new AndroidColor("A700", "#0091EA") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Cyan", "#00BCD4"),
                    new AndroidColor("500", "#00BCD4"),
                    new AndroidColor("50", "#E0F7FA"),
                    new AndroidColor("100", "#B2EBF2"),
                    new AndroidColor("200", "#80DEEA"),
                    new AndroidColor("300", "#4DD0E1"),
                    new AndroidColor("400", "#26C6DA"),
                    new AndroidColor("500", "#00BCD4"),
                    new AndroidColor("600", "#00ACC1"),
                    new AndroidColor("700", "#0097A7"),
                    new AndroidColor("800", "#00838F"),
                    new AndroidColor("900", "#006064"),
                    new AndroidColor("A100", "#84FFFF"),
                    new AndroidColor("A200", "#18FFFF"),
                    new AndroidColor("A400", "#00E5FF"),
                    new AndroidColor("A700", "#00B8D4") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Teal", "#009688"),
                    new AndroidColor("500", "#009688"),
                    new AndroidColor("50", "#E0F2F1"),
                    new AndroidColor("100", "#B2DFDB"),
                    new AndroidColor("200", "#80CBC4"),
                    new AndroidColor("300", "#4DB6AC"),
                    new AndroidColor("400", "#26A69A"),
                    new AndroidColor("500", "#009688"),
                    new AndroidColor("600", "#00897B"),
                    new AndroidColor("700", "#00796B"),
                    new AndroidColor("800", "#00695C"),
                    new AndroidColor("900", "#004D40"),
                    new AndroidColor("A100", "#A7FFEB"),
                    new AndroidColor("A200", "#64FFDA"),
                    new AndroidColor("A400", "#1DE9B6"),
                    new AndroidColor("A700", "#00BFA5") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Green", "#4CAF50"),
                    new AndroidColor("500", "#4CAF50"),
                    new AndroidColor("50", "#E8F5E9"),
                    new AndroidColor("100", "#C8E6C9"),
                    new AndroidColor("200", "#A5D6A7"),
                    new AndroidColor("300", "#81C784"),
                    new AndroidColor("400", "#66BB6A"),
                    new AndroidColor("500", "#4CAF50"),
                    new AndroidColor("600", "#43A047"),
                    new AndroidColor("700", "#388E3C"),
                    new AndroidColor("800", "#2E7D32"),
                    new AndroidColor("900", "#1B5E20"),
                    new AndroidColor("A100", "#B9F6CA"),
                    new AndroidColor("A200", "#69F0AE"),
                    new AndroidColor("A400", "#00E676"),
                    new AndroidColor("A700", "#00C853") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Light Green", "#8BC34A"),
                    new AndroidColor("500", "#8BC34A"),
                    new AndroidColor("50", "#F1F8E9"),
                    new AndroidColor("100", "#DCEDC8"),
                    new AndroidColor("200", "#C5E1A5"),
                    new AndroidColor("300", "#AED581"),
                    new AndroidColor("400", "#9CCC65"),
                    new AndroidColor("500", "#8BC34A"),
                    new AndroidColor("600", "#7CB342"),
                    new AndroidColor("700", "#689F38"),
                    new AndroidColor("800", "#558B2F"),
                    new AndroidColor("900", "#33691E"),
                    new AndroidColor("A100", "#CCFF90"),
                    new AndroidColor("A200", "#B2FF59"),
                    new AndroidColor("A400", "#76FF03"),
                    new AndroidColor("A700", "#64DD17") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Lime", "#CDDC39"),
                    new AndroidColor("500", "#CDDC39"),
                    new AndroidColor("50", "#F9FBE7"),
                    new AndroidColor("100", "#F0F4C3"),
                    new AndroidColor("200", "#E6EE9C"),
                    new AndroidColor("300", "#DCE775"),
                    new AndroidColor("400", "#D4E157"),
                    new AndroidColor("500", "#CDDC39"),
                    new AndroidColor("600", "#C0CA33"),
                    new AndroidColor("700", "#AFB42B"),
                    new AndroidColor("800", "#9E9D24"),
                    new AndroidColor("900", "#827717"),
                    new AndroidColor("A100", "#F4FF81"),
                    new AndroidColor("A200", "#EEFF41"),
                    new AndroidColor("A400", "#C6FF00"),
                    new AndroidColor("A700", "#AEEA00") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Yellow", "#FFEB3B"),
                    new AndroidColor("500", "#FFEB3B"),
                    new AndroidColor("50", "#FFFDE7"),
                    new AndroidColor("100", "#FFF9C4"),
                    new AndroidColor("200", "#FFF59D"),
                    new AndroidColor("300", "#FFF176"),
                    new AndroidColor("400", "#FFEE58"),
                    new AndroidColor("500", "#FFEB3B"),
                    new AndroidColor("600", "#FDD835"),
                    new AndroidColor("700", "#FBC02D"),
                    new AndroidColor("800", "#F9A825"),
                    new AndroidColor("900", "#F57F17"),
                    new AndroidColor("A100", "#FFFF8D"),
                    new AndroidColor("A200", "#FFFF00"),
                    new AndroidColor("A400", "#FFEA00"),
                    new AndroidColor("A700", "#FFD600") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Amber", "#FFC107"),
                    new AndroidColor("500", "#FFC107"),
                    new AndroidColor("50", "#FFF8E1"),
                    new AndroidColor("100", "#FFECB3"),
                    new AndroidColor("200", "#FFE082"),
                    new AndroidColor("300", "#FFD54F"),
                    new AndroidColor("400", "#FFCA28"),
                    new AndroidColor("500", "#FFC107"),
                    new AndroidColor("600", "#FFB300"),
                    new AndroidColor("700", "#FFA000"),
                    new AndroidColor("800", "#FF8F00"),
                    new AndroidColor("900", "#FF6F00"),
                    new AndroidColor("A100", "#FFE57F"),
                    new AndroidColor("A200", "#FFD740"),
                    new AndroidColor("A400", "#FFC400"),
                    new AndroidColor("A700", "#FFAB00") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Orange", "#FF9800"),
                    new AndroidColor("500", "#FF9800"),
                    new AndroidColor("50", "#FFF3E0"),
                    new AndroidColor("100", "#FFE0B2"),
                    new AndroidColor("200", "#FFCC80"),
                    new AndroidColor("300", "#FFB74D"),
                    new AndroidColor("400", "#FFA726"),
                    new AndroidColor("500", "#FF9800"),
                    new AndroidColor("600", "#FB8C00"),
                    new AndroidColor("700", "#F57C00"),
                    new AndroidColor("800", "#EF6C00"),
                    new AndroidColor("900", "#E65100"),
                    new AndroidColor("A100", "#FFD180"),
                    new AndroidColor("A200", "#FFAB40"),
                    new AndroidColor("A400", "#FF9100"),
                    new AndroidColor("A700", "#FF6D00") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Deep Orange", "#FF5722"),
                    new AndroidColor("500", "#FF5722"),
                    new AndroidColor("50", "#FBE9E7"),
                    new AndroidColor("100", "#FFCCBC"),
                    new AndroidColor("200", "#FFAB91"),
                    new AndroidColor("300", "#FF8A65"),
                    new AndroidColor("400", "#FF7043"),
                    new AndroidColor("500", "#FF5722"),
                    new AndroidColor("600", "#F4511E"),
                    new AndroidColor("700", "#E64A19"),
                    new AndroidColor("800", "#D84315"),
                    new AndroidColor("900", "#BF360C"),
                    new AndroidColor("A100", "#FF9E80"),
                    new AndroidColor("A200", "#FF6E40"),
                    new AndroidColor("A400", "#FF3D00"),
                    new AndroidColor("A700", "#DD2C00") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Brown", "#795548"),
                    new AndroidColor("500", "#795548"),
                    new AndroidColor("50", "#EFEBE9"),
                    new AndroidColor("100", "#D7CCC8"),
                    new AndroidColor("200", "#BCAAA4"),
                    new AndroidColor("300", "#A1887F"),
                    new AndroidColor("400", "#8D6E63"),
                    new AndroidColor("500", "#795548"),
                    new AndroidColor("600", "#6D4C41"),
                    new AndroidColor("700", "#5D4037"),
                    new AndroidColor("800", "#4E342E"),
                    new AndroidColor("900", "#3E2723") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Grey", "#9E9E9E"),
                    new AndroidColor("500", "#9E9E9E"),
                    new AndroidColor("50", "#FAFAFA"),
                    new AndroidColor("100", "#F5F5F5"),
                    new AndroidColor("200", "#EEEEEE"),
                    new AndroidColor("300", "#E0E0E0"),
                    new AndroidColor("400", "#BDBDBD"),
                    new AndroidColor("500", "#9E9E9E"),
                    new AndroidColor("600", "#757575"),
                    new AndroidColor("700", "#616161"),
                    new AndroidColor("800", "#424242"),
                    new AndroidColor("900", "#212121") });
                vAndroidColors.Add(new List<AndroidColor> {
                    new AndroidColor("Blue Grey", "#607D8B"),
                    new AndroidColor("500", "#607D8B"),
                    new AndroidColor("50", "#ECEFF1"),
                    new AndroidColor("100", "#CFD8DC"),
                    new AndroidColor("200", "#B0BEC5"),
                    new AndroidColor("300", "#90A4AE"),
                    new AndroidColor("400", "#78909C"),
                    new AndroidColor("500", "#607D8B"),
                    new AndroidColor("600", "#546E7A"),
                    new AndroidColor("700", "#455A64"),
                    new AndroidColor("800", "#37474F"),
                    new AndroidColor("900", "#263238") });
            }
            catch { }
        }
    }
}
