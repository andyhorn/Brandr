using Brandr.Models;
using System.ComponentModel;
using System.Linq;

namespace Brandr.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string brandingText;
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public BrandrImage BrandrImage { get; }
        public byte[] Image { get => BrandrImage.Image; }
        public string BrandingText
        {
            get => brandingText;
            set
            {
                brandingText = value;
                OnPropertyChanged("BrandingText");
            }
        }
        public double Alpha => GetValue(OpType.Alpha);
        public double Contrast => GetValue(OpType.Contrast);
        public double Exposure => GetValue(OpType.Exposure);
        public double Saturation => GetValue(OpType.Saturation);
        #endregion

        public MainWindowViewModel()
        {
            BrandrImage = new BrandrImage();
        }

        public void ValueChanged(string property, double value)
        {
            if(string.IsNullOrWhiteSpace(property))
            {
                return;
            }
            
            foreach(var op in BrandrImage.Operations)
            {
                if(op.Type.ToString() == property)
                {
                    op.Value = value;
                    OnPropertyChanged(property);
                    break;
                }
            }
        }
        public void Process()
        {
            BrandrImage.ProcessChanges();
            OnPropertyChanged("Image");
        }

        public void Reset(string property)
        {
            if(string.IsNullOrWhiteSpace(property))
            {
                return;
            }

            BrandrImage.Reset(property);
        }

        public void ResetAll()
        {
            BrandrImage.ResetAll();
        }

        public bool LoadImage()
        {
            BrandrImage.LoadImage();
            OnPropertyChanged("Image");

            return Image != null;
        }

        public void SaveImage()
        {
            BrandrImage.SaveImage();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private double GetValue(OpType property)
        {
            if (BrandrImage == null)
            {
                return 0;
            }

            var value = BrandrImage.Operations.FirstOrDefault(op => op.Type == property)?.Value;

            return value ?? 0;
        }
    }
}
