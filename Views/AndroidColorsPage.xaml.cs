using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public sealed partial class AndroidColorsPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private SeeColors.ViewModel.RainbowViewModel _viewModel = null;
        SeeColors.ViewModel.RainbowViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel != value)
                {
                    _viewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public AndroidColorsPage()
        {
            this.InitializeComponent();
            ViewModel = SeeColors.ViewModel.RainbowViewModel.Instance;
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

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is AndroidColor color)
            {
                this.Frame.Navigate(typeof(AndroidColorInfoPage), color);
            }
        }
    }
}
