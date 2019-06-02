using Brandr.Helpers;

namespace Brandr.Models
{
    public class BrandrImage
    {
        private readonly string ImageFilter = "Images (*.bmp;*.jpg;*.gif;*.png;*.jpeg)|*.bmp;*.jpg;*.gif;*.png;*.jpeg";
        private readonly string SaveFilter = "BMP (*.bmp)|*.bmp|JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|All files (*.*)|*.*";
        private readonly string DefaultSaveFormat = "png";
        private byte[] _buffer;
        private byte[] _display;
        public byte[] Image
        {
            get
            {
                if(_buffer == null)
                {
                    return null;
                }

                if(_display == null)
                {
                    var length = _buffer.Length;
                    _display = new byte[length];
                    _buffer.CopyTo(_display, 0);
                }

                //_buffer.CopyTo(_display, 0);
                return _display;
            }
        }

        public int Saturation { get; set; }

        public BrandrImage()
        {
            _buffer = null;
            _display = null;
        }

        public bool LoadImage()
        {
            //var filePath = FileHelper.GetFilePath(ImageFilter);

            //if(!string.IsNullOrWhiteSpace(filePath))
            //{
            //    var bytes = FileHelper.GetFileBytes(filePath);
            //    _buffer = bytes;
            //}

            //return _buffer != null;

            var bytes = FileHelper.GetBytes(ImageFilter);

            _buffer = bytes;

            return _buffer != null;
        }

        public void SaveImage()
        {
            if(_display != null)
            {
                FileHelper.SaveBytes(_display, SaveFilter, DefaultSaveFormat);
            }
        }

        public void SetSaturation()
        {
            var bytes = Processor.Saturation(_buffer, Saturation);
            _display = bytes;
        }
    }
}
