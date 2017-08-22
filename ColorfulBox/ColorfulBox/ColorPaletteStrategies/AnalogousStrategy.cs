using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp;

namespace ColorfulBox
{
    public class AnalogousStrategy : ColorPaletteStrategy
    {
        public override void ChangeColorPoints(IList<ColorPoint> colorPoints)
        {
            var primaryColorPoint = colorPoints.FirstOrDefault(p => p.IsPrimary);
            if (primaryColorPoint == null)
                return;

            var primaryHsv = ColorHelper.ToHsv(primaryColorPoint.Color);

            var primatyIndex = colorPoints.IndexOf(primaryColorPoint);
            var degree = 10;
            for (int i = 0; i < colorPoints.Count; i++)
            {
                if (i == primatyIndex)
                    continue;

                var colorPoint = colorPoints[i];
                var hsvColor = ColorHelper.ToHsv(colorPoint.Color);
                var hue = primaryHsv.H + (i - primatyIndex) * degree;
                if (hue < 0)
                    hue += 360;
                else if (hue > 360)
                    hue -= 360;

                colorPoint.Color = ColorHelper.FromHsv(hue, primaryHsv.S, primaryHsv.V);
            }
        }
    }
}
