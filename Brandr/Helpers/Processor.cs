using ImageProcessor;
using System.IO;

namespace Brandr.Helpers
{
    public static class Processor
    {
        public static void Saturation(ref byte[] buffer, double saturation)
        {
            using(var inputStream = new MemoryStream(buffer))
            {
                using(var outputStream = new MemoryStream())
                {
                    using(var factory = new ImageFactory())
                    {
                        factory.Load(inputStream)
                            .Saturation((int)saturation)
                            .Save(outputStream);
                    }

                    var length = (int)outputStream.Length;

                    //var bytes = new byte[length];
                    buffer = new byte[length];

                    outputStream.Read(buffer, 0, length);
                }
            }
        }

        public static void Exposure(ref byte[] buffer, double exposure)
        {
            using(var inputStream = new MemoryStream(buffer))
            {
                using(var outputStream = new MemoryStream())
                {
                    using(var factory = new ImageFactory())
                    {
                        factory.Load(inputStream)
                            .Brightness((int)exposure)
                            .Save(outputStream);
                    }

                    var length = (int)outputStream.Length;

                    //var bytes = new byte[length];
                    buffer = new byte[length];

                    outputStream.Read(buffer, 0, length);
                }
            }
        }
    }
}
