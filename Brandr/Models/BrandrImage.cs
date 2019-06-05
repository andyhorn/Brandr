using Brandr.Helpers;
using System.Collections.Generic;

namespace Brandr.Models
{
    public class BrandrImage
    {
        private byte[] _original;
        private byte[] _preview;
        private byte[] _reduced;
        private IOperationList _ops;
        public List<IEditOperation> Operations { get => _ops.Operations; }

        #region Properties
        public byte[] Image
        {
            get
            {
                // if no image bytes are loaded, return null
                if(_original == null)
                {
                    return null;
                }
                else
                {
                    return _preview;
                }
            }
        }
        public IEditOperation Saturation => _ops.Get(OpType.Saturation);
        public IEditOperation Exposure => _ops.Get(OpType.Exposure);
        public IEditOperation Contrast => _ops.Get(OpType.Contrast);
        #endregion

        public BrandrImage()
        {
            _original = null;
            _reduced = null;
            _preview = null;
            _ops = new OperationList();
        }

        public bool LoadImage()
        {
            // get the file bytes
            var bytes = FileHelper.GetBytes(Constants.ImageFilter);

            // if nothing was loaded, return
            if (bytes == null || bytes.Length == 0)
            {
                return false;
            }

            // copy the file bytes to our original cache
            _original = bytes;

            // reset any changes
            _ops.ResetAll();

            // get a reduced size image for the preview
            int quality = 50;
            _reduced = Processor.GetPreview(bytes, quality);
            _preview = new byte[_reduced.Length];
            _reduced.CopyTo(_preview, 0);

            return _original != null &&
                _reduced != null;
        }

        public void SaveImage()
        {
            if(_original != null)
            {
                // get the list of edits
                var edits = _ops.GetChanged();

                // copy the original bytes to a new array
                var edited = new byte[_original.Length];
                _original.CopyTo(edited, 0);

                // process all the changes
                Processor.Process(ref edited, edits);

                // save the modified bytes to a file
                FileHelper.SaveBytes(edited, Constants.SaveFilter, Constants.DefaultSaveFormat);
            }
        }

        public void ProcessChanges()
        {
            if(_reduced != null)
            {
                var edits = _ops.GetChanged();

                // if there are no changes, we can return early
                if(edits.Count == 0)
                {
                    return;
                }
                else
                {
                    // get a new byte array
                    byte[] bytes = null;
                    bytes = new byte[_reduced.Length];

                    // copy the reduced backup to the new array
                    _reduced.CopyTo(bytes, 0);

                    // process all the changes
                    Processor.Process(ref bytes, edits);

                    // copy the changes into the preview for the user to see
                    int length = bytes.Length;
                    _preview = new byte[length];
                    bytes.CopyTo(_preview, 0);
                }
            }
        }

        public void Reset(string property)
        {
            if(string.IsNullOrWhiteSpace(property))
            {
                return;
            }

            _ops.Reset(property);
        }

        public void ResetAll()
        {
            _ops.ResetAll();
        }
    }
}
