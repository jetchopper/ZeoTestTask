using MatrixTestTask.Models;
using MatrixTestTask.Utils;
using MatrixTestTask.ViewModels;
using System.Collections.ObjectModel;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MatrixTestTask
{
    public sealed partial class MainPage : Page
    {
        public static readonly DependencyProperty NumbersProperty =
        DependencyProperty.Register("Numbers", typeof(ObservableCollection<NumberViewModel>), typeof(MainPage), null);

        public ObservableCollection<NumberViewModel> Numbers
        {
            get { return (ObservableCollection<NumberViewModel>)GetValue(NumbersProperty); }
            set { SetValue(NumbersProperty, value); }
        }

        private int[,] _matrix;

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Numbers = new ObservableCollection<NumberViewModel>();
            ResetMatrix();
        }

        private void Button_ResetClick(object sender, RoutedEventArgs e)
        {
            ResetMatrix();
        }

        private void Button_RotateClick(object sender, RoutedEventArgs e)
        {
            RotateMatrix();
        }

        private void ResetMatrix()
        {
            Numbers.Clear();
            _matrix = MatrixModel.GetInitialMatrix();

            for (int i = 0; i < Settings.MatrixSize; i++)
            {
                for (int j = 0; j < Settings.MatrixSize; j++)
                {
                    Numbers.Add(new NumberViewModel(j + (i * Settings.MatrixSize) + 1, new Vector2(j, i)));
                }
            }
        }

        private void RotateMatrix()
        {
            _matrix = MatrixModel.RotateMatrix(_matrix);

            for (int i = 0; i < Settings.MatrixSize; i++)
            {
                for (int j = 0; j < Settings.MatrixSize; j++)
                {
                    Numbers[_matrix[j, i]].Index = new Vector2(i, j);
                }
            }
        }
    }
}
