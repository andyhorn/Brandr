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
        public IEditOperation Saturation => _ops.Get(OpType.Saturation);
        public IEditOperation Exposure => _ops.Get(OpType.Exposure);
        public IEditOperation Contrast => _ops.Get(OpType.Contrast);
        public IEditOperation Alpha => _ops.Get(OpType.Alpha);
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

            _ops.Reset(property);

            //switch(property)
            //{
            //    case "Alpha":
            //    {
            //        Alpha.Reset();
            //        break;
            //    }
            //    case "Exposure":
            //    {
            //        Exposure.Reset();
            //        break;
            //    }
            //    case "Saturation":
            //    {
            //        Saturation.Reset();
            //        break;
            //    }
            //    case "Contrast":
            //    {
            //        Contrast.Reset();
            //        break;
            //    }
            //}
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
