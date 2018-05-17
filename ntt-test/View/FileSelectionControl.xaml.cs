using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ntt_test.View
{
    public partial class FileSelectionControl : UserControl
    {
        public event FileSelectedEventHandler FileSelected;
        public delegate void FileSelectedEventHandler(string fileName);

        protected ViewModel.FileSelection _viewModel;

        public FileSelectionControl()
        {
            _viewModel = new ViewModel.FileSelection(this);
            _viewModel.FileSelected += (fn) => FileSelected?.Invoke(fn);
            InitializeComponent();
        }

        private void Control_Drop(object sender, DragEventArgs e)
        {
            _viewModel.FileDrop(sender, e);
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FileButton_Click(sender, e);
        }
    }
}
