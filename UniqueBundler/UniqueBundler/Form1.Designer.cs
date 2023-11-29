﻿namespace UniqueBundler
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            AssetName = new DataGridViewTextBoxColumn();
            AssetExtension = new DataGridViewTextBoxColumn();
            AssetClass = new DataGridViewComboBoxColumn();
            AssetSize = new DataGridViewTextBoxColumn();
            AssetField = new DataGridViewButtonColumn();
            IsInclude = new DataGridViewCheckBoxColumn();
            menuStrip1 = new MenuStrip();
            addFileToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            loadNormalBundleToolStripMenuItem = new ToolStripMenuItem();
            loadGZIPBundleToolStripMenuItem = new ToolStripMenuItem();
            loadAESBundleToolStripMenuItem = new ToolStripMenuItem();
            loadGZIPandAESBundleToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveBundleToolStripMenuItem = new ToolStripMenuItem();
            saveGZIPBundleToolStripMenuItem = new ToolStripMenuItem();
            saveAESBundleToolStripMenuItem = new ToolStripMenuItem();
            saveAESAndToolStripMenuItem = new ToolStripMenuItem();
            optionToolStripMenuItem = new ToolStripMenuItem();
            openClassConfigToolStripMenuItem = new ToolStripMenuItem();
            openExtensionConfigToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowDrop = true;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { AssetName, AssetExtension, AssetClass, AssetSize, AssetField, IsInclude });
            dataGridView1.Location = new Point(20, 53);
            dataGridView1.Margin = new Padding(4, 2, 4, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.RowTemplate.Height = 41;
            dataGridView1.Size = new Size(2201, 979);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            dataGridView1.UserAddedRow += dataGridView1_UserAddedRow;
            dataGridView1.UserDeletedRow += dataGridView1_UserDeletedRow;
            dataGridView1.DragDrop += dataGridView1_DragDrop;
            dataGridView1.DragEnter += dataGridView1_DragEnter;
            // 
            // AssetName
            // 
            AssetName.HeaderText = "Name";
            AssetName.MinimumWidth = 10;
            AssetName.Name = "AssetName";
            AssetName.Width = 200;
            // 
            // AssetExtension
            // 
            dataGridViewCellStyle1.BackColor = Color.LightGray;
            AssetExtension.DefaultCellStyle = dataGridViewCellStyle1;
            AssetExtension.HeaderText = "Extension";
            AssetExtension.MinimumWidth = 10;
            AssetExtension.Name = "AssetExtension";
            AssetExtension.ReadOnly = true;
            AssetExtension.Width = 200;
            // 
            // AssetClass
            // 
            AssetClass.HeaderText = "Class";
            AssetClass.MinimumWidth = 10;
            AssetClass.Name = "AssetClass";
            AssetClass.SortMode = DataGridViewColumnSortMode.Automatic;
            AssetClass.Width = 200;
            // 
            // AssetSize
            // 
            dataGridViewCellStyle2.BackColor = Color.LightGray;
            AssetSize.DefaultCellStyle = dataGridViewCellStyle2;
            AssetSize.HeaderText = "Size";
            AssetSize.MinimumWidth = 10;
            AssetSize.Name = "AssetSize";
            AssetSize.ReadOnly = true;
            AssetSize.SortMode = DataGridViewColumnSortMode.Programmatic;
            AssetSize.Width = 200;
            // 
            // AssetField
            // 
            AssetField.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            AssetField.DefaultCellStyle = dataGridViewCellStyle3;
            AssetField.HeaderText = "Field";
            AssetField.MinimumWidth = 10;
            AssetField.Name = "AssetField";
            // 
            // IsInclude
            // 
            IsInclude.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            IsInclude.HeaderText = "Include";
            IsInclude.MinimumWidth = 10;
            IsInclude.Name = "IsInclude";
            IsInclude.SortMode = DataGridViewColumnSortMode.Automatic;
            IsInclude.Width = 137;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { addFileToolStripMenuItem, loadToolStripMenuItem, saveToolStripMenuItem, optionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(2242, 42);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // addFileToolStripMenuItem
            // 
            addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            addFileToolStripMenuItem.Size = new Size(121, 38);
            addFileToolStripMenuItem.Text = "Add File";
            addFileToolStripMenuItem.Click += addFileToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadNormalBundleToolStripMenuItem, loadGZIPBundleToolStripMenuItem, loadAESBundleToolStripMenuItem, loadGZIPandAESBundleToolStripMenuItem });
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(85, 38);
            loadToolStripMenuItem.Text = "Load";
            // 
            // loadNormalBundleToolStripMenuItem
            // 
            loadNormalBundleToolStripMenuItem.Name = "loadNormalBundleToolStripMenuItem";
            loadNormalBundleToolStripMenuItem.Size = new Size(372, 44);
            loadNormalBundleToolStripMenuItem.Text = "Normal Bundle";
            loadNormalBundleToolStripMenuItem.Click += loadNormalBundleToolStripMenuItem_Click;
            // 
            // loadGZIPBundleToolStripMenuItem
            // 
            loadGZIPBundleToolStripMenuItem.Name = "loadGZIPBundleToolStripMenuItem";
            loadGZIPBundleToolStripMenuItem.Size = new Size(372, 44);
            loadGZIPBundleToolStripMenuItem.Text = "GZIP Bundle";
            loadGZIPBundleToolStripMenuItem.Click += loadGZIPBundleToolStripMenuItem_Click;
            // 
            // loadAESBundleToolStripMenuItem
            // 
            loadAESBundleToolStripMenuItem.Name = "loadAESBundleToolStripMenuItem";
            loadAESBundleToolStripMenuItem.Size = new Size(372, 44);
            loadAESBundleToolStripMenuItem.Text = "AES Bundle";
            loadAESBundleToolStripMenuItem.Click += loadAESBundleToolStripMenuItem_Click;
            // 
            // loadGZIPandAESBundleToolStripMenuItem
            // 
            loadGZIPandAESBundleToolStripMenuItem.Name = "loadGZIPandAESBundleToolStripMenuItem";
            loadGZIPandAESBundleToolStripMenuItem.Size = new Size(372, 44);
            loadGZIPandAESBundleToolStripMenuItem.Text = "GZIP and AES Bundle";
            loadGZIPandAESBundleToolStripMenuItem.Click += loadGZIPandAESBundleToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveBundleToolStripMenuItem, saveGZIPBundleToolStripMenuItem, saveAESBundleToolStripMenuItem, saveAESAndToolStripMenuItem });
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(84, 38);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveBundleToolStripMenuItem
            // 
            saveBundleToolStripMenuItem.Name = "saveBundleToolStripMenuItem";
            saveBundleToolStripMenuItem.Size = new Size(372, 44);
            saveBundleToolStripMenuItem.Text = "Normal Bundle";
            saveBundleToolStripMenuItem.Click += saveNormalBundleToolStripMenuItem_Click;
            // 
            // saveGZIPBundleToolStripMenuItem
            // 
            saveGZIPBundleToolStripMenuItem.Name = "saveGZIPBundleToolStripMenuItem";
            saveGZIPBundleToolStripMenuItem.Size = new Size(372, 44);
            saveGZIPBundleToolStripMenuItem.Text = "GZIP Bundle";
            saveGZIPBundleToolStripMenuItem.Click += saveGZIPBundleToolStripMenuItem_Click;
            // 
            // saveAESBundleToolStripMenuItem
            // 
            saveAESBundleToolStripMenuItem.Name = "saveAESBundleToolStripMenuItem";
            saveAESBundleToolStripMenuItem.Size = new Size(372, 44);
            saveAESBundleToolStripMenuItem.Text = "AES Bundle";
            saveAESBundleToolStripMenuItem.Click += saveAESBundleToolStripMenuItem_Click;
            // 
            // saveAESAndToolStripMenuItem
            // 
            saveAESAndToolStripMenuItem.Name = "saveAESAndToolStripMenuItem";
            saveAESAndToolStripMenuItem.Size = new Size(372, 44);
            saveAESAndToolStripMenuItem.Text = "GZIP and AES Bundle";
            saveAESAndToolStripMenuItem.Click += saveAESAndToolStripMenuItem_Click;
            // 
            // optionToolStripMenuItem
            // 
            optionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openClassConfigToolStripMenuItem, openExtensionConfigToolStripMenuItem });
            optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            optionToolStripMenuItem.Size = new Size(108, 38);
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
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem optionToolStripMenuItem;
        private ToolStripMenuItem addFileToolStripMenuItem;
        private ToolStripMenuItem saveBundleToolStripMenuItem;
        private ToolStripMenuItem openClassConfigToolStripMenuItem;
        private ToolStripMenuItem openExtensionConfigToolStripMenuItem;
        private DataGridViewTextBoxColumn AssetName;
        private DataGridViewTextBoxColumn AssetExtension;
        private DataGridViewComboBoxColumn AssetClass;
        private DataGridViewTextBoxColumn AssetSize;
        private DataGridViewButtonColumn AssetField;
        private DataGridViewCheckBoxColumn IsInclude;
        private ToolStripMenuItem saveGZIPBundleToolStripMenuItem;
        private ToolStripMenuItem saveAESBundleToolStripMenuItem;
        private ToolStripMenuItem saveAESAndToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem loadNormalBundleToolStripMenuItem;
        private ToolStripMenuItem loadGZIPBundleToolStripMenuItem;
        private ToolStripMenuItem loadAESBundleToolStripMenuItem;
        private ToolStripMenuItem loadGZIPandAESBundleToolStripMenuItem;
    }
}