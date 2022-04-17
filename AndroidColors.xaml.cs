using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace SeeColors_UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AndroidColors : Page
    {
        public static int clickedBtnIndex = -1;
        public static string clickedBtnName = "";

        public AndroidColors()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 重写导航至此页面的代码,显示动画
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transition)
            {
                navigationTransition.DefaultNavigationTransitionInfo = transition;
            }
        }

        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 0;
            clickedBtnName = "Red";
            ShowColor();
        }

        private void PinkButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 1;
            clickedBtnName = "Pink";
            ShowColor();
        }

        private void PurpleButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 2;
            clickedBtnName = "Purple";
            ShowColor();
        }

        private void DeepPurpleButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 3;
            clickedBtnName = "Deep Purple";
            ShowColor();
        }

        private void IndigoButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 4;
            clickedBtnName = "Indigo";
            ShowColor();
        }

        private void BlueButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 5;
            clickedBtnName = "Blue";
            ShowColor();
        }

        private void LightBlueButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 6;
            clickedBtnName = "Light Blue";
            ShowColor();
        }

        private void CyanButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 7;
            clickedBtnName = "Cyan";
            ShowColor();
        }

        private void TealButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 8;
            clickedBtnName = "Teal";
            ShowColor();
        }

        private void GreenButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 9;
            clickedBtnName = "Green";
            ShowColor();
        }

        private void LightGreenButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 10;
            clickedBtnName = "Light Green";
            ShowColor();
        }

        private void LimeButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 11;
            clickedBtnName = "Lime";
            ShowColor();
        }

        private void YellowButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 12;
            clickedBtnName = "Yellow";
            ShowColor();
        }

        private void AmberButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 13;
            clickedBtnName = "Amber";
            ShowColor();
        }

        private void OrangeButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 14;
            clickedBtnName = "Orange";
            ShowColor();
        }

        private void DeepOrangeButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 15;
            clickedBtnName = "Deep Orange";
            ShowColor();
        }

        private void BrownButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 16;
            clickedBtnName = "Brown";
            ShowColor();
        }

        private void GreyButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 17;
            clickedBtnName = "Grey";
            ShowColor();
        }

        private void BlueGreyButton_Click(object sender, RoutedEventArgs e)
        {
            clickedBtnIndex = 18;
            clickedBtnName = "Blue Grey";
            ShowColor();
        }

        private void ShowColor()
        {
            this.Frame.Navigate(typeof(MDColorInfo));
        }

    }
}
