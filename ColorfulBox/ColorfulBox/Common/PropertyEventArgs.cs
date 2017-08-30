using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorfulBox
{
  public  class PropertyEventArgs
    {
        public PropertyEventArgs(string propertyName,object oldValue,object newValue)
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        //
        // 摘要:
        //     获取属性在报告的更改后的值。
        //
        // 返回结果:
        //     发生更改之后的属性值。
        public object NewValue { get; }
        
        //
        // 摘要:
        //     获取属性在报告的更改前的值。
        //
        // 返回结果:
        //     发生更改之前的属性值。
        public object OldValue { get; }

        //
        // 摘要:
        //     获取发生值更改的项属性的名称。
        //
        // 返回结果:
        //     发生值更改的项属性的名称。
        public string PropertyName { get; }
    }
}
