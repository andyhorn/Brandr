using Brandr.Helpers;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Brandr.Models
{
    public class BgraBitmap
    {
        private readonly byte[] _pixels;    // byte array to store the actual pixels
        private readonly int _height;       // image's height
        private readonly int _width;        // image's width
        private readonly int _stride;       // image's stride - number of bytes per row of pixel data
        private bool _changed;
        private WriteableBitmap _image;

        public WriteableBitmap Image
        {
            get
            {
                if(_changed)
                {
                    _image = GetBitmap();
                    _changed = false;
                }

                return _image;
            }
        }

        public enum Values
        {
            Blue = 0,
            Green,
            Red,
            Alpha
        }
        public BgraBitmap(int height, int width)
        {
            _height = height;
            _width = width;
            _stride = _width * 4;
            _pixels = new byte[_width * _height * 4];   // four bytes per pixel (blue, green, red, alpha)
        }

        public WriteableBitmap GetBitmap(double dpiX = 300, double dpiY = 300)
        {
            WriteableBitmap bitmap = new WriteableBitmap(
                _width,
                _height,
                dpiX,
                dpiY,
                PixelFormats.Bgr32,
                null);

            Int32Rect rect = new Int32Rect(0, 0, _width, _height);

            bitmap.WritePixels(rect, _pixels, _stride, 0);

            return bitmap;
        }

        #region Getters
        public void GetPixel(int x, int y, out byte blue, out byte green, out byte red, out byte alpha)
        {
            int index = CalcIndex(x, y, Values.Blue); // start with blue

            blue = _pixels[index];
            green = _pixels[index + (int)Values.Green];
            red = _pixels[index + (int)Values.Red];
            alpha = _pixels[index + (int)Values.Alpha];
        }

        public byte GetBlue(int x, int y)
        {
            int index = (x * 4) + (y * _stride) + (int)Values.Blue; // blue is the first byte in the pixel
            byte blue = _pixels[index];
            return blue;
        }

        public byte GetGreen(int x, int y)
        {
            int index = (x * 4) + (y * _stride) + (int)Values.Green; // green is the second byte in the pixel
            byte green = _pixels[index];
            return green;
        }

        public byte GetRed(int x, int y)
        {
            int index = (x * 4) + (y * _stride) + (int)Values.Red; // red is the third byte in the pixel
            byte red = _pixels[index];
            return red;
        }

        public byte GetAlpha(int x, int y)
        {
            int index = (x * 4) + (y * _stride) + (int)Values.Alpha; // alpha is the fourth (last) byte in the pixel
            byte alpha = _pixels[index];
            return alpha;
        }

        public byte[] GetPixels()
        {
            var bytes = new byte[_pixels.Length];
            _pixels.CopyTo(bytes, 0);

            return bytes;
        }
        #endregion

        #region Setters
        public void SetBlue(int x, int y, byte blue)
        {
            int index = CalcIndex(x, y, Values.Blue);
            _pixels[index] = blue;

            _changed = true;
        }

        public void SetGreen(int x, int y, byte green)
        {
            int index = CalcIndex(x, y, Values.Green);
            _pixels[index] = green;

            _changed = true;
        }

        public void SetRed(int x, int y, byte red)
        {
            int index = CalcIndex(x, y, Values.Red);
            _pixels[index] = red;

            _changed = true;
        }

        public void SetAlpha(int x, int y, byte alpha)
        {
            int index = CalcIndex(x, y, Values.Alpha);
            _pixels[index] = alpha;

            _changed = true;
        }

        public void SetPixel(int x, int y, byte blue, byte green, byte red, byte alpha)
        {
            int index = CalcIndex(x, y, (int)Values.Blue);

            _pixels[index++] = blue;
            _pixels[index++] = green;
            _pixels[index++] = red;
            _pixels[index] = alpha;

            _changed = true;
        }

        public void SetPixel(int x, int y, byte blue, byte green, byte red)
        {
            SetPixel(x, y, blue, green, red, 255);
        }

        public void SetColor(byte blue, byte green, byte red, byte alpha)
        {
            int totalBytes = _width * _height * 4;
            int index = 0;
            while(index < totalBytes)
            {
                _pixels[index++] = blue;
                _pixels[index++] = green;
                _pixels[index++] = red;
                _pixels[index++] = alpha;
            }

            _changed = true;
        }

        public void SetColor(byte blue, byte green, byte red)
        {
            SetColor(blue, green, red, 255);
        }
        #endregion

        private int CalcIndex(int x, int y, Values color)
        {
            // get the x position, skipping 4 bytes for every pixel
            // get the y position, row * number of bytes per row
            int index = (x * 4) + (y * _stride) + (int)color;
            return index;
        }
    }
}
