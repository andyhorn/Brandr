using Brandr.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Documents;

namespace Brandr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;

        public byte[] Image { get => viewModel.Image; }

        public MainWindow()
        {
            viewModel = new MainWindowViewModel();

            InitializeComponent();

            viewModel.Saturation = (int)SaturationSlider.Value;
            viewModel.BrandingText = Brand_Text.Text;

            DataContext = viewModel;
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LoadImage();
            SaturationSlider.Value = 0;
            ExposureSlider.Value = 0;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveImage();
        }


        private void BrandingChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.BrandingText = Brand_Text.Text;
        }

        private void SaturationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            viewModel.Saturation = (int)SaturationSlider.Value;
        }

        private void ExposureSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            viewModel.Exposure = (int)ExposureSlider.Value;
        }

        private void ContrastSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            viewModel.Contrast = (int)ContrastSlider.Value;
        }

        private void ParameterSet(object sender, MouseButtonEventArgs e)
        {
            viewModel.Process();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            var link = sender as Hyperlink;

            var tag = link.Tag.ToString();

            switch(tag)
            {
                case "Exposure":
                {
                    ExposureSlider.Value = 0;
                    viewModel.Exposure = 0;
                    break;
                }
                case "Saturation":
                {
                    SaturationSlider.Value = 0;
                    viewModel.Saturation = 0;
                    break;
                }
                case "Contrast":
                {
                    ContrastSlider.Value = 0;
                    viewModel.Contrast = 0;
                    break;
                }
            }

            viewModel.Process();
        }

        /*
        private void Brand_Display_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(_position == null)
            {
                _position = Brand_Display.TransformToAncestor(Brand_Canvas).Transform(new Point(0, 0));
            }

            var mousePosition = Mouse.GetPosition(Brand_Canvas);

            deltaX = mousePosition.X - _position.Value.X;
            deltaY = mousePosition.Y - _position.Value.Y;

            _isMoving = true;
        }

        private void Brand_Display_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentTT = Brand_Display.RenderTransform as TranslateTransform;
            var currentX = Mouse.GetPosition(Brand_Canvas);

            _isMoving = false;
        }

        private void Brand_Display_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if(!_isMoving)
            {
                return;
            }

            var mousePosition = Mouse.GetPosition(Brand_Canvas);

            var offsetX = (_currentTT == null ? _position.Value.X : _position.Value.X - _currentTT.X) + deltaX - mousePosition.X;
            var offsetY = (_currentTT == null ? _position.Value.Y : _position.Value.Y - _currentTT.Y) + deltaY - mousePosition.Y;

            Brand_Display.RenderTransform = new TranslateTransform(-offsetX, -offsetY);
        }
        */
    }
}
