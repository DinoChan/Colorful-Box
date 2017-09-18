using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace ColorfulBox
{
    public class ArgbConverter : IColorConverter
    {
        public ArgbModel Model { get; set; }

        public Color ToColor(Color color, double value)
        {
            var byteOfValue = Convert.ToByte(Math.Min(255, value));
            switch (Model)
            {
                case ArgbModel.Alpha:
                    return Color.FromArgb(byteOfValue, color.R, color.G, color.B);
                case ArgbModel.Red:
                    return Color.FromArgb(color.A, byteOfValue, color.G, color.B);
                case ArgbModel.Green:
                    return Color.FromArgb(color.A, color.R, byteOfValue, color.B);
                case ArgbModel.Blue:
                    return Color.FromArgb(color.A, color.R, color.G, byteOfValue);
            }

            return Color.FromArgb(0, 0, 0, 0);
        }
    }
}
