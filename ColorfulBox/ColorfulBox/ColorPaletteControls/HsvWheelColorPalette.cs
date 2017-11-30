using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Microsoft.Toolkit.Uwp;
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
        }

        protected override void OnColorPointVisualDragDelta(ColorPointVisual colorPointVisual, Point position)
        {
            base.OnColorPointVisualDragDelta(colorPointVisual, position);
            _dragOrginalPosition = new Point(_dragOrginalPosition.X + position.X, _dragOrginalPosition.Y + position.Y);
            //Debug.WriteLine(_dragOrginalPosition.X + "   " + _dragOrginalPosition.Y);
            colorPointVisual.ColorPoint.HsvColor = GetColor(_dragOrginalPosition, colorPointVisual.ColorPoint.HsvColor);
        }

        protected override void OnColorPaletteStrategyChanged(ColorPaletteStrategy oldValue, ColorPaletteStrategy newValue)
        {
            base.OnColorPaletteStrategyChanged(oldValue, newValue);

            newValue?.ChangeColorPoints(Items.Cast<ColorPoint>().ToList());
        }

        protected override void OnColorChanged(ColorPoint colorPoint, HsvColor oldValue, HsvColor newValue)
        {
            base.OnColorChanged(colorPoint, oldValue, newValue);

            ColorPaletteStrategy?.OnColorChanged(colorPoint, oldValue, Items.OfType<ColorPoint>().ToList());
        }

        private HsvColor GetColor(Point point, HsvColor orginalColor)
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


            //var orginalHsvColor = orginalColor.ToHsvEx();
            var hue = (theta / (Math.PI * 2) * 360.0);
            var result = new HsvColor {A = orginalColor.A, H = hue, S = saturation, V = orginalColor.V};
            return result;
        }
    }
}
