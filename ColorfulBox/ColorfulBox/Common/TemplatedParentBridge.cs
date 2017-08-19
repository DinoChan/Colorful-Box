using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ColorfulBox
{
    public class TemplatedParentBridge : DependencyObject
    {

        /// <summary>
        /// 获取或设置TemplatedParent的值
        /// </summary>  
        public object TemplatedParent
        {
            get { return (object)GetValue(TemplatedParentProperty); }
            set { SetValue(TemplatedParentProperty, value); }
        }

        /// <summary>
        /// 标识 TemplatedParent 依赖属性。
        /// </summary>
        public static readonly DependencyProperty TemplatedParentProperty =
            DependencyProperty.Register("TemplatedParent", typeof(object), typeof(TemplatedParentBridge), new PropertyMetadata(null, OnTemplatedParentChanged));

        private static void OnTemplatedParentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            TemplatedParentBridge target = obj as TemplatedParentBridge;
            object oldValue = (object)args.OldValue;
            object newValue = (object)args.NewValue;
            if (oldValue != newValue)
                target.OnTemplatedParentChanged(oldValue, newValue);
        }

        protected virtual void OnTemplatedParentChanged(object oldValue, object newValue)
        {
        }

    }
}
