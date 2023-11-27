namespace UniqueBundler
{
    public partial class Form1 : Form
    {
        public struct Data
        {
            public object value;
            public bool isUse;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] fileNames = File.GetNames(File.AllFileFilter, false);
            foreach (string fileName in fileNames)
                SetLine(File.GetLineValue(fileName));
        }

        private void SetLine(string[] values, int targetRow = -1)
        {
            int colNum = dataGridView1.Columns.Count;
            int rowNum = dataGridView1.Rows.Count;

#if DEBUG
            // Check values length
            if (values.Length != colNum)
                throw new ArgumentException($"'values' should have {colNum} elements.");

            // Check value
            foreach (string value in values )
                if (value == null || value == "")
                    throw new ArgumentException($"'values' can't have null or empty string.");
#endif

            // Set values
            if (targetRow == -1) targetRow = rowNum - 1;
            for(int col = 0; col < colNum; col++)
                dataGridView1.Rows[targetRow].Cells[col].Value = values[col];
        }
    }
}