using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ListViewAndListBoxDataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int counter = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void SetField<T>(ref T field, T value, string propertyName)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        ObservableCollection<int> _numbers_list;
        public ObservableCollection<int> numbers_list { get=>_numbers_list; set=>SetField(ref _numbers_list, value, nameof(numbers_list)); }

        public class NumberString : INotifyPropertyChanged
        {
            int _num;
            public int num
            {
                get => _num;
                set => SetField(ref _num, value, nameof(num));
            }

            string _str;
            public string str
            {
                get => _str;
                set => SetField(ref _str, value, nameof(str));
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void SetField<T>(ref T field, T value, string propertyName)
            {
                if (!EqualityComparer<T>.Default.Equals(field, value))
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        // Verbose version
        //public class NumberString : INotifyPropertyChanged
        //{
        //    int _num;
        //    public int num
        //    {
        //        get => _num;
        //        set
        //        {
        //            if (_num != value)
        //            {
        //                _num = value;
        //                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(num)));
        //            }
        //        }
        //    }

        //    string _str;
        //    public string str {
        //        get => _str;
        //        set
        //        {
        //            if (_str != value)
        //            {
        //                _str = value;
        //                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(str)));
        //            }
        //        }
        //    }

        //    public event PropertyChangedEventHandler PropertyChanged;
        //}

        // Non-updating version
        //public class NumberString
        //{
        //    public int num { get; set; }
        //    public string str { get; set; }
        //}

        public ObservableCollection<NumberString> numstr_list { get; set; }

        public MainWindow()
        {
            numbers_list = new ObservableCollection<int>();
            numstr_list = new ObservableCollection<NumberString>();
            
            for (int i = 0; i < 10; i++)
            {
                numbers_list.Add(counter++);
                numstr_list.Add(new NumberString() { num = counter, str = counter.ToString() });
            }

            InitializeComponent();
            this.DataContext = this;
        }

        private void AddNumber(object sender, RoutedEventArgs e)
        {
            numbers_list.Add(counter++);
            numstr_list.Add(new NumberString() { num = counter, str = counter.ToString() });
        }

        private void DeleteNumber(object sender, RoutedEventArgs e)
        {
            numbers_list.RemoveAt(0);
            numstr_list.RemoveAt(0);
        }

        private void NewNumbers(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < numbers_list.Count; i++)
            {
                numbers_list[i] = counter++;
                numstr_list[i].num = counter;
                numstr_list[i].str = counter.ToString();

                //this.DataContext = null;
                //this.DataContext = this;
            }
        }

        private void NewList(object sender, RoutedEventArgs e)
        {
            var _numbers_list = new ObservableCollection<int>();
            for (int i = 0; i < 10; i++)
                _numbers_list.Add(counter++);
            numbers_list = _numbers_list;
        }
    }
}
