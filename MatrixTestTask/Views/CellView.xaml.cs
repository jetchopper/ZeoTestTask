using System.ComponentModel;
using System.Numerics;
using MatrixTestTask.Utils;
using MatrixTestTask.ViewModels;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace MatrixTestTask.Views
{
    public sealed partial class CellView : UserControl
    {
        private Visual _visual;
        private NumberViewModel _numberViewModel;

        public CellView()
        {
            InitializeComponent();
            SetImplicitAnimation();

            DataContextChanged += CellView_DataContextChanged;
        }

        private void SetImplicitAnimation()
        {
            _visual = ElementCompositionPreview.GetElementVisual(this);
            _visual.Offset = new Vector3(-Vector2.One * (float)Settings.ViewSize / 2, 0);
            _visual.ImplicitAnimations = Helper.GetImlicitAnimation(_visual);
        }

        private void SetOffset()
        {
            _visual.Offset = new Vector3((_numberViewModel.Index - Vector2.One * Settings.MatrixSize / 2) * (float)Settings.ViewSize, 0);
        }

        private void CellView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (_numberViewModel != null) _numberViewModel.PropertyChanged -= NumberViewModel_PropertyChanged;

            if (args.NewValue is NumberViewModel numberViewModel)
            {
                _numberViewModel = numberViewModel;
                _numberViewModel.PropertyChanged += NumberViewModel_PropertyChanged;

                SetOffset();
            }
        }

        private void NumberViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(NumberViewModel.Index))) SetOffset();
        }
    }
}
