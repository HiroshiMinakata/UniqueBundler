using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniqueBundler
{
    public partial class FieldForm : Form
    {
        public FieldForm(FileManager.ClassFieldData[] assetDatas)
        {
            this.assetDatas = assetDatas;
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            foreach (FileManager.ClassFieldData assetData in assetDatas)
            {
                string value = "";
                FileManager.Object2String(assetData.data, ref value);
                dataGridView1.Rows.Add(assetData.name, value, assetData.isUse);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                string data = dataGridView1.Rows[row].Cells[1].Value.ToString();
                assetDatas[row].data = FileManager.String2Object(data, assetDatas[row].data);
            }
            int b = 0;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        FileManager.ClassFieldData[] assetDatas;
    }
}
