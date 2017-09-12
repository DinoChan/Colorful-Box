using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace ColorfulBox
{

    public class ColorPalette : ListView
    {
        #region Constants

        #endregion

        #region Dependency Properties


        /// <summary>
        /// 标识 ColorPaletteStrategy 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorPaletteStrategyProperty =
            DependencyProperty.Register("ColorPaletteStrategy", typeof(ColorPaletteStrategy), typeof(ColorPalette), new PropertyMetadata(null, OnColorPaletteStrategyChanged));

        private static void OnColorPaletteStrategyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorPalette target = obj as ColorPalette;
            ColorPaletteStrategy oldValue = (ColorPaletteStrategy)args.OldValue;
            ColorPaletteStrategy newValue = (ColorPaletteStrategy)args.NewValue;
            if (oldValue != newValue)
                target.OnColorPaletteStrategyChanged(oldValue, newValue);
        }
        #endregion

        #region Constructors
        public ColorPalette()
        {

        }
        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置ColorPaletteStrategy的值
        /// </summary>  
        public ColorPaletteStrategy ColorPaletteStrategy
        {
            get { return (ColorPaletteStrategy)GetValue(ColorPaletteStrategyProperty); }
            set { SetValue(ColorPaletteStrategyProperty, value); }
        }

        #endregion

        #region Events

        #endregion

        #region Fields

        #endregion

        #region Override Methods

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ColorPointVisual();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            if (element is ColorPointVisual visual)
            {
                visual.ManipulationStarted -= OnColorPointVisualDragStarted;
                visual.ManipulationStarted += OnColorPointVisualDragStarted;

                visual.ManipulationDelta -= OnColorPointVisualDragDelta;
                visual.ManipulationDelta += OnColorPointVisualDragDelta;
            }

            var colorPoint = item as ColorPoint;
            colorPoint.ColorChanged -= OnColorChanged;
            colorPoint.ColorChanged += OnColorChanged;
        }



        protected virtual void OnColorPointVisualDragStarted(ColorPointVisual colorPointVisual, Point position)
        {
        }

        protected virtual void OnColorPointVisualDragDelta(ColorPointVisual colorPointVisual, Point position)
        {
        }

        protected virtual void OnColorPaletteStrategyChanged(ColorPaletteStrategy oldValue, ColorPaletteStrategy newValue)
        {
        }

        protected virtual void OnColorChanged(ColorPoint colorPoint, Color oldValue, Color newValue)
        {

        }
        #endregion

        #region Public Methods

        #endregion

        #region Event Methods

        #endregion

        #region Private Methods

        private void OnColorPointVisualDragStarted(object sender, Windows.UI.Xaml.Input.ManipulationStartedRoutedEventArgs e)
        {
            if (sender is ColorPointVisual visual)
                visual.IsSelected = true;

            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (element == null)
                return;

            var bounds = element.GetBoundsRelativeTo(this);
            if (bounds == null)
                return;

            var position = new Point(bounds.Value.X + bounds.Value.Width / 2, bounds.Value.Y + bounds.Value.Height / 2);

            OnColorPointVisualDragStarted(sender as ColorPointVisual, position);
        }

        private void OnColorPointVisualDragDelta(object sender, Windows.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            //Debug.WriteLine(e.Delta.Translation.Y);
            OnColorPointVisualDragDelta(sender as ColorPointVisual, new Point(e.Delta.Translation.X, e.Delta.Translation.Y));
        }

        private void OnColorChanged(object sender, PropertyEventArgs e)
        {
            OnColorChanged(sender as ColorPoint, (Color)e.OldValue, (Color)e.NewValue);
        }


        #endregion


        //protected virtual ColorPointVisual CreateColorPointVisual(ColorPoint colorPoint)
        //{
        //	return new ColorPointVisual();
        //}

    }
}