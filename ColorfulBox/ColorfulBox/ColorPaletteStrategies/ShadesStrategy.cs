using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorfulBox
{
    public class ShadesStrategy : ColorPaletteStrategy
    {
        public ShadesStrategy()
        {
        }

        private bool _isColorsChanging;

        public override void ChangeColorPoints(IList<ColorPoint> colorPoints)
        {
            _isColorsChanging = true;
            try
            {
                var primaryColorPoint = colorPoints.FirstOrDefault(p => p.IsPrimary);
                if (primaryColorPoint == null)
                    return;

                var primaryHsv = primaryColorPoint.HsvColor;
                primaryHsv.V = 1;
                var primatyIndex = colorPoints.IndexOf(primaryColorPoint);
                for (int i = 0; i < colorPoints.Count; i++)
                {
                    var colorPoint = colorPoints[i];
                    var hue = primaryHsv.H;
                    var saturation = primaryHsv.S;
                    var value = (i + 1) * .2;
                    colorPoint.HsvColor = new HsvColor { A = 1, H = hue, S = saturation, V = value };
                }
            }
            finally
            {
                _isColorsChanging = false;
            }
        }

        public override void OnColorChanged(ColorPoint colorPoint, HsvColor oldColor, IList<ColorPoint> colorPoints)
        {
            base.OnColorChanged(colorPoint, oldColor, colorPoints);
            if (_isColorsChanging)
                return;

            _isColorsChanging = true;
            try
            {
                var primaryColorPoint = colorPoints.FirstOrDefault(p => p.IsPrimary);
                if (primaryColorPoint == null)
                    return;

                var colorPointHsv = colorPoint.HsvColor;
                var colorPointIndex = colorPoints.IndexOf(colorPoint);
                var primaryHsv = primaryColorPoint.HsvColor;
                var primatyIndex = colorPoints.IndexOf(primaryColorPoint);
                var oldHsv = oldColor;
                for (int i = 0; i < colorPoints.Count; i++)
                {
                    var hue = colorPointHsv.H;
                    var saturation = colorPointHsv.S;
                    var value = 0d;
                    var point = colorPoints[i];
                    if (colorPointIndex != primatyIndex)
                        value = point.HsvColor.V;
                    else
                    {
                        value = (colorPointHsv.V + (i - colorPointIndex + 5) % 5 * .2) % 1;
                    }

                    point.HsvColor = new HsvColor { A = 1, H = hue, S = saturation, V = value };
                }
            }
            finally
            {
                _isColorsChanging = false;
            }

        }
    }
}
