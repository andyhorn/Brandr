using Brandr.Models;
using System.ComponentModel;

namespace Brandr.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string brandingText;
        public BrandrImage BrandrImage { get; set; }
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
        public double Saturation
        {
            get => BrandrImage.Saturation;
            set
            {
                BrandrImage.Saturation = value;
                OnPropertyChanged("Saturation");
            }
        }
        public double Exposure
        {
            get => BrandrImage.Exposure;
            set
            {
                BrandrImage.Exposure = value;
                OnPropertyChanged("Exposure");
            }
        }

        public void Process()
        {
            BrandrImage.ProcessChanges();
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
