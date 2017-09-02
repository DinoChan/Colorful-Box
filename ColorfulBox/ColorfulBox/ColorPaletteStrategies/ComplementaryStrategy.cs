using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI;
using System.Diagnostics;

namespace ColorfulBox
{
    public class ComplementaryStrategy : ColorPaletteStrategy
    {
        public ComplementaryStrategy()
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

                var primaryHsv = primaryColorPoint.Color.ToHsv();
                primaryHsv.S = 0.76;
                var primatyIndex = colorPoints.IndexOf(primaryColorPoint);
                for (int i = 0; i < colorPoints.Count; i++)
                {
                    var colorPoint = colorPoints[i];
                    if (i == primatyIndex)
                    {
                        colorPoint.Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.FromHsv(primaryHsv.H, primaryHsv.S, primaryHsv.V);
                    }
                    else
                    {
                        var hue = primaryHsv.H;

                        if (i > primatyIndex)
                        {
                            hue += 180;
                            if (hue > 360)
                                hue -= 360;
                        }
                        var value = primaryHsv.V;
                        var saturation = primaryHsv.S;
                        if (Math.Abs(i - primatyIndex) > 1)
                        {
                            value -= (Math.Abs(i - primatyIndex) - 1) * 0.3;
                            value = Math.Max(0, value);
                        }

                        saturation *= 1 + Math.Abs(i - primatyIndex) * 0.15;
                        saturation = Math.Min(1, saturation);

                        colorPoint.Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.FromHsv(hue, saturation, value);
                    }
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
                var primaryColorPoint = colorPoints.FirstOrDefault(p => p.IsPrimary);
                if (primaryColorPoint == null)
                    return;

                var primaryHsv = primaryColorPoint.Color.ToHsv();
                var primatyIndex = colorPoints.IndexOf(primaryColorPoint);
                var colorPointHsv = colorPoint.Color.ToHsv();
                var colorPointIndex = colorPoints.IndexOf(colorPoint);
                if (primaryColorPoint != colorPoint)
                {
                    if (colorPointIndex > primatyIndex)
                    {
                        primaryHsv.H = colorPointHsv.H + 180;
                        if (primaryHsv.H > 360)
                            primaryHsv.H -= 360;
                    }
                    else
                    {
                        primaryHsv.H = colorPointHsv.H;
                    }
                }
                var oldHsv = oldColor.ToHsv();
                for (int i = 0; i < colorPoints.Count; i++)
                {

                    var hue = primaryHsv.H;
                    var point = colorPoints[i];
                    var pointHsv = point.Color.ToHsv();
                    if (i > primatyIndex)
                    {
                        hue += 180;
                        if (hue > 360)
                            hue -= 360;
                    }

                    var saturation = pointHsv.S;
                    if (colorPoint == primaryColorPoint)
                    {
                        if (i == primatyIndex)
                            continue;

                        var saturationRate =  pointHsv.S/ oldHsv.S;
                        if (pointHsv.S == 1)
                        {
                            saturationRate = 1 + Math.Abs(i - primatyIndex) * 0.15;
                        }
                        
                        saturation = saturationRate* primaryHsv.S  ;
                        saturation = Math.Min(1, saturation);
                        if (i == 0)
                        {
                            Debug.WriteLine("saturationRate:"+saturationRate);
                            Debug.WriteLine("oldHsv.S:" + oldHsv.S);
                            Debug.WriteLine("primaryHsv.S:" + primaryHsv.S);
                            Debug.WriteLine("saturation:" + saturation);
                            Debug.WriteLine("pointHsv.S:" + pointHsv.S);
                        }
                    }
                    //var saturationRate = primaryHsv.S / pointHsv.S;
                    //if (saturationRate == 1 )
                    //{
                    //    saturationRate = 1 + Math.Abs(i - primatyIndex) * 0.15;
                    //}
                    //var saturation = primaryHsv.S / saturationRate;
                    point.Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.FromHsv(hue, saturation, pointHsv.V);
                }
            }
            finally
            {
                _isColorsChanging = false;
            }

        }
    }
}
