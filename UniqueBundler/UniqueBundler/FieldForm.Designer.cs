﻿namespace UniqueBundler
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
            Cancel = new Button();
            OK = new Button();
            FieldName = new DataGridViewTextBoxColumn();
            FieldValue = new DataGridViewTextBoxColumn();
            IsUse = new DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { FieldName, FieldValue, IsUse });
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.RowTemplate.Height = 41;
            dataGridView1.Size = new Size(883, 492);
            dataGridView1.TabIndex = 0;
            // 
            // Cancel
            // 
            Cancel.Location = new Point(745, 510);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(150, 46);
            Cancel.TabIndex = 1;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // OK
            // 
            OK.Location = new Point(589, 510);
            OK.Name = "OK";
            OK.Size = new Size(150, 46);
            OK.TabIndex = 2;
            OK.Text = "OK";
            OK.UseVisualStyleBackColor = true;
            OK.Click += OK_Click;
            // 
            // FieldName
            // 
            FieldName.HeaderText = "Name";
            FieldName.MinimumWidth = 10;
            FieldName.Name = "FieldName";
            FieldName.ReadOnly = true;
            FieldName.Width = 200;
            // 
            // FieldValue
            // 
            FieldValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FieldValue.HeaderText = "Value";
            FieldValue.MinimumWidth = 10;
            FieldValue.Name = "FieldValue";
            // 
            // IsUse
            // 
            IsUse.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            IsUse.HeaderText = "IsUse";
            IsUse.MinimumWidth = 10;
            IsUse.Name = "IsUse";
            IsUse.Width = 75;
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
        private Button Cancel;
        private Button OK;
        private DataGridViewTextBoxColumn FieldName;
        private DataGridViewTextBoxColumn FieldValue;
        private DataGridViewCheckBoxColumn IsUse;
    }
}