﻿using System.Text;

namespace UniqueBundler
{
    public partial class FieldForm : Form
    {
        public FieldForm(FileManager.ClassFieldData[] assetDatas, string assetName)
        {
            this.assetDatas = assetDatas;
            InitializeComponent();
            InitializeDataGridView();
            if(assetName != "")
                this.Text += " : " + assetName;
        }

        private void InitializeDataGridView()
        {
            if(assetDatas == null) return;
            foreach (FileManager.ClassFieldData assetData in assetDatas)
            {
                dataGridView1.Rows.Add();
                int row = dataGridView1.Rows.Count - 1;

                if (assetData.data == null) return;
                // File
                if (assetData.data.GetType() == typeof(byte[]))
                {
                    DataGridViewButtonCell buttonCell = new DataGridViewButtonCell();
                    dataGridView1.Rows[row].Cells[1] = buttonCell;
                    dataGridView1.Rows[row].Cells[1].Tag = assetData.data;
                }

                string value = "";
                FileManager.Object2String(assetData.data, ref value);
                dataGridView1.Rows[row].SetValues(assetData.name, value, assetData.isUse);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                if (dataGridView1.Rows[row].Cells[1].Value == null) dataGridView1.Rows[row].Cells[1].Value = "";
                string data = dataGridView1.Rows[row].Cells[1].Value.ToString();
                assetDatas[row].isUse = Convert.ToBoolean(dataGridView1.Rows[row].Cells[2].Value);
                assetDatas[row].data = FileManager.String2Object(data, assetDatas[row].data);
                if (assetDatas[row].data.GetType() == typeof(byte[]))
                    assetDatas[row].data = dataGridView1.Rows[row].Cells[1].Tag;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // File open button
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell is DataGridViewButtonCell)
                {
                    string fileName = FileManager.GetOpenFileNames(FileManager.AllFileFilter, false)[0];
                    if (fileName == "") return;
                    FileInfo fileInfo = new FileInfo(fileName);
                    string size = FileManager.FormatFileSize(fileInfo.Length);
                    byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                    cell.Value = size;
                    cell.Tag = fileNameBytes;
                }
            }
        }

        FileManager.ClassFieldData[] assetDatas;
    }
}
