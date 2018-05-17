using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ntt_test.View
{
    public partial class LoadingControl : UserControl
    {
        public event CanceledEventArgs Canceled;
        public delegate void CanceledEventArgs(string message);
        public event LoadedEventArgs FileLoaded;
        public delegate void LoadedEventArgs(List<Model.Link> linksList);

        protected ViewModel.Loading _viewModel;

        public LoadingControl()
        {
            _viewModel = new ViewModel.Loading(this);
            _viewModel.FileLoaded += (l) => FileLoaded?.Invoke(l);
            _viewModel.Canceled += _viewModel_Canceled;
            InitializeComponent();
        }

        private void _viewModel_Canceled(string message)
        {
            if (!string.IsNullOrEmpty(message))
                MessageBox.Show(message);
            Canceled?.Invoke(message);
        }

        public void LoadFile(string fileName)
        {
            _viewModel.LoadFile(fileName);
        }

        public void LoadFromDatabase()
        {
            _viewModel.LoadFromDatabase();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelButton_Click(sender, e);
            Canceled?.Invoke(null);
        }
    }
}
