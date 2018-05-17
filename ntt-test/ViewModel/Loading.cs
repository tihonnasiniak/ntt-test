using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using ntt_test.View;
using ntt_test.Model;

namespace ntt_test.ViewModel
{
    public class Loading
    {
        public event LoadingControl.CanceledEventArgs Canceled;
        public event LoadingControl.LoadedEventArgs FileLoaded;

        protected LoadingControl _control;
        protected FileLoader _loader;
        protected double _lastPercent;

        public Loading(LoadingControl control)
        {
            _control = control;
            _loader = new FileLoader();
            _loader.FileLoaded += (l) => Application.Current.Dispatcher.Invoke(new Action(() => FileLoaded?.Invoke(l)));
            _loader.ProgressChanged += Loader_ProgressChanged;
            _loader.Error += (s) => Application.Current.Dispatcher.Invoke(new Action(() => Canceled?.Invoke(s)));
        }

        public void LoadFile(string fileName)
        {
            Task.Run(() => _loader.Load(fileName));
        }
        
        private void Loader_ProgressChanged(double percent)
        {
            if (percent - _lastPercent > 0.01)
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _control.LoadProgressBar.Value = percent;
                    }));
                _lastPercent = percent;
            }
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _loader?.Cancel();
            Canceled?.Invoke(null);
        }
    }
}
