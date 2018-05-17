using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

using ntt_test.View;

namespace ntt_test.ViewModel
{
    public class FileSelection
    {
        public event FileSelectionControl.FileSelectedEventHandler FileSelected;
        public event FileSelectionControl.DatabaseSelectedEventHandler DatabaseSelected;

        protected FileSelectionControl _control;

        public FileSelection(FileSelectionControl control)
        {
            _control = control;
        }

        public void FileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    FileSelected?.Invoke(files.First());
                }
                else
                    MessageBox.Show("Перетащите один файл");
            }
        }

        public void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true)
                FileSelected?.Invoke(openFileDialog.FileName);
        }

        public void DatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseSelected?.Invoke();
        }
    }
}
