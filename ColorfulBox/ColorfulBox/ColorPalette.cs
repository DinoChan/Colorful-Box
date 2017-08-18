using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace ColorfulBox
{
	[ContentProperty(Name = nameof(ColorPoints))]
	public class ColorPalette : Control
	{
		/// <summary>
		///     标识 ColorPoints 依赖属性。
		/// </summary>
		public static readonly DependencyProperty ColorPointsProperty =
			DependencyProperty.Register("ColorPoints", typeof(Collection<ColorPoint>), typeof(ColorPalette),
				new PropertyMetadata(null, OnColorPointsChanged));


		public ColorPalette()
		{
			ColorPoints = new ObservableCollection<ColorPoint>();
			ColorPointVisualDragStartedCommand = new DelegateCommand<object>(ColorPointVisualDragStarted);
			ColorPointVisualDragDeltaCommand = new DelegateCommand<object>(ColorPointVisualDragDelta);
		}

		public ICommand ColorPointVisualDragStartedCommand { get; }

		public ICommand ColorPointVisualDragDeltaCommand { get; }

		/// <summary>
		///     获取或设置ColorPoints的值
		/// </summary>
		public Collection<ColorPoint> ColorPoints
		{
			get => (Collection<ColorPoint>) GetValue(ColorPointsProperty);
			set => SetValue(ColorPointsProperty, value);
		}



		/// <summary>
		/// 获取或设置SelectedColorPoint的值
		/// </summary>  
		public ColorPoint SelectedColorPoint
		{
			get { return (ColorPoint)GetValue(SelectedColorPointProperty); }
			set { SetValue(SelectedColorPointProperty, value); }
		}

		/// <summary>
		/// 标识 SelectedColorPoint 依赖属性。
		/// </summary>
		public static readonly DependencyProperty SelectedColorPointProperty =
			DependencyProperty.Register("SelectedColorPoint", typeof(ColorPoint), typeof(ColorPalette), new PropertyMetadata(null, OnSelectedColorPointChanged));

		private static void OnSelectedColorPointChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			ColorPalette target = obj as ColorPalette;
			ColorPoint oldValue = (ColorPoint)args.OldValue;
			ColorPoint newValue = (ColorPoint)args.NewValue;
			if (oldValue != newValue)
				target.OnSelectedColorPointChanged(oldValue, newValue);
		}

		protected virtual void OnSelectedColorPointChanged(ColorPoint oldValue, ColorPoint newValue)
		{
		}

		private static void OnColorPointsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			var target = obj as ColorPalette;
			var oldValue = (Collection<ColorPoint>) args.OldValue;
			var newValue = (Collection<ColorPoint>) args.NewValue;
			if (oldValue != newValue)
				target.OnColorPointsChanged(oldValue, newValue);
		}


		protected virtual void OnColorPointsChanged(Collection<ColorPoint> oldValue, Collection<ColorPoint> newValue)
		{
			var notifyCollectionChanged = oldValue as INotifyCollectionChanged;
			if (notifyCollectionChanged != null)
				notifyCollectionChanged.CollectionChanged -= OnColorPointsCollectionChanged;

			notifyCollectionChanged = newValue as INotifyCollectionChanged;

			if (notifyCollectionChanged != null)
				notifyCollectionChanged.CollectionChanged += OnColorPointsCollectionChanged;
		}

		protected virtual void OnColorPointsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
		}

		protected virtual ColorPointVisual CreateColorPointVisual(ColorPoint colorPoint)
		{
			return new ColorPointVisual();
		}

		private void ColorPointVisualDragStarted(object param)
		{
			var parameter = param as DragStartedCommandParameter;
			if (parameter != null)
				OnColorPointVisualDragStarted(parameter.ColorPointVisual, parameter.Position);
		}

		private void ColorPointVisualDragDelta(object param)
		{
			var parameter = param as DragDeltaCommandParameter;
			if (parameter != null)
				OnColorPointVisualDragDelta(parameter.ColorPointVisual, parameter.Translation);
		}

		protected virtual void OnColorPointVisualDragStarted(ColorPointVisual colorPointVisual, Point position)
		{
		}

		protected virtual void OnColorPointVisualDragDelta(ColorPointVisual colorPointVisual, Point position)
		{
		}
	}
}