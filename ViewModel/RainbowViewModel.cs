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

        public ObservableCollection<WindowsColor> vWindowsColors = new ObservableCollection<WindowsColor>();
        public ObservableCollection<AndroidColor> vAndroidColors = new ObservableCollection<AndroidColor>();

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
                vAndroidColors.Add(new AndroidColor("Red", "#F44336", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#F44336"),
                    new AndroidColorInfo("50", "#FFEBEE"),
                    new AndroidColorInfo("100", "#FFCDD2"),
                    new AndroidColorInfo("200", "#EF9A9A"),
                    new AndroidColorInfo("300", "#E57373"),
                    new AndroidColorInfo("400", "#EF5350"),
                    new AndroidColorInfo("500", "#F44336"),
                    new AndroidColorInfo("600", "#E53935"),
                    new AndroidColorInfo("700", "#D32F2F"),
                    new AndroidColorInfo("800", "#C62828"),
                    new AndroidColorInfo("900", "#B71C1C"),
                    new AndroidColorInfo("A100", "#FF8A80"),
                    new AndroidColorInfo("A200", "#FF5252"),
                    new AndroidColorInfo("A400", "#FF1744"),
                    new AndroidColorInfo("A700", "#D50000") }));
                vAndroidColors.Add(new AndroidColor("Pink", "#E91E63", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#E91E63"),
                    new AndroidColorInfo("50", "#FCE4EC"),
                    new AndroidColorInfo("100", "#F8BBD0"),
                    new AndroidColorInfo("200", "#F48FB1"),
                    new AndroidColorInfo("300", "#F06292"),
                    new AndroidColorInfo("400", "#EC407A"),
                    new AndroidColorInfo("500", "#E91E63"),
                    new AndroidColorInfo("600", "#D81B60"),
                    new AndroidColorInfo("700", "#C2185B"),
                    new AndroidColorInfo("800", "#AD1457"),
                    new AndroidColorInfo("900", "#880E4F"),
                    new AndroidColorInfo("A100", "#FF80AB"),
                    new AndroidColorInfo("A200", "#FF4081"),
                    new AndroidColorInfo("A400", "#F50057"),
                    new AndroidColorInfo("A700", "#C51162") }));
                vAndroidColors.Add(new AndroidColor("Purple", "#9C27B0", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#9C27B0"),
                    new AndroidColorInfo("50", "#F3E5F5"),
                    new AndroidColorInfo("100", "#E1BEE7"),
                    new AndroidColorInfo("200", "#CE93D8"),
                    new AndroidColorInfo("300", "#BA68C8"),
                    new AndroidColorInfo("400", "#AB47BC"),
                    new AndroidColorInfo("500", "#9C27B0"),
                    new AndroidColorInfo("600", "#8E24AA"),
                    new AndroidColorInfo("700", "#7B1FA2"),
                    new AndroidColorInfo("800", "#6A1B9A"),
                    new AndroidColorInfo("900", "#4A148C"),
                    new AndroidColorInfo("A100", "#EA80FC"),
                    new AndroidColorInfo("A200", "#E040FB"),
                    new AndroidColorInfo("A400", "#D500F9"),
                    new AndroidColorInfo("A700", "#AA00FF") }));
                vAndroidColors.Add(new AndroidColor("Deep Purple", "#673AB7", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#673AB7"),
                    new AndroidColorInfo("50", "#EDE7F6"),
                    new AndroidColorInfo("100", "#D1C4E9"),
                    new AndroidColorInfo("200", "#B39DDB"),
                    new AndroidColorInfo("300", "#9575CD"),
                    new AndroidColorInfo("400", "#7E57C2"),
                    new AndroidColorInfo("500", "#673AB7"),
                    new AndroidColorInfo("600", "#5E35B1"),
                    new AndroidColorInfo("700", "#512DA8"),
                    new AndroidColorInfo("800", "#4527A0"),
                    new AndroidColorInfo("900", "#311B92"),
                    new AndroidColorInfo("A100", "#B388FF"),
                    new AndroidColorInfo("A200", "#7C4DFF"),
                    new AndroidColorInfo("A400", "#651FFF"),
                    new AndroidColorInfo("A700", "#6200EA") }));
                vAndroidColors.Add(new AndroidColor("Indigo", "#3F51B5", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#3F51B5"),
                    new AndroidColorInfo("50", "#E8EAF6"),
                    new AndroidColorInfo("100", "#C5CAE9"),
                    new AndroidColorInfo("200", "#9FA8DA"),
                    new AndroidColorInfo("300", "#7986CB"),
                    new AndroidColorInfo("400", "#5C6BC0"),
                    new AndroidColorInfo("500", "#3F51B5"),
                    new AndroidColorInfo("600", "#3949AB"),
                    new AndroidColorInfo("700", "#303F9F"),
                    new AndroidColorInfo("800", "#283593"),
                    new AndroidColorInfo("900", "#1A237E"),
                    new AndroidColorInfo("A100", "#8C9EFF"),
                    new AndroidColorInfo("A200", "#536DFE"),
                    new AndroidColorInfo("A400", "#3D5AFE"),
                    new AndroidColorInfo("A700", "#304FFE") }));
                vAndroidColors.Add(new AndroidColor("Blue", "#2196F3", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#2196F3"),
                    new AndroidColorInfo("50", "#E3F2FD"),
                    new AndroidColorInfo("100", "#BBDEFB"),
                    new AndroidColorInfo("200", "#90CAF9"),
                    new AndroidColorInfo("300", "#64B5F6"),
                    new AndroidColorInfo("400", "#42A5F5"),
                    new AndroidColorInfo("500", "#2196F3"),
                    new AndroidColorInfo("600", "#1E88E5"),
                    new AndroidColorInfo("700", "#1976D2"),
                    new AndroidColorInfo("800", "#1565C0"),
                    new AndroidColorInfo("900", "#0D47A1"),
                    new AndroidColorInfo("A100", "#82B1FF"),
                    new AndroidColorInfo("A200", "#448AFF"),
                    new AndroidColorInfo("A400", "#2979FF"),
                    new AndroidColorInfo("A700", "#2962FF") }));
                vAndroidColors.Add(new AndroidColor("Light Blue", "#03A9F4", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#03A9F4"),
                    new AndroidColorInfo("50", "#E1F5FE"),
                    new AndroidColorInfo("100", "#B3E5FC"),
                    new AndroidColorInfo("200", "#81D4FA"),
                    new AndroidColorInfo("300", "#4FC3F7"),
                    new AndroidColorInfo("400", "#29B6F6"),
                    new AndroidColorInfo("500", "#03A9F4"),
                    new AndroidColorInfo("600", "#039BE5"),
                    new AndroidColorInfo("700", "#0288D1"),
                    new AndroidColorInfo("800", "#0277BD"),
                    new AndroidColorInfo("900", "#01579B"),
                    new AndroidColorInfo("A100", "#80D8FF"),
                    new AndroidColorInfo("A200", "#40C4FF"),
                    new AndroidColorInfo("A400", "#00B0FF"),
                    new AndroidColorInfo("A700", "#0091EA") }));
                vAndroidColors.Add(new AndroidColor("Cyan", "#00BCD4", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#00BCD4"),
                    new AndroidColorInfo("50", "#E0F7FA"),
                    new AndroidColorInfo("100", "#B2EBF2"),
                    new AndroidColorInfo("200", "#80DEEA"),
                    new AndroidColorInfo("300", "#4DD0E1"),
                    new AndroidColorInfo("400", "#26C6DA"),
                    new AndroidColorInfo("500", "#00BCD4"),
                    new AndroidColorInfo("600", "#00ACC1"),
                    new AndroidColorInfo("700", "#0097A7"),
                    new AndroidColorInfo("800", "#00838F"),
                    new AndroidColorInfo("900", "#006064"),
                    new AndroidColorInfo("A100", "#84FFFF"),
                    new AndroidColorInfo("A200", "#18FFFF"),
                    new AndroidColorInfo("A400", "#00E5FF"),
                    new AndroidColorInfo("A700", "#00B8D4") }));
                vAndroidColors.Add(new AndroidColor("Teal", "#009688", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#009688"),
                    new AndroidColorInfo("50", "#E0F2F1"),
                    new AndroidColorInfo("100", "#B2DFDB"),
                    new AndroidColorInfo("200", "#80CBC4"),
                    new AndroidColorInfo("300", "#4DB6AC"),
                    new AndroidColorInfo("400", "#26A69A"),
                    new AndroidColorInfo("500", "#009688"),
                    new AndroidColorInfo("600", "#00897B"),
                    new AndroidColorInfo("700", "#00796B"),
                    new AndroidColorInfo("800", "#00695C"),
                    new AndroidColorInfo("900", "#004D40"),
                    new AndroidColorInfo("A100", "#A7FFEB"),
                    new AndroidColorInfo("A200", "#64FFDA"),
                    new AndroidColorInfo("A400", "#1DE9B6"),
                    new AndroidColorInfo("A700", "#00BFA5") }));
                vAndroidColors.Add(new AndroidColor("Green", "#4CAF50", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#4CAF50"),
                    new AndroidColorInfo("50", "#E8F5E9"),
                    new AndroidColorInfo("100", "#C8E6C9"),
                    new AndroidColorInfo("200", "#A5D6A7"),
                    new AndroidColorInfo("300", "#81C784"),
                    new AndroidColorInfo("400", "#66BB6A"),
                    new AndroidColorInfo("500", "#4CAF50"),
                    new AndroidColorInfo("600", "#43A047"),
                    new AndroidColorInfo("700", "#388E3C"),
                    new AndroidColorInfo("800", "#2E7D32"),
                    new AndroidColorInfo("900", "#1B5E20"),
                    new AndroidColorInfo("A100", "#B9F6CA"),
                    new AndroidColorInfo("A200", "#69F0AE"),
                    new AndroidColorInfo("A400", "#00E676"),
                    new AndroidColorInfo("A700", "#00C853") }));
                vAndroidColors.Add(new AndroidColor("Light Green", "#8BC34A", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#8BC34A"),
                    new AndroidColorInfo("50", "#F1F8E9"),
                    new AndroidColorInfo("100", "#DCEDC8"),
                    new AndroidColorInfo("200", "#C5E1A5"),
                    new AndroidColorInfo("300", "#AED581"),
                    new AndroidColorInfo("400", "#9CCC65"),
                    new AndroidColorInfo("500", "#8BC34A"),
                    new AndroidColorInfo("600", "#7CB342"),
                    new AndroidColorInfo("700", "#689F38"),
                    new AndroidColorInfo("800", "#558B2F"),
                    new AndroidColorInfo("900", "#33691E"),
                    new AndroidColorInfo("A100", "#CCFF90"),
                    new AndroidColorInfo("A200", "#B2FF59"),
                    new AndroidColorInfo("A400", "#76FF03"),
                    new AndroidColorInfo("A700", "#64DD17") }));
                vAndroidColors.Add(new AndroidColor("Lime", "#CDDC39", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#CDDC39"),
                    new AndroidColorInfo("50", "#F9FBE7"),
                    new AndroidColorInfo("100", "#F0F4C3"),
                    new AndroidColorInfo("200", "#E6EE9C"),
                    new AndroidColorInfo("300", "#DCE775"),
                    new AndroidColorInfo("400", "#D4E157"),
                    new AndroidColorInfo("500", "#CDDC39"),
                    new AndroidColorInfo("600", "#C0CA33"),
                    new AndroidColorInfo("700", "#AFB42B"),
                    new AndroidColorInfo("800", "#9E9D24"),
                    new AndroidColorInfo("900", "#827717"),
                    new AndroidColorInfo("A100", "#F4FF81"),
                    new AndroidColorInfo("A200", "#EEFF41"),
                    new AndroidColorInfo("A400", "#C6FF00"),
                    new AndroidColorInfo("A700", "#AEEA00") }));
                vAndroidColors.Add(new AndroidColor("Yellow", "#FFEB3B", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#FFEB3B"),
                    new AndroidColorInfo("50", "#FFFDE7"),
                    new AndroidColorInfo("100", "#FFF9C4"),
                    new AndroidColorInfo("200", "#FFF59D"),
                    new AndroidColorInfo("300", "#FFF176"),
                    new AndroidColorInfo("400", "#FFEE58"),
                    new AndroidColorInfo("500", "#FFEB3B"),
                    new AndroidColorInfo("600", "#FDD835"),
                    new AndroidColorInfo("700", "#FBC02D"),
                    new AndroidColorInfo("800", "#F9A825"),
                    new AndroidColorInfo("900", "#F57F17"),
                    new AndroidColorInfo("A100", "#FFFF8D"),
                    new AndroidColorInfo("A200", "#FFFF00"),
                    new AndroidColorInfo("A400", "#FFEA00"),
                    new AndroidColorInfo("A700", "#FFD600") }));
                vAndroidColors.Add(new AndroidColor("Amber", "#FFC107", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#FFC107"),
                    new AndroidColorInfo("50", "#FFF8E1"),
                    new AndroidColorInfo("100", "#FFECB3"),
                    new AndroidColorInfo("200", "#FFE082"),
                    new AndroidColorInfo("300", "#FFD54F"),
                    new AndroidColorInfo("400", "#FFCA28"),
                    new AndroidColorInfo("500", "#FFC107"),
                    new AndroidColorInfo("600", "#FFB300"),
                    new AndroidColorInfo("700", "#FFA000"),
                    new AndroidColorInfo("800", "#FF8F00"),
                    new AndroidColorInfo("900", "#FF6F00"),
                    new AndroidColorInfo("A100", "#FFE57F"),
                    new AndroidColorInfo("A200", "#FFD740"),
                    new AndroidColorInfo("A400", "#FFC400"),
                    new AndroidColorInfo("A700", "#FFAB00") }));
                vAndroidColors.Add(new AndroidColor("Orange", "#FF9800", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#FF9800"),
                    new AndroidColorInfo("50", "#FFF3E0"),
                    new AndroidColorInfo("100", "#FFE0B2"),
                    new AndroidColorInfo("200", "#FFCC80"),
                    new AndroidColorInfo("300", "#FFB74D"),
                    new AndroidColorInfo("400", "#FFA726"),
                    new AndroidColorInfo("500", "#FF9800"),
                    new AndroidColorInfo("600", "#FB8C00"),
                    new AndroidColorInfo("700", "#F57C00"),
                    new AndroidColorInfo("800", "#EF6C00"),
                    new AndroidColorInfo("900", "#E65100"),
                    new AndroidColorInfo("A100", "#FFD180"),
                    new AndroidColorInfo("A200", "#FFAB40"),
                    new AndroidColorInfo("A400", "#FF9100"),
                    new AndroidColorInfo("A700", "#FF6D00") }));
                vAndroidColors.Add(new AndroidColor("Deep Orange", "#FF5722", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#FF5722"),
                    new AndroidColorInfo("50", "#FBE9E7"),
                    new AndroidColorInfo("100", "#FFCCBC"),
                    new AndroidColorInfo("200", "#FFAB91"),
                    new AndroidColorInfo("300", "#FF8A65"),
                    new AndroidColorInfo("400", "#FF7043"),
                    new AndroidColorInfo("500", "#FF5722"),
                    new AndroidColorInfo("600", "#F4511E"),
                    new AndroidColorInfo("700", "#E64A19"),
                    new AndroidColorInfo("800", "#D84315"),
                    new AndroidColorInfo("900", "#BF360C"),
                    new AndroidColorInfo("A100", "#FF9E80"),
                    new AndroidColorInfo("A200", "#FF6E40"),
                    new AndroidColorInfo("A400", "#FF3D00"),
                    new AndroidColorInfo("A700", "#DD2C00") }));
                vAndroidColors.Add(new AndroidColor("Brown", "#795548", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#795548"),
                    new AndroidColorInfo("50", "#EFEBE9"),
                    new AndroidColorInfo("100", "#D7CCC8"),
                    new AndroidColorInfo("200", "#BCAAA4"),
                    new AndroidColorInfo("300", "#A1887F"),
                    new AndroidColorInfo("400", "#8D6E63"),
                    new AndroidColorInfo("500", "#795548"),
                    new AndroidColorInfo("600", "#6D4C41"),
                    new AndroidColorInfo("700", "#5D4037"),
                    new AndroidColorInfo("800", "#4E342E"),
                    new AndroidColorInfo("900", "#3E2723") }));
                vAndroidColors.Add(new AndroidColor("Grey", "#9E9E9E", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#9E9E9E"),
                    new AndroidColorInfo("50", "#FAFAFA"),
                    new AndroidColorInfo("100", "#F5F5F5"),
                    new AndroidColorInfo("200", "#EEEEEE"),
                    new AndroidColorInfo("300", "#E0E0E0"),
                    new AndroidColorInfo("400", "#BDBDBD"),
                    new AndroidColorInfo("500", "#9E9E9E"),
                    new AndroidColorInfo("600", "#757575"),
                    new AndroidColorInfo("700", "#616161"),
                    new AndroidColorInfo("800", "#424242"),
                    new AndroidColorInfo("900", "#212121") }));
                vAndroidColors.Add(new AndroidColor("Blue Grey", "#607D8B", new List<AndroidColorInfo> {
                    new AndroidColorInfo("500", "#607D8B"),
                    new AndroidColorInfo("50", "#ECEFF1"),
                    new AndroidColorInfo("100", "#CFD8DC"),
                    new AndroidColorInfo("200", "#B0BEC5"),
                    new AndroidColorInfo("300", "#90A4AE"),
                    new AndroidColorInfo("400", "#78909C"),
                    new AndroidColorInfo("500", "#607D8B"),
                    new AndroidColorInfo("600", "#546E7A"),
                    new AndroidColorInfo("700", "#455A64"),
                    new AndroidColorInfo("800", "#37474F"),
                    new AndroidColorInfo("900", "#263238") }));
            }
            catch { }
        }
    }
}
