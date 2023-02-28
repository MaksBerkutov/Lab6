using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab6.Module.ViewModel
{
    public partial class MainWindowViewModel:Base.ViewModel
    {
        public Base.Command AddTask2 { get; }
        public Base.Command ReadTask2 { get; }
        public Base.Command ShowTask2 { get; }
        private ObservableCollection<string> _pathToFiles;  
        private ObservableCollection<Logic.Task2.Task_EventArgs> _Reads;
        private Logic.Task2.Task_EventArgs _ReadsSelected;
        private async void AddTask2Handler(object obj)
        {
            await Task.Run(() =>
            {
                var openFileDialog = new OpenFileDialog() { Filter = "Text Files (*.txt)|*.txt" };
                if (openFileDialog.ShowDialog() == true)
                    App.Current.Dispatcher.Invoke(() => PathToFilesValue = openFileDialog.FileName);
            });
        }
        private async void ShowTask2Handler(object obj)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(_ReadsSelected.TextRead,_ReadsSelected.Name);
            });
        }
        private async void ReadTask2Handler(object obj)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(() => _Reads.Clear());
                var Reader = new Logic.Task2(_pathToFiles.ToArray());

               Reader.FileRead += (object sender, Logic.Task2.Task_EventArgs e) => App.Current.Dispatcher.Invoke(() => _Reads.Add(e));
                Reader.ReadFilesAsync();
            });
        }
        private bool CanReadTask2(object obj) => _pathToFiles.Any();
        private bool CanShowTask2(object obj) => _ReadsSelected!=null;
       


        public Logic.Task2.Task_EventArgs ReadsSelected
        {
            get => _ReadsSelected;
            set
            {
                if (_ReadsSelected != value)
                {
                    _ReadsSelected = value;
                    OnPropertyChanged(nameof(ReadsSelected));
                } 
            }
        }
        public ObservableCollection<Logic.Task2.Task_EventArgs> Reads
        {
            get => _Reads;
            set
            {
                _Reads = value;
                OnPropertyChanged(nameof(Reads));
            }
        }
        public ObservableCollection<string> PathToFiles=> _pathToFiles;
        public string PathToFilesValue
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _pathToFiles.Add(value);
                    OnPropertyChanged(nameof(PathToFiles));
                }
            }
        }

    }
}
