using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Colors.Rainbow.Models;
using Colors.Rainbow.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Colors.Rainbow.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ColorsPage : Page
    {
        private RainbowViewModel _viewModel = null;
        public ColorsPage()
        {
            _viewModel = RainbowViewModel.Instance;
            this.InitializeComponent();

            CommonShadow.Receivers.Add(WinColorsScrollViewer);
            ColorInfoShadow.Receivers.Add(BackgroundGrid);

            HideGridAnim.Completed += (s, e) =>
            {
                ColorInfoGrid.Visibility = Visibility.Collapsed;
            };
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                RainbowViewModel.Instance.FilterColors(tb.Text);
            }
        }

        private void OnClickColorButton(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is WindowsColor color)
            {
                RainbowViewModel.Instance.SelectedWinColor = color;
                ShowColorInfo();
            }
        }

        private void OnClickHideInfo(object sender, RoutedEventArgs e)
        {
            HideGridAnim.Begin();
        }

        private void FavButton_Click(object sender, RoutedEventArgs e)
        {
            if (RainbowViewModel.Instance.CheckFavorite(RainbowViewModel.Instance.SelectedWinColor.Hex))
            {
                RainbowViewModel.Instance.RemoveFavorite(RainbowViewModel.Instance.SelectedWinColor.Hex);
                FavoriteIcon.Visibility = Visibility.Collapsed;
            }
            else
            {
                RainbowViewModel.Instance.AddFavorite(RainbowViewModel.Instance.SelectedWinColor.Hex);
                FavoriteIcon.Visibility = Visibility.Visible;
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Windows.ApplicationModel.DataTransfer.DataPackage dataPackage = new Windows.ApplicationModel.DataTransfer.DataPackage();
                dataPackage.SetText(RainbowViewModel.Instance.SelectedWinColor.Hex);
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
            }
            catch { }
        }

        public void ShowColorInfo()
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(RainbowViewModel.Instance.SelectedWinColor.VisualColor);
            InfoColor1Ellipse.Fill = solidColorBrush;
            InfoColor2Ellipse.Fill = solidColorBrush;
            InfoColor3Ellipse.Fill = solidColorBrush;

            ColorInfoGrid.Visibility = Visibility.Visible;
            ShowGridAnim.Begin();
        }

    }
}
