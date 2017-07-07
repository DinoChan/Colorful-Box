using System;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace ColorfulBox
{
    public static class VisualTreeExtensions
    {
        /// <summary>
        ///     Get the bounds of an element relative to another element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="otherElement">
        ///     The element relative to the other element.
        /// </param>
        /// <returns>
        ///     The bounds of the element relative to another element, or null if
        ///     the elements are not related.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="element" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="otherElement" /> is null.
        /// </exception>
        public static Rect? GetBoundsRelativeTo(this FrameworkElement element, UIElement otherElement)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (otherElement == null)
                throw new ArgumentNullException(nameof(otherElement));

            try
            {
                Point origin, bottom;
                var transform = element.TransformToVisual(otherElement);
                if (transform != null &&
                    transform.TryTransform(new Point(), out origin) &&
                    transform.TryTransform(new Point(element.ActualWidth, element.ActualHeight), out bottom))
                    return new Rect(origin, bottom);
            }
            catch (ArgumentException)
            {
                // Ignore any exceptions thrown while trying to transform
            }

            return null;
        }
    }
}