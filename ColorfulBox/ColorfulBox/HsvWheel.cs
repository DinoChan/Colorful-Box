using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Toolkit.Uwp;
using WinRTXamlToolkit.Imaging;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace ColorfulBox
{

    [TemplatePart(Name = ImageElementName, Type = typeof(Image))]
    public class HsvWheel : Control
    {
        private const string ImageElementName = "ImageElement";

        private Image _imageElement;

        public HsvWheel()
        {
            DefaultStyleKey = typeof(HsvWheel);
            SizeChanged += OnHsvWheelSizeChanged;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _imageElement = GetTemplateChild(ImageElementName) as Image;
        }

        private void OnHsvWheelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_imageElement == null)
                return;

            SizeChanged -= OnHsvWheelSizeChanged;
            var width = Convert.ToInt32(e.NewSize.Width);
            var height = Convert.ToInt32(e.NewSize.Height);
            var diameter = width < height ? width : height;
            diameter = 3000;
            var radius = diameter / 2;
            var source = new WriteableBitmap(diameter, diameter);
            var pixels = source.PixelBuffer.GetPixels();
            var array = new double[diameter, diameter];
            for (var i = 0; i < diameter * diameter; i++)
            {
                var x = i % diameter;
                var y = i / diameter;
                var distance = Math.Sqrt(Math.Pow(radius - x, 2) + Math.Pow(radius - y, 2));
                var saturation = distance / radius;
                array[x, y] = saturation;
                if (saturation >= 1)
                {
                    pixels.Bytes[i * 4] = 0;
                    pixels.Bytes[i * 4 + 1] = 0;
                    pixels.Bytes[i * 4 + 2] = 0;
                    pixels.Bytes[i * 4 + 3] = 0;
                }
                else
                {
                    var distanceOfX = x - radius;
                    var distanceOfY = y - radius;

                    var theta = Math.Atan2(-distanceOfY, distanceOfX);

                    if (theta < 0)
                        theta += 2 * Math.PI;


                    var hue = theta / (Math.PI * 2) * 360.0;
                    var color = ColorHelper.FromHsv(hue, saturation, 1);
                    pixels.Bytes[i * 4] = color.B;
                    pixels.Bytes[i * 4 + 1] = color.G;
                    pixels.Bytes[i * 4 + 2] = color.R;
                    pixels.Bytes[i * 4 + 3] = 255;
                }
            }
            pixels.UpdateFromBytes();
            source.Invalidate();
            _imageElement.Source = source;
        }

        public async void SaveSource()
        {
            var source = _imageElement.Source as WriteableBitmap;
            await source.SaveAsync(KnownFolders.PicturesLibrary, "Wheel2.png");
        }

        public async void Save()
        {
            var bitmap = new RenderTargetBitmap();
            var file = await KnownFolders.PicturesLibrary.CreateFileAsync("Wheel.png", CreationCollisionOption.GenerateUniqueName);
            await bitmap.RenderAsync(this);
            var buffer = await bitmap.GetPixelsAsync();
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encod = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                encod.SetPixelData(BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)bitmap.PixelWidth,
                    (uint)bitmap.PixelHeight,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    buffer.ToArray()
                );
                await encod.FlushAsync();
            }
        }
    }

}
