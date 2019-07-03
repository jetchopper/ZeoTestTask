using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace MatrixTestTask.Views
{
    public sealed partial class ImplicitGridView : ItemsControl
    {
        public static readonly DependencyProperty ColumnCountProperty =
        DependencyProperty.Register("ColumnCount", typeof(int), typeof(ImplicitGridView), new PropertyMetadata(4));

        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        private INotifyCollectionChanged _collectionChanged;
        private ImplicitAnimationCollection _implicitAnimations;
        private int _index;
        private readonly List<object> _templates = new List<object>();


        public ImplicitGridView()
        {
            InitializeComponent();
            ImitializeAnimations();
            RegisterPropertyChangedCallback(ItemsSourceProperty, OnItemsSourceChanged);
        }

        private void ImitializeAnimations()
        {
            var compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            var offsetImplicit = compositor.CreateVector3KeyFrameAnimation();
            offsetImplicit.Target = "Offset";
            offsetImplicit.InsertExpressionKeyFrame(1f, "This.FinalValue");
            offsetImplicit.Duration = TimeSpan.FromMilliseconds(300);

            _implicitAnimations = compositor.CreateImplicitAnimationCollection();
            _implicitAnimations["Offset"] = offsetImplicit;
        }

        private void OnItemsSourceChanged(DependencyObject sender, DependencyProperty dp)
        {
            _index = 0;
            if (_collectionChanged != null) _collectionChanged.CollectionChanged -= Changed_CollectionChanged;

            if (ItemsSource is INotifyCollectionChanged changed)
            {
                _collectionChanged = changed;
                _collectionChanged.CollectionChanged += Changed_CollectionChanged;
            }
        }

        private void Changed_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldStartingIndex == -1 && e.NewStartingIndex == -1) _index = 0;

            if (e.Action == NotifyCollectionChangedAction.Move)
            {

            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            if (!_templates.Any(x => x == item))
            {
                _templates.Add(item);
                CreateVisual((ContentPresenter)element);
            }
        }

        private async void CreateVisual(ContentPresenter element)
        {
            var visual = ElementCompositionPreview.GetElementVisual(element);
            visual.ImplicitAnimations = _implicitAnimations;

            await Task.Delay(16);

            Canvas.SetLeft(element, _index % ColumnCount * 32);
            Canvas.SetTop(element, _index / ColumnCount * 32);

            _index++;
        }
    }
}
