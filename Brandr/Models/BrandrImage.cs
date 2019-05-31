using Brandr.Helpers;
using ImageProcessor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Brandr.Models
{
    public class BrandrImage
    {
        private readonly ImageFactory imageFactory;
        private byte[] imageData;

        private readonly string ImageFilter = "Images (*.bmp;*.jpg;*.gif;*.png;*.jpeg)|*.bmp;*.jpg;*.gif;*.png;*.jpeg";
        private readonly string SaveFilter = "BMP (*.bmp)|*.bmp|JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|All files (*.*)|*.*";
        private readonly string DefaultSaveFormat = "png";

        public ImageSource Image
        {
            get
            {
                if (imageFactory.Image != null)
                {
                    var bytes = GetBytes();

                    var image = ImageHelper.GetImage(bytes);

                    return image;
                }
                else
                {
                    return null;
                }
            }
        }


        //public ImageSource Image { get; set; }

        public BrandrImage()
        {
            imageFactory = new ImageFactory();
            imageData = null;
        }

        public bool LoadImage()
        {
            using(var stream = FileHelper.OpenFile(ImageFilter))
            {
                imageData = FileHelper.GetBytes(stream);
            }

            if (imageData != null)
            {
                imageFactory.Load(imageData);
                return true;
            }

            return false;
        }

        //public void LoadImage()
        //{
        //    using(var stream = FileHelper.OpenFile(ImageFilter))
        //    {
        //        imageData = FileHelper.GetBytes(stream);
        //    }

        //    if (imageData != null)
        //    {
        //        Image = ImageHelper.GetImage(imageData);
        //    }
        //}

        public void SaveImage()
        {
            var filePath = FileHelper.GetSavePath(SaveFilter, DefaultSaveFormat);

            if(filePath != string.Empty)
            {
                imageFactory.Save(filePath);
            }
        }

        //public void SaveImage()
        //{
        //    FileHelper.SaveFile(imageData, SaveFilter, DefaultSaveFormat);
        //}

        private byte[] GetBytes()
        {
            var extension = imageFactory.CurrentImageFormat.ImageFormat;

            var filePath = $"./tmp.{extension}";
            imageFactory.Save(filePath);

            var bytes = FileHelper.GetFileBytes(filePath);

            FileHelper.DeleteFile(filePath);

            return bytes;
        }
    }
}
