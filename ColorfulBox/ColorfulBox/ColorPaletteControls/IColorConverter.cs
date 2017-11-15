using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace ColorfulBox
{
    public interface IColorConverter
    {
        Color ToColor(Color color, double value);
    }
}
