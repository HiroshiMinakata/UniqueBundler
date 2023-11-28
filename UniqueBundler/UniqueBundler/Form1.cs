namespace UniqueBundler
{
    public partial class Form1 : Form
    {
        public struct Data
        {
            public object value;
            public bool isUse;
        }

        private FileManager file;
        private const int sizeIndex = 3;
        private const int fieldIndex = 4;

        public Form1()
        {
            InitializeComponent();
            file = new FileManager();
            AssetClass.Items.AddRange(file.GetClassNames());
        }

        #region Event
        // Add file
        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] fileNames = file.GetNames(file.AllFileFilter, false);
            AddDatas(fileNames);
        }

        // Drag file
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // Drop file
        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            AddDatas(fileNames);
        }

        // Click cell
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == fieldIndex) OpenFieldForm(e.RowIndex);
        }

        // Delete
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            assetsDatas.RemoveAt(e.Row.Index);
        }

        // Open class config
        private void openClassConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManager.OpenConfigFile();
        }

        // Open extension config
        private void openExtensionConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManager.OpenExtensionsFile();
        }

        // Add empty data
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int row = e.Row.Index - 1;
            string[] datas = { "", "", "None", "", "" };
            string className = datas[2];
            assetsDatas.Add(file.GetDefaultFieldDatas(className));
            SetLine(datas, row);
            SetValues(className, row);
            ReloadSize(row);
        }
        #endregion

        private void AddDatas(string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                string[] datas = file.GetLineValue(fileName);
                if (datas == null) continue;
                SetLine(datas);
                string className = datas[2];
                SetValues(className);
            }
        }

        private void SetLine(string[] values, int targetRow = -1)
        {
            int colNum = dataGridView1.Columns.Count;

#if DEBUG
            // Check values length
            if (values.Length != colNum)
                throw new ArgumentException($"'values' should have {colNum} elements.");

            // Check value
            foreach (string value in values)
                if (value == null)
                    throw new ArgumentException($"'values' can't have null.");
#endif

            // Set values
            if (targetRow == -1)
                dataGridView1.Rows.Add(values);
            else
                for (int col = 0; col < colNum; col++)
                    dataGridView1.Rows[targetRow].Cells[col].Value = values[col];
        }

        private void SetValues(string className, int targetRow = -1)
        {
            if (targetRow == -1)
                assetsDatas.Add(file.GetDefaultFieldDatas(className));
            else
                assetsDatas[targetRow] = file.GetDefaultFieldDatas(className);
        }
        private void OpenFieldForm(int row)
        {
            if (assetsDatas.Count <= row) return;
            FieldForm propertyForm = new FieldForm(assetsDatas[row]);
            if (propertyForm.ShowDialog() == DialogResult.OK)
                ReloadSize(row);
        }

        private void ReloadSize(int row)
        {
            long size = FileManager.GetSize(assetsDatas[row]);
            dataGridView1.Rows[row].Cells[sizeIndex].Value = FileManager.FormatFileSize(size);
        }

        List<FileManager.ClassFieldData[]> assetsDatas = new List<FileManager.ClassFieldData[]>();
    }
}