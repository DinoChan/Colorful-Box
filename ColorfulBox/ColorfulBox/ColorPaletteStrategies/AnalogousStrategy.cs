using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI;

namespace ColorfulBox
{
    public class AnalogousStrategy : ColorPaletteStrategy
    {
        public AnalogousStrategy()
        {
            _degree = 10;
        }

        private bool _isColorsChanging;
        private double _degree;

        public override void ChangeColorPoints(IList<ColorPoint> colorPoints)
        {
            _isColorsChanging = true;
            try
            {
                var primaryColorPoint = colorPoints.FirstOrDefault(p => p.IsPrimary);
                if (primaryColorPoint == null)
                    return;

                var primaryHsv = primaryColorPoint.Color.ToHsvEx();

                var primatyIndex = colorPoints.IndexOf(primaryColorPoint);
                for (int i = 0; i < colorPoints.Count; i++)
                {
                    if (i == primatyIndex)
                        continue;

                    var colorPoint = colorPoints[i];
                    var hsvColor = colorPoint.Color.ToHsvEx();
                    var hue = primaryHsv.H + (i - primatyIndex) * _degree;
                    while (hue < 0)
                    {
                        hue += 360;
                    }
                    while (hue > 360)
                    {
                        hue -= 360;
                    }

                    colorPoint.Color = ColorExtensions.FromHsvEx(hue, primaryHsv.S, primaryHsv.V);
                }
            }
            finally
            {
                _isColorsChanging = false;
            }
        }

        public override void OnColorChanged(ColorPoint colorPoint, Color oldColor, IList<ColorPoint> colorPoints)
        {
            base.OnColorChanged(colorPoint, oldColor, colorPoints);
            if (_isColorsChanging)
                return;

            _isColorsChanging = true;
            try
            {
                var primaryPoint = colorPoints.FirstOrDefault(p => p.IsPrimary);
                if (primaryPoint == null)
                    return;

                if (primaryPoint != colorPoint)
                {
                    var index = colorPoints.IndexOf(colorPoint);
                    var hsv = colorPoint.Color.ToHsvEx();
                    var primaryHsv = primaryPoint.Color.ToHsvEx();
                    var primaryIndex = colorPoints.IndexOf(primaryPoint);
                    var degreeDifference = hsv.H - primaryHsv.H;
                    if (degreeDifference < -180)
                        degreeDifference += 360;

                    _degree = (degreeDifference) / (index - primaryIndex);
                }

                ChangeColorPoints(colorPoints);
            }
            finally
            {
                _isColorsChanging = false;
            }

        }
    }
}
