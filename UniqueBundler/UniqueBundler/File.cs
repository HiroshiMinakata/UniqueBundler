using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UniqueBundler
{
    public class File
    {
        public static string[] GetLineValue(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            string assetName = Path.GetFileNameWithoutExtension(fi.Name);
            string extension = fi.Extension.Substring(1);
            string className = "a";
            string size = "a";
            string field = "a";
            return new string[] { assetName, extension, className, size, field };
        }

        /// <summary>
        /// Displays a file open dialog and returns the names of the selected files.
        /// </summary>
        /// <param name="filter">Filter string to use in file dialogs.</param>
        /// <param name="multiSelect">Allow multiple selection of files.</param>
        /// <returns>Names of the selected files. Empty string if canceled.</returns>
        public static string[] GetNames(string filter, bool multiSelect)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = filter;
                openFileDialog.Multiselect = multiSelect;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileNames;
            }
            return Array.Empty<string>();
        }
        public const string AllFileFilter = "All files (*.*)|*.*";
        public const string ABFileFilter = "AssetBundle (*.ab*)|*.ab*";

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}
