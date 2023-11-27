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

        public Form1()
        {
            InitializeComponent();
            file = new FileManager();
            AssetClass.Items.AddRange(file.GetClassNames());
        }

        #region Event
        // Add File
        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] fileNames = file.GetNames(file.AllFileFilter, false);
            foreach (string fileName in fileNames)
            {
                string[] datas = file.GetLineValue(fileName);
                SetLine(datas);
                SetValues(datas[2]);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int field = 4;
            if (e.ColumnIndex == field) OpenFieldForm(e.RowIndex);
        }
        #endregion

        private void SetLine(string[] values, int targetRow = -1)
        {
            int colNum = dataGridView1.Columns.Count;

#if DEBUG
            // Check values length
            if (values.Length != colNum)
                throw new ArgumentException($"'values' should have {colNum} elements.");

            // Check value
            foreach (string value in values)
                if (value == null || value == "")
                    throw new ArgumentException($"'values' can't have null or empty string.");
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
            {

            }
        }

        List<FileManager.ClassFieldData[]> assetsDatas = new List<FileManager.ClassFieldData[]>();
    }
}