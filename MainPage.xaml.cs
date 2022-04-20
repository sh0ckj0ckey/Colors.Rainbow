using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace SeeColors_UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static ApplicationDataContainer SettingContainer = ApplicationData.Current.LocalSettings;
        public bool mode = true; //true：Windows   false：Android

        public MainPage()
        {
            this.InitializeComponent();

            if ("Windows.Mobile" == Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveForegroundColor = Colors.LightGray;
            if (SettingContainer.Values["language"] == null || SettingContainer.Values["language"].ToString() == "en_US")
            {
                Switch2EN();
            }
            else
            {
                Switch2CN();
            }
            if (SettingContainer.Values["theme"] == null || SettingContainer.Values["theme"].ToString() == "light")
            {
                Switch2Light();
            }
            else
            {
                Switch2Dark();
            }
            PreviewFrame.Navigate(typeof(WindowsColors));
            HideStatusBar();
        }

        /// <summary>
        /// 按下物理返回键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (PreviewFrame.CanGoBack)
            {
                PreviewFrame.GoBack();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 手机端隐藏状态栏
        /// </summary>
        private async void HideStatusBar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusbar = StatusBar.GetForCurrentView();
                await statusbar.HideAsync();
            }
        }

        #region 调色
        static Color mixedColor = new Color();
        SolidColorBrush scb = new SolidColorBrush();

        private void rSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateSliderHeader();
            mixedColor = Color.FromArgb(255, Convert.ToByte(rSlider.Value), Convert.ToByte(gSlider.Value), Convert.ToByte(bSlider.Value));
            scb = new SolidColorBrush(mixedColor);
            colorMix1.Fill = scb;
            colorMix2.Fill = scb;
            colorMix3.Fill = scb;
            PalleteTitleTextBlock.Text = mixedColor.ToString();
            PalleteTitleHyperlinkButton.Foreground = scb;
        }

        private void gSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateSliderHeader();
            mixedColor = Color.FromArgb(255, Convert.ToByte(rSlider.Value), Convert.ToByte(gSlider.Value), Convert.ToByte(bSlider.Value));
            scb = new SolidColorBrush(mixedColor);
            colorMix1.Fill = scb;
            colorMix2.Fill = scb;
            colorMix3.Fill = scb;
            PalleteTitleTextBlock.Text = mixedColor.ToString();
            PalleteTitleHyperlinkButton.Foreground = scb;
        }

        private void bSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdateSliderHeader();
            mixedColor = Color.FromArgb(255, Convert.ToByte(rSlider.Value), Convert.ToByte(gSlider.Value), Convert.ToByte(bSlider.Value));
            scb = new SolidColorBrush(mixedColor);
            colorMix1.Fill = scb;
            colorMix2.Fill = scb;
            colorMix3.Fill = scb;
            PalleteTitleTextBlock.Text = mixedColor.ToString();
            PalleteTitleHyperlinkButton.Foreground = scb;
        }

        private void UpdateSliderHeader()
        {
            rSlider.Header = "R = " + rSlider.Value + ", " + (rSlider.Value / (rSlider.Value + gSlider.Value + bSlider.Value)).ToString("0.00");
            gSlider.Header = "G = " + gSlider.Value + ", " + (gSlider.Value / (rSlider.Value + gSlider.Value + bSlider.Value)).ToString("0.00");
            bSlider.Header = "B = " + bSlider.Value + ", " + (bSlider.Value / (rSlider.Value + gSlider.Value + bSlider.Value)).ToString("0.00");
        }
        #endregion

        /// <summary>
        /// 切换到英文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            Switch2EN();
            SettingContainer.Values["language"] = "en_US";
            if (mode == true)
            {
                PreviewFrame.Navigate(typeof(WindowsColors));
            }
            else
            {
                PreviewFrame.Navigate(typeof(AndroidColorsPage));
            }
        }

        /// <summary>
        /// 切换到中文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            Switch2CN();
            SettingContainer.Values["language"] = "zh_CN";
            if (mode == true)
            {
                PreviewFrame.Navigate(typeof(WindowsColors));
            }
            else
            {
                PreviewFrame.Navigate(typeof(AndroidColorsPage));
            }
        }

        private void Switch2CN()
        {
            PreviewPivotItem.Header = "预览";
            PalletePivotItem.Header = "调色";
            AboutPivotItem.Header = "关于";
        }

        private void Switch2EN()
        {
            PreviewPivotItem.Header = "Preview";
            PalletePivotItem.Header = "Pallete";
            AboutPivotItem.Header = "About";
        }

        /// <summary>
        /// 复制RGB对应的HEX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PalleteTitleHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dp = new DataPackage();
            dp.SetText(PalleteTitleTextBlock.Text);
            Clipboard.SetContent(dp);
            CopySuccessGrid.Visibility = Visibility.Visible;
            RGBShowSuccess.Begin();
        }
        private void DoubleAnimation_Completed(object sender, object e)
        {
            RGBHideSuccess.Begin();
        }

        /// <summary>
        /// 切换模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abb3_Click(object sender, RoutedEventArgs e)
        {
            mode = mode == true ? false : true;
            modeIcon.Source = mode == true ? new BitmapImage(new Uri("ms-appx:///Assets/Icon/android.png", UriKind.Absolute)) : new BitmapImage(new Uri("ms-appx:///Assets/Icon/windows.png", UriKind.Absolute));
            if (mode == true)
            {
                PreviewFrame.Navigate(typeof(WindowsColors));
            }
            else
            {
                PreviewFrame.Navigate(typeof(AndroidColorsPage));
            }
        }

        /// <summary>
        /// 切换主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abb4_Click(object sender, RoutedEventArgs e)
        {
            SettingContainer.Values["theme"] = (SettingContainer.Values["theme"] == null || SettingContainer.Values["theme"].ToString() == "light") ? "dark" : "light";
            if (SettingContainer.Values["theme"].ToString() == "dark")
            {
                Switch2Dark();
                WindowsColors.Current.RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                Switch2Light();
                WindowsColors.Current.RequestedTheme = ElementTheme.Light;
            }
        }

        private void Switch2Dark()
        {
            this.RequestedTheme = ElementTheme.Dark;
            SwitchDarkTextBlock.Visibility = Visibility.Collapsed;
            SwitchLightTextBlock.Visibility = Visibility.Visible;
            BackgroundAcrylicBrush.TintColor = Color.FromArgb(255, 59, 59, 59);
            BackgroundAcrylicBrush.FallbackColor = Color.FromArgb(255, 59, 59, 59);
        }

        private void Switch2Light()
        {
            this.RequestedTheme = ElementTheme.Light;
            SwitchDarkTextBlock.Visibility = Visibility.Visible;
            SwitchLightTextBlock.Visibility = Visibility.Collapsed;
            BackgroundAcrylicBrush.TintColor = Color.FromArgb(255, 255, 255, 255);
            BackgroundAcrylicBrush.FallbackColor = Color.FromArgb(255, 255, 255, 255);
        }
    }
}
