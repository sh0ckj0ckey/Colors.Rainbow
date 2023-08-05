using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Colors.Rainbow.ViewModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class PalettePage : Page
    {
        private RainbowViewModel _viewModel = null;
        public PalettePage()
        {
            _viewModel = RainbowViewModel.Instance;
            this.InitializeComponent();
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
        }

        #endregion

    }
}
