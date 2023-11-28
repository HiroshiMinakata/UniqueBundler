using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBundler
{
    class FileSizeComparer : IComparer
    {
        private const int sizeIndex = 3;

        private readonly ListSortDirection direction;

        public FileSizeComparer(ListSortDirection direction)
        {
            this.direction = direction;
        }

        public int Compare(object x, object y)
        {
            string size1 = ((DataGridViewRow)x).Cells[sizeIndex].Value as string;
            string size2 = ((DataGridViewRow)y).Cells[sizeIndex].Value as string;

            long bytes1 = ParseFileSize(size1);
            long bytes2 = ParseFileSize(size2);

            int result = bytes1.CompareTo(bytes2);

            // ソート方向に応じて比較結果を反転
            return direction == ListSortDirection.Ascending ? result : -result;
        }

        private long ParseFileSize(string size)
        {
            if (string.IsNullOrEmpty(size))
                return 0;

            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            string[] split = size.Split(' ');

            if (split.Length != 2)
                return 0;

            double value = double.Parse(split[0]);
            string unit = split[1];

            int index = Array.IndexOf(sizes, unit);
            return (long)(value * Math.Pow(1024, index));
        }
    }
}
