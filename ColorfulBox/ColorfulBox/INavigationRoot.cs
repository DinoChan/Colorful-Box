using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorfulBox
{
   public interface INavigationRoot
    {
        event EventHandler IsPaneOpenChanged;

        bool IsPaneOpen { get; }

        double CompactPaneLength { get; }
    }
}
