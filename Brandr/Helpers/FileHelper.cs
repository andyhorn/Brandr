using Microsoft.Win32;

namespace Brandr.Helpers
{
    public static class FileHelper
    {
        public static byte[] GetBytes(string filter = null)
        {
            var openDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = filter
            };

            var fileSelected = openDialog.ShowDialog();

            if(fileSelected.HasValue && fileSelected.Equals(true))
            {
                using(var stream = openDialog.OpenFile())
                {
                    var length = (int)stream.Length;
                    var bytes = new byte[length];

                    stream.Read(bytes, 0, length);

                    return bytes;
                }
            }

            return null;

        }

        public static void SaveBytes(byte[] bytes, string filter = null, string def = null)
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
    }
}
