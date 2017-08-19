using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace ColorfulBox
{
    public class ManipulationBridge
    {

        /// <summary>
        //  从指定元素获取 DragStartedCommand 依赖项属性的值。
        /// </summary>
        /// <param name="obj">The element from which the property value is read.</param>
        /// <returns>DragStartedCommand 依赖项属性的值</returns>
        public static ICommand GetDragStartedCommand(ColorPointVisual obj)
        {
            return (ICommand)obj.GetValue(DragStartedCommandProperty);
        }

        /// <summary>
        /// 将 DragStartedCommand 依赖项属性的值设置为指定元素。
        /// </summary>
        /// <param name="obj">The element on which to set the property value.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetDragStartedCommand(ColorPointVisual obj, ICommand value)
        {
            obj.SetValue(DragStartedCommandProperty, value);
        }

        /// <summary>
        /// 标识 DragStartedCommand 依赖项属性。
        /// </summary>
        public static readonly DependencyProperty DragStartedCommandProperty =
            DependencyProperty.RegisterAttached("DragStartedCommand", typeof(ICommand), typeof(ManipulationBridge), new PropertyMetadata(null, OnDragStartedCommandChanged));


        private static void OnDragStartedCommandChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as ColorPointVisual;
            ICommand oldValue = (ICommand)args.OldValue;
            ICommand newValue = (ICommand)args.NewValue;
            if (oldValue == newValue)
                return;

            if (newValue == null)
                return;

            target.ManipulationStarted += (s, e) =>
            {
                var commandParameter = new DragStartedCommandParameter(target, e.Position);
                newValue.Execute(commandParameter);
            };
        }


        /// <summary>
        //  从指定元素获取 DragDeltaCommand 依赖项属性的值。
        /// </summary>
        /// <param name="obj">The element from which the property value is read.</param>
        /// <returns>DragDeltaCommand 依赖项属性的值</returns>
        public static ICommand GetDragDeltaCommand(ColorPointVisual obj)
        {
            return (ICommand)obj.GetValue(DragDeltaCommandProperty);
        }

        /// <summary>
        /// 将 DragDeltaCommand 依赖项属性的值设置为指定元素。
        /// </summary>
        /// <param name="obj">The element on which to set the property value.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetDragDeltaCommand(ColorPointVisual obj, ICommand value)
        {
            obj.SetValue(DragDeltaCommandProperty, value);
        }

        /// <summary>
        /// 标识 DragDeltaCommand 依赖项属性。
        /// </summary>
        public static readonly DependencyProperty DragDeltaCommandProperty =
            DependencyProperty.RegisterAttached("DragDeltaCommand", typeof(ICommand), typeof(ManipulationBridge), new PropertyMetadata(null, OnDragDeltaCommandChanged));


        private static void OnDragDeltaCommandChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as ColorPointVisual;
            ICommand oldValue = (ICommand)args.OldValue;
            ICommand newValue = (ICommand)args.NewValue;
            if (oldValue == newValue)
                return;

            if (newValue == null)
                return;

            target.ManipulationDelta += (s, e) =>
            {
                var commandParameter = new DragDeltaCommandParameter(target, new Windows.Foundation.Point(e.Delta.Translation.X,e.Delta.Translation.Y));
                newValue.Execute(commandParameter);
            };
        }



    }
}
