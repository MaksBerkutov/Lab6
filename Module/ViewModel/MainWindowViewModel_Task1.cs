using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Module.ViewModel
{
    public partial class MainWindowViewModel:Base.ViewModel
    {
        private ObservableCollection<int> _Nums;
        private int _MinN;
        private int _CountN;
        private int _FindedNum;
        private int _MaxN;
        public Base.Command StartTask1 { get; }
        public Base.Command InputTask1 { get; }
        private static Random rnd = new Random();
        private int _IndexFindedNum;


        private async void StartTask1Handler(object obj)
        {
            IEnumerable<int> _NumsTmp = new List<int>(_Nums.ToArray());
            _NumsTmp = await Logic.Task1.SortedAsync<int>(
                 await Logic.Task1.ClearCopyItemAsync<int>(_NumsTmp));

            App.Current.Dispatcher.Invoke(() =>_Nums = new ObservableCollection<int>( _NumsTmp.ToArray()));
          IndexFindedNum =  await Logic.Task1.FindItemAsync(_NumsTmp, _FindedNum);
        }
        private bool CanStartTask1(object obj) => _Nums != null && _Nums.Any() 
            && _FindedNum > 0 && _FindedNum <= _MaxN;
        private async void InputTask1Handler(object obj)
        { 
            
            await Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    _Nums.Clear();
                    for (int i = 0; i < _CountN; i++)
                        _Nums.Add(rnd.Next(_MinN, _MaxN));
                    OnPropertyChanged(nameof(AllNum));
                });
               
            });
        }
        private bool CanInputTask1(object obj) => _MinN > 0 && _MaxN > 0 && _CountN > 0 &&
            _MinN < _MaxN;

        public MainWindowViewModel()
        {
            _Nums = new ObservableCollection<int>();
            StartTask1 = new Base.Command(StartTask1Handler, CanStartTask1);
            InputTask1 = new Base.Command(InputTask1Handler, CanInputTask1);
        }

        public ObservableCollection<int> AllNum
        {
            get => _Nums;
           
        }
        public int MinN
        {
            get => _MinN;
            set
            {
                _MinN = value;
                OnPropertyChanged(nameof(MinN));
            }
        }
        public int CountN
        {
            get => _CountN;
            set
            {
                _CountN = value;
                OnPropertyChanged(nameof(CountN));
            }
        }
        public int MaxN
        {
            get => _MaxN;
            set
            {
                _MaxN = value;
                OnPropertyChanged(nameof(MaxN));
            }
        }

        public int IndexFindedNum
        {
            get => _IndexFindedNum;
            set
            {
                _IndexFindedNum = value;
                OnPropertyChanged(nameof(IndexFindedNum));
                OnPropertyChanged(nameof(AllNum));
            }
        }
        public int FindedNum
        {
            get => _FindedNum;
            set
            {
                _FindedNum = value;
                OnPropertyChanged(nameof(FindedNum));
            }
        }

    }
}
