namespace UniqueBundler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            AssetName = new DataGridViewTextBoxColumn();
            AssetExtension = new DataGridViewTextBoxColumn();
            AssetClass = new DataGridViewComboBoxColumn();
            AssetSize = new DataGridViewTextBoxColumn();
            AssetField = new DataGridViewButtonColumn();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadBundleToolStripMenuItem = new ToolStripMenuItem();
            addFileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveBundleToolStripMenuItem = new ToolStripMenuItem();
            optionToolStripMenuItem = new ToolStripMenuItem();
            openClassConfigToolStripMenuItem = new ToolStripMenuItem();
            openExtensionConfigToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { AssetName, AssetExtension, AssetClass, AssetSize, AssetField });
            dataGridView1.Location = new Point(20, 53);
            dataGridView1.Margin = new Padding(4, 2, 4, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.RowTemplate.Height = 41;
            dataGridView1.Size = new Size(2201, 979);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.UserAddedRow += dataGridView1_UserAddedRow;
            dataGridView1.UserDeletingRow += dataGridView1_UserDeletingRow;
            dataGridView1.DragDrop += dataGridView1_DragDrop;
            dataGridView1.DragEnter += dataGridView1_DragEnter;
            // 
            // AssetName
            // 
            AssetName.HeaderText = "Name";
            AssetName.MinimumWidth = 10;
            AssetName.Name = "AssetName";
            AssetName.SortMode = DataGridViewColumnSortMode.NotSortable;
            AssetName.Width = 200;
            // 
            // AssetExtension
            // 
            AssetExtension.HeaderText = "Extension";
            AssetExtension.MinimumWidth = 10;
            AssetExtension.Name = "AssetExtension";
            AssetExtension.ReadOnly = true;
            AssetExtension.SortMode = DataGridViewColumnSortMode.NotSortable;
            AssetExtension.Width = 200;
            // 
            // AssetClass
            // 
            AssetClass.HeaderText = "Class";
            AssetClass.MinimumWidth = 10;
            AssetClass.Name = "AssetClass";
            AssetClass.Width = 200;
            // 
            // AssetSize
            // 
            AssetSize.HeaderText = "Size";
            AssetSize.MinimumWidth = 10;
            AssetSize.Name = "AssetSize";
            AssetSize.ReadOnly = true;
            AssetSize.SortMode = DataGridViewColumnSortMode.NotSortable;
            AssetSize.Width = 200;
            // 
            // AssetField
            // 
            AssetField.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            AssetField.DefaultCellStyle = dataGridViewCellStyle1;
            AssetField.HeaderText = "Field";
            AssetField.MinimumWidth = 10;
            AssetField.Name = "AssetField";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, saveToolStripMenuItem, optionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(2242, 40);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadBundleToolStripMenuItem, addFileToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(71, 36);
            fileToolStripMenuItem.Text = "File";
            // 
            // loadBundleToolStripMenuItem
            // 
            loadBundleToolStripMenuItem.Name = "loadBundleToolStripMenuItem";
            loadBundleToolStripMenuItem.Size = new Size(280, 44);
            loadBundleToolStripMenuItem.Text = "Load Bundle";
            // 
            // addFileToolStripMenuItem
            // 
            addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            addFileToolStripMenuItem.Size = new Size(280, 44);
            addFileToolStripMenuItem.Text = "Add File";
            addFileToolStripMenuItem.Click += addFileToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveBundleToolStripMenuItem });
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(84, 36);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveBundleToolStripMenuItem
            // 
            saveBundleToolStripMenuItem.Name = "saveBundleToolStripMenuItem";
            saveBundleToolStripMenuItem.Size = new Size(279, 44);
            saveBundleToolStripMenuItem.Text = "Save Bundle";
            // 
            // optionToolStripMenuItem
            // 
            optionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openClassConfigToolStripMenuItem, openExtensionConfigToolStripMenuItem });
            optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            optionToolStripMenuItem.Size = new Size(108, 36);
            optionToolStripMenuItem.Text = "Option";
            // 
            // openClassConfigToolStripMenuItem
            // 
            openClassConfigToolStripMenuItem.Name = "openClassConfigToolStripMenuItem";
            openClassConfigToolStripMenuItem.Size = new Size(388, 44);
            openClassConfigToolStripMenuItem.Text = "Open Class config";
            openClassConfigToolStripMenuItem.Click += openClassConfigToolStripMenuItem_Click;
            // 
            // openExtensionConfigToolStripMenuItem
            // 
            openExtensionConfigToolStripMenuItem.Name = "openExtensionConfigToolStripMenuItem";
            openExtensionConfigToolStripMenuItem.Size = new Size(388, 44);
            openExtensionConfigToolStripMenuItem.Text = "Open Extension config";
            openExtensionConfigToolStripMenuItem.Click += openExtensionConfigToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2242, 1054);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 2, 4, 2);
            Name = "Form1";
            Text = "Unique Bundler 1.0";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem optionToolStripMenuItem;
        private ToolStripMenuItem loadBundleToolStripMenuItem;
        private ToolStripMenuItem addFileToolStripMenuItem;
        private ToolStripMenuItem saveBundleToolStripMenuItem;
        private ToolStripMenuItem openClassConfigToolStripMenuItem;
        private ToolStripMenuItem openExtensionConfigToolStripMenuItem;
        private DataGridViewTextBoxColumn AssetName;
        private DataGridViewTextBoxColumn AssetExtension;
        private DataGridViewComboBoxColumn AssetClass;
        private DataGridViewTextBoxColumn AssetSize;
        private DataGridViewButtonColumn AssetField;
    }
}