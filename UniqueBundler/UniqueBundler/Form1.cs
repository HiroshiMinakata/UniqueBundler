using System.ComponentModel;
using System.Text;
using static UniqueBundler.FileManager;
using static UniqueBundler.Form1;

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
        private const int AssetNameIndex = 0;
        private const int ExtensionIndex = 1;
        private const int ClassIndex = 2;
        private const int SizeIndex = 3;
        private const int FieldIndex = 4;
        private const int IsIncludeIndex = 5;

        public Form1()
        {
            InitializeComponent();
            file = new FileManager();
            AssetClass.Items.AddRange(file.GetClassNames());
            InitializeData(0);
        }

        #region Event
        #region Load from file

        // Normal
        private void loadNormalBundleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string loadFileName = GetOpenFileNames(ABFileFilter, false)[0];
            if (loadFileName == "") return;
            LoadBundle loadBundle = new LoadBundle(loadFileName);
            bool loaded = loadBundle.NormalRead();
            if (loaded == false) return;
            SetLoadData(loadBundle);
        }

        // GZIP
        private void loadGZIPBundleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string loadFileName = GetOpenFileNames(ABFileFilter, false)[0];
            if (loadFileName == "") return;
            LoadBundle loadBundle = new LoadBundle(loadFileName);
            bool loaded = loadBundle.GZIPRead();
            if (loaded == false) return;
            SetLoadData(loadBundle);
        }

        // AES
        private void loadAESBundleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string loadFileName = GetOpenFileNames(ABFileFilter, false)[0];
            if (loadFileName == "") return;
            LoadBundle loadBundle = new LoadBundle(loadFileName);
            PasswordForm passForm = new PasswordForm();
            if (passForm.ShowDialog() != DialogResult.OK) return;
            bool loaded = loadBundle.AESRead(passForm.key, passForm.iv);
            if (loaded == false) return;
            SetLoadData(loadBundle);
        }

        // GZIP and AES
        private void loadGZIPandAESBundleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string loadFileName = GetOpenFileNames(ABFileFilter, false)[0];
            if (loadFileName == "") return;
            LoadBundle loadBundle = new LoadBundle(loadFileName);
            PasswordForm passForm = new PasswordForm();
            if (passForm.ShowDialog() != DialogResult.OK) return;
            bool loaded = loadBundle.GZIPandAESRead(passForm.key, passForm.iv);
            if (loaded == false) return;
            SetLoadData(loadBundle);
        }

        private void SetLoadData(LoadBundle loadBundle)
        {
            if (loadBundle.assetNum < 0) return;
            dataGridView1.Rows.Clear();
            int assetNum = loadBundle.assetNum;
            ClassFieldData[][] assetsDatas = loadBundle.assetsFieldDatas;
            string[] assetNames = new string[assetNum];
            string[] formats = new string[assetNum];
            string[] classNames = new string[assetNum];
            string[] fieldStrings = new string[assetNum];

            for (int i = 0; i < assetNum; i++)
            {
                assetNames[i] = loadBundle.metaDatas[i].name;
                formats[i] = loadBundle.footers[i].format;
                classNames[i] = loadBundle.footers[i].className;
                ClassFieldData[] defaultData = file.GetDefaultFieldDatas(classNames[i]);
                for (int j = 0; j < defaultData.Length; j++)
                    assetsDatas[i][j].name = defaultData[j].name;
                fieldStrings[i] = GetFieldString(assetsDatas[i]);
            }

            for (int i = 0; i < assetNum; i++)
            {
                dataGridView1.Rows.Add(assetNames[i], formats[i], classNames[i], "", fieldStrings[i], true);
                dataGridView1.Rows[i].Cells[IsIncludeIndex].Tag = assetsDatas[i];
                ReloadSize(i);
            }
            InitializeData(dataGridView1.Rows.Count - 1);
        }
        #endregion

        #region Save file

        // Normal
        private void saveNormalBundleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int assetNum = GetAssetNum();
            if (assetNum == 0)
            {
                MessageBox.Show("Please select one or more.", "Save bundle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            WriteBundle writeBundle = Save();
            if (writeBundle == null) return;
            writeBundle.NormalWrite();
        }

        // GZIP
        private void saveGZIPBundleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int assetNum = GetAssetNum();
            if (assetNum == 0)
            {
                MessageBox.Show("Please select one or more.", "Save bundle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            WriteBundle writeBundle = Save();
            if (writeBundle == null) return;
            writeBundle.GZIPWrite();
        }

        // AES
        private void saveAESBundleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int assetNum = GetAssetNum();
            if (assetNum == 0)
            {
                MessageBox.Show("Please select one or more.", "Save bundle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            WriteBundle writeBundle = Save();

            if (writeBundle == null) return;

            PasswordForm passForm = new PasswordForm();
            if (passForm.ShowDialog() != DialogResult.OK) return;
            writeBundle.AESWrite(passForm.key, passForm.iv);
        }

        // GZIP and AES
        private void saveAESAndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int assetNum = GetAssetNum();
            if (assetNum == 0)
            {
                MessageBox.Show("Please select one or more.", "Save bundle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            WriteBundle writeBundle = Save();

            if (writeBundle == null) return;

            PasswordForm passForm = new PasswordForm();
            if (passForm.ShowDialog() != DialogResult.OK) return;
            writeBundle.GZIPandAESWrite(passForm.key, passForm.iv);
        }

        private WriteBundle Save()
        {
            string saveFileName = GetSaveFileName(".ab", ABFileFilter);
            if (saveFileName == "")
                return null;
            int assetNum = GetAssetNum();
            ClassFieldData[][] assetsDatas = new ClassFieldData[assetNum][];
            string[] assetNames = new string[assetNum];
            string[] formats = new string[assetNum];
            string[] classNames = new string[assetNum];

            GetWriteAssetsDatas(assetsDatas, assetNames, classNames, formats);
            return new WriteBundle(1, assetNames, assetsDatas, formats, classNames, saveFileName);
        }
        #endregion

        // Add file
        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] fileNames = GetOpenFileNames(AllFileFilter, true);
            AddDatas(fileNames);
            GetToalSize();
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
            GetToalSize();
        }

        // Click cell
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == FieldIndex) OpenFieldForm(e.RowIndex);
        }

        // Open class config
        private void openClassConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenConfigFile();
        }

        // Open extension config
        private void openExtensionConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenExtensionsFile();
        }

        // Add empty data
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int row = e.Row.Index - 1;
            InitializeData(row + 1);
        }

        // Change value
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (row < 0) return;
            // Change class
            if (col == ClassIndex)
            {
                string newClassName = dataGridView1.Rows[row].Cells[ClassIndex].Value.ToString();
                ChangeValue(row, newClassName);
            }

            GetToalSize();
        }
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Sort
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == SizeIndex)
            {
                ListSortDirection direction = dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending ||
                                              dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None
                                              ? ListSortDirection.Ascending
                                              : ListSortDirection.Descending;

                dataGridView1.Sort(new FileSizeComparer(direction));

                dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
            }
        }

        // Added
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        // Deleted
        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            GetToalSize();
        }
        #endregion

        private int GetAssetNum()
        {
            int assetNum = 0;
            for (int row = 0; row < dataGridView1.RowCount; row++)
                if (Convert.ToBoolean(dataGridView1.Rows[row].Cells[IsIncludeIndex].Value) == true)
                    assetNum++;
            return assetNum;
        }

        private void GetWriteAssetsDatas(ClassFieldData[][] assetsDatas, string[] assetNames, string[] classNames, string[] formats)
        {
            int num = 0;
            for (int row = 0; row < dataGridView1.RowCount; row++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[row].Cells[IsIncludeIndex].Value) == false) continue;
                assetsDatas[num] = GetData(row);
                var assetNameValue = dataGridView1.Rows[row].Cells[AssetNameIndex].Value;
                assetNames[num] = assetNameValue != null ? assetNameValue.ToString() : "";
                var formatValue = dataGridView1.Rows[row].Cells[ExtensionIndex].Value;
                formats[num] = formatValue != null ? formatValue.ToString() : "";
                var classNameValue = dataGridView1.Rows[row].Cells[ClassIndex].Value;
                classNames[num] = classNameValue != null ? classNameValue.ToString() : "";
                num++;
            }
        }

        private void AddDatas(string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                if (fileName == "") break;
                string[] datas = file.GetLineValue(fileName);
                if (datas == null) continue;
                SetLine(datas);
                string className = datas[2];
                SetValues(className);
                ReloadSize(dataGridView1.Rows.Count - 2);
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
                dataGridView1.Rows[targetRow].SetValues(values);
        }

        private void SetValues(string className, int targetRow = -1)
        {
            ClassFieldData[] data = file.GetDefaultFieldDatas(className).ToArray();
            if (targetRow == -1)
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[IsIncludeIndex].Tag = data;
            else
                dataGridView1.Rows[targetRow].Cells[IsIncludeIndex].Tag = data;
        }

        private void OpenFieldForm(int row)
        {
            if (row >= dataGridView1.Rows.Count || row < 0) return;
            var data = GetData(row);
            FieldForm propertyForm = new FieldForm(data);
            if (propertyForm.ShowDialog() == DialogResult.OK)
            {
                ReloadSize(row);
                ClassFieldData[] fieldData = (ClassFieldData[])dataGridView1.Rows[row].Cells[IsIncludeIndex].Tag;
                string fieldString = GetFieldString(fieldData);
                dataGridView1.Rows[row].Cells[FieldIndex].Value = fieldString;
                string ext = SetExtension(row);
                UpdateClassName(row, ext);
            }
        }

        private string SetExtension(int row)
        {
            ClassFieldData[] datas = GetData(row);
            foreach (ClassFieldData data in datas)
            {
                if (data.data.GetType() != typeof(byte[])) return "";
                string path = Encoding.UTF8.GetString((byte[])data.data);
                string ext = Path.GetExtension(path);
                if (ext == null || ext == "")
                    return "";
                ext = ext.Substring(1);
                if (ext == "tmp")
                    return "";
                dataGridView1.Rows[row].Cells[ExtensionIndex].Value = ext;
                return ext;
            }
            return "";
        }

        private void UpdateClassName(int row, string extension)
        {
            if (extension == "") return;
            string className = dataGridView1.Rows[row].Cells[ClassIndex].Value.ToString();
            if (className != file.GetClassNames()[0]) return;
            string newClassName = file.GetClassName(extension);
            if (className == newClassName) return;
            ChangeValue(row, newClassName);
        }

        private long ReloadSize(int row = -1)
        {
            if (row == -1) row = dataGridView1.Rows.Count - 1;
            long size = 0;

            string assetName = dataGridView1.Rows[row].Cells[0].Value?.ToString() ?? "";
            string extension = dataGridView1.Rows[row].Cells[1].Value?.ToString() ?? "";
            string className = dataGridView1.Rows[row].Cells[2].Value?.ToString() ?? "";

            // Meta data
            // Asset name
            size += sizeof(int);
            size += assetName.Length;
            // Offset
            size += sizeof(long);
            // Size
            size += sizeof(long);

            // Field
            size += GetSize(GetData(row));

            // Footer
            // Extension
            size += sizeof(int);
            size += extension.Length;
            // Class name
            size += sizeof(int);
            size += className.Length;

            // Set size
            dataGridView1.Rows[row].Cells[SizeIndex].Value = FormatFileSize(size);

            return size;
        }

        private long GetToalSize()
        {
            long totalSize = 0;

            // Header size
            //totalSize += sizeof(int) * 5;
            //totalSize += sizeof(long) * 1;

            for (int row = 0; row < dataGridView1.RowCount; row++)
            {
                bool isInclude = Convert.ToBoolean(dataGridView1.Rows[row].Cells[IsIncludeIndex].Value);
                if (isInclude == false) continue;
                totalSize += ReloadSize(row);
            }

            AssetSize.HeaderText = "Size : " + FormatFileSize(totalSize);
            return totalSize;
        }

        private void ChangeValue(int row, string newClassName)
        {
            if (row < 0) return;

            ClassFieldData[] oldData = GetData(row);
            ClassFieldData[] newData = file.GetDefaultFieldDatas(newClassName).ToArray();

            if (oldData == null) return;

            // Copy data
            int minLength = Math.Min(oldData.Length, newData.Length);
            for (int i = 0; i < minLength; i++)
                if (newData[i].name == oldData[i].name)
                    newData[i].data = oldData[i].data;
                else if (oldData[i].name == "Data" && newData[i].name.Contains("Data"))
                    newData[i].data = oldData[i].data;

            // Set data
            dataGridView1.Rows[row].Cells[IsIncludeIndex].Tag = newData;
            ReloadSize(row);
            ClassFieldData[] fieldData = (ClassFieldData[])dataGridView1.Rows[row].Cells[IsIncludeIndex].Tag;
            string fieldString = GetFieldString(fieldData);
            dataGridView1.Rows[row].Cells[FieldIndex].Value = fieldString;
            dataGridView1.Rows[row].Cells[ClassIndex].Value = newClassName;
        }

        private void InitializeData(int row)
        {
            string[] datas = { "", "", file.GetClassNames()[0], "", "", "False" };
            string className = datas[2];
            ClassFieldData[] data = file.GetDefaultFieldDatas(className).ToArray();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[IsIncludeIndex].Tag = data;
            SetLine(datas, row);
            SetValues(className, row);
            ReloadSize(row);
            string fieldString = GetFieldString(data);
            dataGridView1.Rows[row].Cells[FieldIndex].Value = fieldString;
        }

        private ClassFieldData[] GetData(int row)
        {
            ClassFieldData[] data = (ClassFieldData[])dataGridView1.Rows[row].Cells[IsIncludeIndex].Tag;
            return data;
        }
    }
}