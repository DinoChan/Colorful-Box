using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace ColorfulBox
{
    public class ColorPaletteStrategy
    {
        public virtual void ChangeColorPoints(IList<ColorPoint> colorPoints)
        {

        }

        public virtual void OnColorChanged(ColorPoint colorPoint, Color oldColor, IList<ColorPoint> colorPoints)
        {

        }
    }
}
