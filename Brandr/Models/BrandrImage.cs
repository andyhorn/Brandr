using Brandr.Helpers;
using System.Collections.Generic;

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
        public List<IEditOperation> Operations { get => _ops.Operations; }

        #region Properties
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

                return _display;
            }
        }
        public IEditOperation Saturation => _ops.Saturation;
        public IEditOperation Exposure => _ops.Exposure;
        public IEditOperation Contrast => _ops.Contrast;

        public void Set(string property, double value)
        {

        }
        #endregion

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

        public void ProcessChanges()
        {
            if(_buffer != null)
            {
                byte[] bytes = null;

                var edits = _ops.GetChanged();

                if(edits.Count == 0)
                {
                    return;
                }
                else
                {
                    bytes = new byte[_buffer.Length];
                    _buffer.CopyTo(bytes, 0);
                    Processor.Process(ref bytes, edits);
                }

                int length = bytes.Length;

                _display = new byte[length];

                bytes.CopyTo(_display, 0);
            }
        }

        public void Reset(string property)
        {
            if(string.IsNullOrWhiteSpace(property))
            {
                return;
            }

            switch(property)
            {
                case "Alpha":
                {
                    _ops.Alpha.Reset();
                    break;
                }
                case "Exposure":
                {
                    _ops.Exposure.Reset();
                    break;
                }
                case "Saturation":
                {
                    _ops.Saturation.Reset();
                    break;
                }
                case "Contrast":
                {
                    _ops.Contrast.Reset();
                    break;
                }
            }
        }

        public void ResetAll()
        {
            foreach(var op in Operations)
            {
                op.Reset();
            }
        }
    }
}
