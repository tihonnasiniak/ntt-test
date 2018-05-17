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
using System.Windows.Threading;
using Microsoft.Win32;
using System.IO;

namespace ntt_test.View
{
    public partial class MainWindow : Window
    {
        //Содержимое главного окна
        public object MainContent
        {
            get { return (object)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }
        public static readonly DependencyProperty MainContentProperty = 
            DependencyProperty.Register("MainContent", typeof(object), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        public MainWindow()
        {
            InitializeComponent();
            ShowFileSelection();
        }

        //Эту логику лучше вынести в отдельный ViewModel, но пока экранов немного, то можно и так
        public void ShowFileSelection()
        {
            var control = new FileSelectionControl();
            control.FileSelected += ShowLoading;
            control.DatabaseSelected += ShowDatabaseLoading;
            MainContent = control;
        }

        private void ShowLoading(string fileName)
        {
            var control = new LoadingControl();
            control.Canceled += (m) => ShowFileSelection();
            control.FileLoaded += ShowData;
            MainContent = control;
            control.LoadFile(fileName);
        }

        private void ShowDatabaseLoading()
        {
            var control = new LoadingControl();
            control.Canceled += (m) => ShowFileSelection();
            control.FileLoaded += ShowData;
            MainContent = control;
            control.LoadFromDatabase();
        }

        private void ShowData(List<Model.Link> linksList)
        {
            var control = new DataControl(linksList);
            MainContent = control;
        }
    }
}
