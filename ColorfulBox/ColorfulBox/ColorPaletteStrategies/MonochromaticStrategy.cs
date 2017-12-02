using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI;
using System.Diagnostics;
using Microsoft.Toolkit.Uwp;

namespace ColorfulBox
{
    public class MonochromaticStrategy : ColorPaletteStrategy
    {
        public MonochromaticStrategy()
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
                primaryHsv.S = 1;
                var primatyIndex = colorPoints.IndexOf(primaryColorPoint);
                for (int i = 0; i < colorPoints.Count; i++)
                {
                    var colorPoint = colorPoints[i];
                    if (i == primatyIndex)
                    {
                        colorPoint.HsvColor = primaryHsv;
                    }
                    else
                    {
                        var hue = primaryHsv.H;
                        var saturation = primaryHsv.S;
                        var value = primaryHsv.V;
                        saturation *= 1 + Math.Min(0, i - primatyIndex) * .25;
                        value *= 1 + Math.Min(0,  primatyIndex-i) * .25;
                        colorPoint.HsvColor = new HsvColor { A = 1, H = hue, S = saturation, V = value };
                    }
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

                var primaryHsv = primaryColorPoint.HsvColor;
                var primatyIndex = colorPoints.IndexOf(primaryColorPoint);
                var colorPointHsv = colorPoint.HsvColor;
                var colorPointIndex = colorPoints.IndexOf(colorPoint);
                if (primaryColorPoint != colorPoint)
                {
                        primaryHsv.H = colorPointHsv.H;
                }
                var oldHsv = oldColor;
                for (int i = 0; i < colorPoints.Count; i++)
                {

                    var hue = primaryHsv.H;
                    var point = colorPoints[i];
                    var pointHsv = point.HsvColor;
                    
                    var saturation = pointHsv.S;
                    var value = pointHsv.V;
                    if (colorPoint == primaryColorPoint)
                    {
                        if (i == primatyIndex)
                            continue;

                        var saturationRate = pointHsv.S / oldHsv.S;
                        if (pointHsv.S == 1)
                        {
                            saturationRate = 1 + Math.Min(0, i - primatyIndex) * .25;
                        }
                        saturation = saturationRate * primaryHsv.S;
                        saturation = Math.Min(1, saturation);

                        var valueRate = pointHsv.V / oldHsv.V;
                        if (pointHsv.V == 1)
                        {
                            valueRate = 1 + Math.Max(0, primatyIndex - i) * .25;
                        }
                        value = valueRate * primaryHsv.V;
                        value = Math.Min(1, value);

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
