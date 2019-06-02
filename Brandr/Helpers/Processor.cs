using ImageProcessor;
using System.IO;

namespace Brandr.Helpers
{
    public static class Processor
    {
        public static byte[] Saturation(byte[] buffer, int saturation)
        {
            using(var inputStream = new MemoryStream(buffer))
            {
                using(var outputStream = new MemoryStream())
                {
                    using(var factory = new ImageFactory())
                    {
                        factory.Load(inputStream)
                            .Saturation(saturation)
                            .Save(outputStream);
                    }

                    var length = (int)outputStream.Length;

                    var bytes = new byte[length];

                    outputStream.Read(bytes, 0, length);

                    return bytes;
                }
            }
        }
    }
}
