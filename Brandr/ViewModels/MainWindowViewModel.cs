using Brandr.Helpers;
using Brandr.Models;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace Brandr.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string brandingText;
        private double saturation;
        public BrandrImage BrandrImage { get; set; }
        public ImageSource Image
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
        public double Saturation
        {
            get => saturation;
            set
            {
                saturation = value;
                OnPropertyChanged("Saturation");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            BrandrImage = new BrandrImage();
        }

        public void LoadImage()
        {
            BrandrImage.LoadImage();
            OnPropertyChanged("Image");
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
