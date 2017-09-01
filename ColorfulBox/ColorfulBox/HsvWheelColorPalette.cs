using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;
using ColorHelper = Microsoft.Toolkit.Uwp.Helpers.ColorHelper;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ColorfulBox
{
    public class HsvWheelColorPalette : ColorPalette
    {
        private Point _dragOrginalPosition;


        public HsvWheelColorPalette()
        {
            DefaultStyleKey = typeof(HsvWheelColorPalette);
        }


        protected override void OnColorPointVisualDragStarted(ColorPointVisual colorPointVisual, Point position)
        {
            base.OnColorPointVisualDragStarted(colorPointVisual, position);
            _dragOrginalPosition = position;
            return;
            var bounds = colorPointVisual.GetBoundsRelativeTo(this);
            if (bounds == null)
                return;

            _dragOrginalPosition = new Point(bounds.Value.X + bounds.Value.Width / 2, bounds.Value.Y + bounds.Value.Height / 2);
        }

        protected override void OnColorPointVisualDragDelta(ColorPointVisual colorPointVisual, Point position)
        {
            base.OnColorPointVisualDragDelta(colorPointVisual, position);
            _dragOrginalPosition = new Point(_dragOrginalPosition.X + position.X, _dragOrginalPosition.Y + position.Y);
            colorPointVisual.ColorPoint.Color = GetColor(_dragOrginalPosition, colorPointVisual.ColorPoint.Color);
        }

        protected override void OnColorPaletteStrategyChanged(ColorPaletteStrategy oldValue, ColorPaletteStrategy newValue)
        {
            base.OnColorPaletteStrategyChanged(oldValue, newValue);
            if (newValue == null)
                return;

            newValue.ChangeColorPoints(this.Items.Cast<ColorPoint>().ToList());
        }

        protected override void OnColorChanged(ColorPoint colorPoint, Color oldValue, Color newValue)
        {
            base.OnColorChanged(colorPoint, oldValue, newValue);
            if (ColorPaletteStrategy == null)
                return;

            ColorPaletteStrategy.OnColorChanged(colorPoint, oldValue, Items.OfType<ColorPoint>().ToList());
        }

        private Color GetColor(Point point,Color orginalColor)
        {
            var centerPoint = new Point(ActualWidth / 2, ActualHeight / 2);
            var diameter = ActualWidth < ActualHeight ? ActualWidth : ActualHeight;
            var radius = diameter / 2;

            var distance = Math.Sqrt(Math.Pow(centerPoint.X - point.X, 2) + Math.Pow(centerPoint.Y - point.Y, 2));
            var saturation = distance / radius;
            saturation = Math.Min(1, saturation);


            var distanceOfX = point.X - centerPoint.X;
            var distanceOfY = point.Y - centerPoint.Y;

            var theta = Math.Atan2(distanceOfY, distanceOfX);

            if (theta < 0)
                theta += 2 * Math.PI;


            var orginalHsvColor = orginalColor.ToHsv();
            var hue = (int)(theta / (Math.PI * 2) * 360.0);
            var color = ColorHelper.FromHsv(hue, saturation, orginalHsvColor.V);
            return color;
        }
    }
}
