using Brandr.Models;
using System.ComponentModel;

namespace Brandr.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string brandingText;
        public BrandrImage BrandrImage { get; set; }
        public byte[] Image
        {
            get
            {
                return BrandrImage.Image;
            }
        }
        public string BrandingText
        {
            get => brandingText;
            set
            {
                brandingText = value;
                OnPropertyChanged("BrandingText");
            }
        }
        public int Saturation
        {
            get => BrandrImage.Saturation;
            set
            {
                BrandrImage.Saturation = value;
                OnPropertyChanged("Saturation");
            }
        }

        public void SetSaturation()
        {
            BrandrImage.SetSaturation();

            OnPropertyChanged("Image");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            BrandrImage = new BrandrImage();
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
    }
}
