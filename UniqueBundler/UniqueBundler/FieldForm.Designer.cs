namespace UniqueBundler
{
    partial class FieldForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            Name = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            IsUse = new DataGridViewCheckBoxColumn();
            Cancel = new Button();
            OK = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Name, Value, IsUse });
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.RowTemplate.Height = 41;
            dataGridView1.Size = new Size(883, 492);
            dataGridView1.TabIndex = 0;
            // 
            // Name
            // 
            Name.HeaderText = "Name";
            Name.MinimumWidth = 10;
            Name.Name = "Name";
            Name.ReadOnly = true;
            Name.Width = 200;
            // 
            // Value
            // 
            Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Value.HeaderText = "Value";
            Value.MinimumWidth = 10;
            Value.Name = "Value";
            Value.ReadOnly = true;
            // 
            // IsUse
            // 
            IsUse.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            IsUse.HeaderText = "IsUse";
            IsUse.MinimumWidth = 10;
            IsUse.Name = "IsUse";
            IsUse.Width = 75;
            // 
            // Cancel
            // 
            Cancel.Location = new Point(745, 510);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(150, 46);
            Cancel.TabIndex = 1;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            // 
            // OK
            // 
            OK.Location = new Point(589, 510);
            OK.Name = "OK";
            OK.Size = new Size(150, 46);
            OK.TabIndex = 2;
            OK.Text = "OK";
            OK.UseVisualStyleBackColor = true;
            // 
            // FieldForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(907, 568);
            Controls.Add(OK);
            Controls.Add(Cancel);
            Controls.Add(dataGridView1);
            Name = "FieldForm";
            Text = "Field Form";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Name;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewCheckBoxColumn IsUse;
        private Button Cancel;
        private Button OK;
    }
}