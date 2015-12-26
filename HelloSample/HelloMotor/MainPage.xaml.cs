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
using Windows.Devices.Gpio;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace HelloMotor
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
            this.Unloaded += (s, e) => _motor.Stop();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var gpio = GpioController.GetDefault();
            _motor = new Motor(gpio.OpenPin(22), gpio.OpenPin(27));
        }

        Motor _motor;

        private void clickFront(object sender, RoutedEventArgs e)
        {
            _motor.GoFront();
        }
        private void clickBack(object sender, RoutedEventArgs e)
        {
            _motor.GoBack();
        }
        private void clickStop(object sender, RoutedEventArgs e)
        {
            _motor.Stop();
        }
    }

    public class Motor
    {
        GpioPin _front, _back;
        public Motor( GpioPin front, GpioPin back )
        {
            _front = front;
            _back = back;

            _front.SetDriveMode(GpioPinDriveMode.Output);
            _front.Write(GpioPinValue.Low);
            _back.SetDriveMode(GpioPinDriveMode.Output);
            _back.Write(GpioPinValue.Low);
        }

        public void GoFront()
        {
            _front.Write(GpioPinValue.High);
            _back.Write(GpioPinValue.Low);
        }
        public void GoBack()
        {
            _front.Write(GpioPinValue.Low);
            _back.Write(GpioPinValue.High);
        }
        public void Stop()
        {
            _front.Write(GpioPinValue.Low);
            _back.Write(GpioPinValue.Low);
        }
    }
}
