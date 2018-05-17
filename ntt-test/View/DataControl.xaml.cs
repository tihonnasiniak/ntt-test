using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

using ntt_test.Model;

namespace ntt_test.View
{
    public partial class DataControl : UserControl
    {
        public bool DBButtonsEnabled
        {
            get { return (bool)GetValue(DBButtonsEnabledProperty); }
            set { SetValue(DBButtonsEnabledProperty, value); }
        }
        public static readonly DependencyProperty DBButtonsEnabledProperty = 
            DependencyProperty.Register("DBButtonsEnabled", typeof(bool), typeof(DataControl), new FrameworkPropertyMetadata(null));

        protected List<Link> _links;

        public DataControl(List<Model.Link> links)
        {
            InitializeComponent();
            _links = links;
            this.DataContext = links;
            DBButtonsEnabled = true;
            return;
        }


        //Вынести во ViewModel
        private void SaveToDBButton_Click(object sender, RoutedEventArgs e)
        {
            lock (_links)
            {
                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => DBButtonsEnabled = false));
                    DBInteractor.AddLinksToDB(_links);
                    Application.Current.Dispatcher.Invoke(new Action(() => DBButtonsEnabled = true));
                });
            }
        }

        private void ClearDBButton_Click(object sender, RoutedEventArgs e)
        {
            lock (_links)
            {
                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => DBButtonsEnabled = false));
                    DBInteractor.ClearDB();
                    Application.Current.Dispatcher.Invoke(new Action(() => DBButtonsEnabled = true));
                });
            }
        }
    }
}
