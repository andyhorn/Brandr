using Microsoft.Win32;
using System.IO;

namespace Brandr.Helpers
{
    public static class FileHelper
    {
        public static Stream OpenFile(string filter = null)
        {
            var openDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = filter
            };

            var isSelected = openDialog.ShowDialog();

            if(isSelected.HasValue && isSelected.Equals(true))
            {
                var stream = openDialog.OpenFile();

                return stream;
            }

            return null;
        }

        public static Stream[] OpenFiles(string filter = null)
        {
            var openDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = filter
            };

            var isSelected = openDialog.ShowDialog();

            if(isSelected.HasValue && isSelected.Equals(true))
            {
                var streams = openDialog.OpenFiles();

                return streams;
            }

            return null;
        }

        public static void SaveFile(Stream stream, string filter = null, string def = null)
        {
            var bytes = GetBytes(stream);

            SaveFile(bytes, filter, def);
        }

        public static void SaveFile(byte[] bytes, string filter = null, string def = null)
        {
            if(bytes == null || !(bytes.Length > 0))
            {
                return;
            }

            var length = bytes.Length;

            var saveDialog = new SaveFileDialog
            {
                Filter = filter,
                DefaultExt = def
            };

            saveDialog.ShowDialog();

            var file = saveDialog.OpenFile();

            if(file.CanWrite)
            {
                file.Write(bytes, 0, length);
                file.Flush();
                file.Close();
            }
        }

        public static byte[] GetBytes(Stream stream)
        {
            if(stream == null)
            {
                return null;
            }

            var length = (int)stream.Length;

            var buffer = new byte[length];

            stream.Read(buffer, 0, length);

            return buffer;
        }

        public static string GetSavePath(string filter = null, string def = ".png")
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = filter,
                DefaultExt = def
            };

            saveDialog.ShowDialog();

            var path = saveDialog.FileName;

            return path;
        }
    }
}
