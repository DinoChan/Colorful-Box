using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace ColorfulBox
{
    public class DragStartedCommandParameter
    {
        public ColorPointVisual ColorPointVisual { get; private set; }

        public Point Position { get; private set; }

        public DragStartedCommandParameter(ColorPointVisual colorPointVisual, Point position)
        {
            ColorPointVisual = colorPointVisual;
            Position = position;
        }

    }
}
