using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Colors.Rainbow.Helpers;
using Colors.Rainbow.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Windows.UI;

namespace Colors.Rainbow.ViewModel
{
    public class RainbowViewModel : ObservableObject
    {
        private static readonly Lazy<RainbowViewModel> lazy = new Lazy<RainbowViewModel>(() => new RainbowViewModel());
        public static RainbowViewModel Instance { get { return lazy.Value; } }


        private ObservableCollection<WindowsColor> _windowsAllColors = new ObservableCollection<WindowsColor>();


        private ObservableCollection<WindowsColor> _windowsColors = null;
        public ObservableCollection<WindowsColor> WindowsColors
        {
            get => _windowsColors;
            set => SetProperty(ref _windowsColors, value);
        }

        private WindowsColor _SelectedWinColor;
        public WindowsColor SelectedWinColor
        {
            get => _SelectedWinColor;
            set => SetProperty(ref _SelectedWinColor, value);
        }

        private HashSet<string> _favoriteColorsHex = new HashSet<string>();

        public ObservableCollection<FavoriteColor> FavoriteColors { get; } = new ObservableCollection<FavoriteColor>();

        public RainbowViewModel()
        {
            try
            {
                InitRainbow();
                InitFavorites();
            }
            catch { }
        }

        private void InitRainbow()
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
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private async void InitFavorites()
        {
            try
            {
                FavoriteColors.Clear();
                string json = await StorageHelper.ReadFileAsync("favorites");
                List<FavoriteColor> favorites = JsonSerializer.Deserialize<List<FavoriteColor>>(json);
                foreach (var item in favorites)
                {
                    FavoriteColors.Add(item);
                    _favoriteColorsHex.Add(item.Hex);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void AddFavorite(string hex)
        {
            if (!_favoriteColorsHex.Contains(hex))
            {
                FavoriteColors.Insert(0, new FavoriteColor()
                {
                    Hex = hex,
                });
                _favoriteColorsHex.Add(hex);

                SaveFavorites();
            }
        }

        public void RemoveFavorite(string hex)
        {
            FavoriteColor remove = null;
            foreach (var favorite in FavoriteColors)
            {
                if (favorite.Hex == hex)
                {
                    remove = favorite;
                }
            }

            if (remove != null)
            {
                FavoriteColors.Remove(remove);
                _favoriteColorsHex.Remove(remove.Hex);
                SaveFavorites();
            }
        }

        public bool CheckFavorite(string hex)
        {
            return _favoriteColorsHex.Contains(hex);
        }

        private async void SaveFavorites()
        {
            try
            {
                var favorites = FavoriteColors.ToList();
                string json = JsonSerializer.Serialize(favorites);
                await StorageHelper.WriteFileAsync("favorites", json);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
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
