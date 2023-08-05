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
            InfoShadow.Receivers.Add(BackgroundGrid);

            HideGridAnim.Completed += (s, e) =>
            {
                InfoGrid.Visibility = Visibility.Collapsed;
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
                ShowInfo();
            }
        }

        private void OnClickHideInfo(object sender, RoutedEventArgs e)
        {
            HideGridAnim.Begin();
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

        public void ShowInfo()
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(RainbowViewModel.Instance.SelectedWinColor.VisualColor);
            InfoColor1Ellipse.Fill = solidColorBrush;
            InfoColor2Ellipse.Fill = solidColorBrush;
            InfoColor3Ellipse.Fill = solidColorBrush;
            InfoNameTextBlock.Text = RainbowViewModel.Instance.SelectedWinColor.Name;
            double total = RainbowViewModel.Instance.SelectedWinColor.RValue + RainbowViewModel.Instance.SelectedWinColor.GValue + RainbowViewModel.Instance.SelectedWinColor.BValue;
            if (total > 0)
            {
                RGrid.Width = (RainbowViewModel.Instance.SelectedWinColor.RValue / total) * RGBBarGrid.Width;
                GGrid.Width = (RainbowViewModel.Instance.SelectedWinColor.GValue / total) * RGBBarGrid.Width;
                BGrid.Width = (RainbowViewModel.Instance.SelectedWinColor.BValue / total) * RGBBarGrid.Width;
            }
            else
            {
                RGrid.Width = 16;
                GGrid.Width = 16;
                BGrid.Width = 16;
            }
            RTextBlock.Text = RainbowViewModel.Instance.SelectedWinColor.RValue.ToString();
            GTextBlock.Text = RainbowViewModel.Instance.SelectedWinColor.GValue.ToString();
            BTextBlock.Text = RainbowViewModel.Instance.SelectedWinColor.BValue.ToString();
            HEXTextBox.Text = RainbowViewModel.Instance.SelectedWinColor.Hex;
            InfoGrid.Visibility = Visibility.Visible;
            ShowGridAnim.Begin();
        }
    }
}
