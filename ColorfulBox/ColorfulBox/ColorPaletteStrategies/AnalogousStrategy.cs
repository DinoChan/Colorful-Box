using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp;
using Windows.UI;

namespace ColorfulBox
{
    public class AnalogousStrategy : ColorPaletteStrategy
    {
        private bool _isColorsChanging;

        public override void ChangeColorPoints(IList<ColorPoint> colorPoints)
        {
            var primaryColorPoint = colorPoints.FirstOrDefault(p => p.IsPrimary);
            if (primaryColorPoint == null)
                return;

            var primaryHsv = primaryColorPoint.Color.ToHsv();

            var primatyIndex = colorPoints.IndexOf(primaryColorPoint);
            var degree = 10;
            for (int i = 0; i < colorPoints.Count; i++)
            {
                if (i == primatyIndex)
                    continue;

                var colorPoint = colorPoints[i];
                var hsvColor = colorPoint.Color.ToHsv();
                var hue = primaryHsv.H + (i - primatyIndex) * degree;
                if (hue < 0)
                    hue += 360;
                else if (hue > 360)
                    hue -= 360;

                colorPoint.Color = Microsoft.Toolkit.Uwp.ColorHelper.FromHsv(hue, primaryHsv.S, primaryHsv.V);
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

                if (primaryPoint == colorPoint)
                    ChangeColorPoints(colorPoints);
            }
            finally
            {
                _isColorsChanging = false;
            }

        }
    }
}
