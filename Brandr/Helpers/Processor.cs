using ImageProcessor;
using System.Collections.Generic;
using System.IO;

namespace Brandr.Helpers
{
    public static class Processor
    {
        public static void Process(ref byte[] buffer, double value, string method)
        {
            using(var inputStream = new MemoryStream(buffer))
            {
                using(var outputStream = new MemoryStream())
                {
                    using(var factory = new ImageFactory())
                    {
                        factory.Load(inputStream);

                        switch(method)
                        {
                            case "Saturation":
                            {
                                factory.Saturation((int)value);
                                break;
                            }
                            case "Exposure":
                            {
                                factory.Brightness((int)value);
                                break;
                            }
                            case "Contrast":
                            {
                                factory.Contrast((int)value);
                                break;
                            }
                        }

                        factory.Save(outputStream);

                        var length = (int)outputStream.Length;

                        buffer = new byte[length];

                        outputStream.Read(buffer, 0, length);
                    }
                }
            }
        }

        public static void ProcessMultiple(ref byte[] buffer, Dictionary<string, double> edits)
        {
            using(var inputStream = new MemoryStream(buffer))
            {
                using(var outputStream = new MemoryStream())
                {
                    using(var factory = new ImageFactory())
                    {
                        factory.Load(inputStream);

                        foreach(var edit in edits)
                        {
                            switch(edit.Key)
                            {
                                case "Contrast":
                                {
                                    factory.Contrast((int)edit.Value);
                                    break;
                                }
                                case "Exposure":
                                {
                                    factory.Brightness((int)edit.Value);
                                    break;
                                }
                                case "Saturation":
                                {
                                    factory.Saturation((int)edit.Value);
                                    break;
                                }
                            }
                        }

                        factory.Save(outputStream);

                        var length = (int)outputStream.Length;

                        buffer = new byte[length];

                        outputStream.Read(buffer, 0, length);
                    }
                }
            }
        }
        /*
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
        */
    }
}
