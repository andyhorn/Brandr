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
        private OperationList _ops;
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

        public double Saturation
        {
            get
            {
                return _ops.Saturation.Get();
            }
            set
            {
                _ops.Saturation.Set(value);
            } 
        }
        public double Exposure
        {
            get
            {
                return _ops.Exposure.Get();
            }
            set
            {
                _ops.Exposure.Set(value);
            }
        }

        public BrandrImage()
        {
            _buffer = null;
            _display = null;
            _ops = new OperationList();
        }

        public bool LoadImage()
        {
            var bytes = FileHelper.GetBytes(ImageFilter);

            _buffer = bytes;

            _ops.ResetAll();

            return _buffer != null;
        }

        public void SaveImage()
        {
            if(_display != null)
            {
                FileHelper.SaveBytes(_display, SaveFilter, DefaultSaveFormat);
            }
        }

        public void Set()
        {
            if (_buffer != null)
            {
                byte[] bytes = new byte[_buffer.Length];
                _buffer.CopyTo(bytes, 0);

                if (_ops.Saturation.Changed)
                {
                    double saturation = _ops.Saturation.Get();
                    Processor.Saturation(ref bytes, saturation);
                }

                if(_ops.Exposure.Changed)
                {
                    //var exposure = MathHelper.AdjustRange(_ops.Exposure.Get(), 100, -100, 5, 0);
                    double exposure = _ops.Exposure.Get();
                    Processor.Exposure(ref bytes, exposure);
                }

                int length = bytes.Length;

                _display = new byte[length];

                bytes.CopyTo(_display, 0);
            }
        }

        public void SetSaturation()
        {
            Set();
        }
        public void SetExposure()
        {
            Set();
        }
    }
}
