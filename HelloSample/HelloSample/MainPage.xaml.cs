using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace HelloSample
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                this.DataContext = _vm = new ViewModel(this.Dispatcher);
                _vm.TimeStart();
            };
            this.Unloaded += (s, e) => { _vm.TimeStop(); };
        }
        ViewModel _vm;
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Windows.UI.Core.CoreDispatcher _disp;

        public ViewModel(Windows.UI.Core.CoreDispatcher disp )
        {
            _disp = disp;
        }

        private DateTime _time = DateTime.Now;
        public DateTime Time
        {
            get { return _time; }
            set { this.SetProperty(ref _time, value); }
        }

        Task _task;
        bool _loop = false;
        public void TimeStart()
        {
            _loop = true;
            _task = new Task(async () => {
                while( _loop )
                {
                    await _disp.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        this.Time = DateTime.Now;
                    });
                    await Task.Delay(1000);
                }
            });
            _task.Start();
        }
        public void TimeStop()
        {
            _loop = false;
        }


        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
