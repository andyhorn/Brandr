using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Brandr.Helpers
{
    public static class ImageHelper
    {
        public static ImageSource GetImage(byte[] data)
        {
            var imageConverter = new ImageSourceConverter();

            var imageSource = imageConverter.ConvertFrom(data) as ImageSource;

            return imageSource;
        }

        public static WriteableBitmap GetBitmap(byte[] data)
        {
            using(var stream = new MemoryStream(data))
            {
                stream.Seek(0, SeekOrigin.Begin);
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.EndInit();

                var write = new WriteableBitmap(bitmap);

                return write;
            }
        }

        public static byte[] GetBytes(Canvas canvas, double pixelDensity = 96d)
        {
            double x = canvas.Margin.Left,
                y = canvas.Margin.Top;

            var background = canvas.Background as ImageBrush;

            var image = background.ImageSource;

            var rect = new Rect(canvas.Margin.Left, canvas.Margin.Top, image.Width, image.Height);

            var bitmap = new RenderTargetBitmap((int)rect.Right, (int)rect.Bottom,
                pixelDensity, pixelDensity, PixelFormats.Default);

            bitmap.Render(canvas);

            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            var stream = new MemoryStream();

            encoder.Save(stream);
            stream.Close();

            var bytes = stream.ToArray();
            return bytes;
        }
    }
}
