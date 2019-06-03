﻿using ImageProcessor;
using System.Collections.Generic;
using System.IO;

namespace Brandr.Helpers
{
    public static class Processor
    {
        public static void Process(ref byte[] buffer, Dictionary<string, double> edits)
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
                                case "Alpha":
                                {
                                    factory.Alpha((int)edit.Value);
                                    break;
                                }
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
                                case "Flip":
                                {
                                    var flipVertical = edit.Value == 1;
                                    factory.Flip(flipVertical);
                                    break;
                                }
                                case "Pixelate":
                                {
                                    factory.Pixelate((int)edit.Value);
                                    break;
                                }
                                case "Quality":
                                {
                                    factory.Quality((int)edit.Value);
                                    break;
                                }
                                case "Rotate":
                                {
                                    factory.Rotate((int)edit.Value);
                                    break;
                                }
                                case "Rounded":
                                {
                                    factory.RoundedCorners((int)edit.Value);
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
    }
}
