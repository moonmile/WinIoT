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
using Windows.UI;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace HelloLED
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var gpio = GpioController.GetDefault();
            this.ledPin = gpio.OpenPin(LED_PIN);
            this.buttonPin = gpio.OpenPin(BUTTON_PIN);

            ledPin.SetDriveMode(GpioPinDriveMode.Output);
            buttonPin.SetDriveMode(GpioPinDriveMode.InputPullUp);

            ledPin.Write(GpioPinValue.Low);
            _isLED = false;

            buttonPin.ValueChanged += ButtonPin_ValueChanged;

        }

        private async void  ButtonPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                // if (buttonPin.Read() == GpioPinValue.High )
                if ( args.Edge == GpioPinEdge.FallingEdge)
                {
                    ellipse1.Fill = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    ellipse1.Fill = new SolidColorBrush(Colors.Gray);
                }
            });

        }

        private const int LED_PIN = 6;
        private const int BUTTON_PIN = 5;
        private GpioPin ledPin;
        private GpioPin buttonPin;


        private bool _isLED = false;

        /// <summary>
        /// ボタンをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickButton(object sender, RoutedEventArgs e)
        {
            _isLED = !_isLED;
            ledPin.Write(_isLED ? GpioPinValue.High : GpioPinValue.Low);
        }
    }
}
