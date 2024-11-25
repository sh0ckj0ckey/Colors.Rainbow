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
using Windows.UI.Xaml.Media;

namespace Colors.Rainbow.ViewModel
{
    public class RainbowViewModel : ObservableObject
    {
        private static readonly Lazy<RainbowViewModel> lazy = new Lazy<RainbowViewModel>(() => new RainbowViewModel());
        public static RainbowViewModel Instance { get { return lazy.Value; } }

        private ObservableCollection<WindowsColor> _allWindowsColors = new ObservableCollection<WindowsColor>();

        private ObservableCollection<WindowsColor> _windowsColors = null;
        public ObservableCollection<WindowsColor> WindowsColors
        {
            get => _windowsColors;
            set => SetProperty(ref _windowsColors, value);
        }

        private HashSet<string> _favoriteColorsHex = new HashSet<string>();

        public ObservableCollection<FavoriteColor> FavoriteColors { get; } = new ObservableCollection<FavoriteColor>();
        
        private WindowsColor _SelectedWinColor;
        public WindowsColor SelectedWinColor
        {
            get => _SelectedWinColor;
            set => SetProperty(ref _SelectedWinColor, value);
        }

        public RainbowViewModel()
        {
            InitRainbow();
            InitFavorites();
        }

        private void InitRainbow()
        {
            _allWindowsColors.Clear();
            Type t = typeof(Windows.UI.Colors);
            foreach (PropertyInfo property in t.GetProperties())
            {
                try
                {
                    string method = "get_" + property.Name;
                    MethodInfo get_method = t.GetMethod(method);
                    Color color = (Color)get_method.Invoke(this, null);
                    _allWindowsColors.Add(new WindowsColor
                    {
                        Name = property.Name,
                        RValue = color.R,
                        GValue = color.G,
                        BValue = color.B,
                        Hex = color.ToString(),
                        SolidColor = new SolidColorBrush(color),
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            this.WindowsColors = _allWindowsColors;
        }

        private async void InitFavorites()
        {
            try
            {
                this.FavoriteColors.Clear();
                string json = await StorageHelper.ReadFileAsync("favorites");
                List<FavoriteColor> favorites = JsonSerializer.Deserialize<List<FavoriteColor>>(json);
                foreach (var item in favorites)
                {
                    _favoriteColorsHex.Add(item.Hex);
                    this.FavoriteColors.Add(item);
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
                _favoriteColorsHex.Add(hex);
                this.FavoriteColors.Insert(0, new FavoriteColor() { Hex = hex });
                SaveFavorites();
            }
        }

        public void RemoveFavorite(string hex)
        {
            FavoriteColor remove = null;
            foreach (var favorite in this.FavoriteColors)
            {
                if (favorite.Hex == hex)
                {
                    remove = favorite;
                }
            }

            if (remove != null)
            {
                _favoriteColorsHex.Remove(remove.Hex);
                this.FavoriteColors.Remove(remove);
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
                var favorites = this.FavoriteColors.ToList();
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
                    this.WindowsColors = _allWindowsColors;
                }
                else
                {
                    var filteredColor = _allWindowsColors.Where(p => p.Name.Contains(key, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    this.WindowsColors = new ObservableCollection<WindowsColor>(filteredColor);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

    }
}
