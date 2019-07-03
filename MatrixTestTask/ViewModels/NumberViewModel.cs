using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace MatrixTestTask.ViewModels
{
    public class NumberViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Vector2 Index
        {
            get => _index;
            set
            {
                _index = value;
                NotifyPropertyChanged();
            }
        }

        public int Number { get; }

        private Vector2 _index;

        public NumberViewModel(int number, Vector2 index)
        {
            Number = number;
            Index = index;
        }
    }
}
