using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Colors.Rainbow.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Windows.UI;

namespace Colors.Rainbow.ViewModel
{
    public class RainbowViewModel : ObservableObject
    {
        private static readonly Lazy<RainbowViewModel> lazy = new Lazy<RainbowViewModel>(() => new RainbowViewModel());
        public static RainbowViewModel Instance { get { return lazy.Value; } }

        public SettingsViewModel AppSettings { get; set; } = new SettingsViewModel();

        private ObservableCollection<WindowsColor> _windowsAllColors = new ObservableCollection<WindowsColor>();

        private ObservableCollection<WindowsColor> _WindowsColors = null;
        public ObservableCollection<WindowsColor> WindowsColors
        {
            get => _WindowsColors;
            set => SetProperty(ref _WindowsColors, value);
        }

        private WindowsColor _SelectedWinColor;
        public WindowsColor SelectedWinColor
        {
            get => _SelectedWinColor;
            set => SetProperty(ref _SelectedWinColor, value);
        }

        public RainbowViewModel()
        {
            try
            {
                InitRainbow();
            }
            catch { }
        }

        public void InitRainbow()
        {
            try
            {
                Type t = typeof(Windows.UI.Colors);
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
                    _windowsAllColors.Add(ec);
                }
                WindowsColors = _windowsAllColors;
            }
            catch { }
        }

        public void FilterColors(string key)
        {
            try
            {
                key = key.Trim();
                if (string.IsNullOrWhiteSpace(key))
                {
                    WindowsColors = _windowsAllColors;
                }
                else
                {
                    var filteredColor = _windowsAllColors.Where(p => p.Name.Contains(key, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    WindowsColors = new ObservableCollection<WindowsColor>(filteredColor);
                }
            }
            catch { }
        }

    }
}
