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
                assetDatas.Add(file.GetDefaultFieldDatas(className));
            else
                assetDatas[targetRow] = file.GetDefaultFieldDatas(className);
        }

        List<FileManager.ClassFieldData[]> assetDatas = new List<FileManager.ClassFieldData[]>();
    }
}