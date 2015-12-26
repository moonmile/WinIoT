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
using Windows.Devices.Pwm;
using Windows.Devices.Pwm.Provider;


// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace HelloServo
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        PwmPin pwm;

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var pc = await Windows.Devices.Pwm.PwmController.GetDefaultAsync();

            int count = pc.PinCount;

            pwm = pc.OpenPin(1);
            var max = pwm.Controller.MaxFrequency;
            var min = pwm.Controller.MinFrequency;

        }

        private void clickLeft(object sender, RoutedEventArgs e)
        {
            pwm.SetActiveDutyCyclePercentage(0.3);
        }

        private void clickRight(object sender, RoutedEventArgs e)
        {
            pwm.SetActiveDutyCyclePercentage(0.8);
        }
    }
}
