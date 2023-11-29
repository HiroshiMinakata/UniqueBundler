namespace UniqueBundler
{
    partial class PasswordForm
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
            OK_Button = new Button();
            Cancel_Button = new Button();
            IV_TextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            Key_TextBox = new TextBox();
            SuspendLayout();
            // 
            // OK_Button
            // 
            OK_Button.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            OK_Button.Location = new Point(482, 206);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(150, 46);
            OK_Button.TabIndex = 1;
            OK_Button.Text = "OK";
            OK_Button.UseVisualStyleBackColor = true;
            OK_Button.Click += OK_Button_Click;
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Cancel_Button.Location = new Point(638, 206);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(150, 46);
            Cancel_Button.TabIndex = 2;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = true;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // IV_TextBox
            // 
            IV_TextBox.Location = new Point(12, 158);
            IV_TextBox.Name = "IV_TextBox";
            IV_TextBox.Size = new Size(776, 39);
            IV_TextBox.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 123);
            label1.Name = "label1";
            label1.Size = new Size(274, 32);
            label1.TabIndex = 6;
            label1.Text = "IV ( Initialization Vector )";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 19);
            label2.Name = "label2";
            label2.Size = new Size(53, 32);
            label2.TabIndex = 8;
            label2.Text = "Key";
            // 
            // Key_TextBox
            // 
            Key_TextBox.Location = new Point(12, 64);
            Key_TextBox.Name = "Key_TextBox";
            Key_TextBox.Size = new Size(776, 39);
            Key_TextBox.TabIndex = 7;
            // 
            // PasswordForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 264);
            Controls.Add(label2);
            Controls.Add(Key_TextBox);
            Controls.Add(label1);
            Controls.Add(IV_TextBox);
            Controls.Add(Cancel_Button);
            Controls.Add(OK_Button);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PasswordForm";
            Text = "Plase input key and IV";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OK_Button;
        private Button Cancel_Button;
        private TextBox IV_TextBox;
        private Label label1;
        private Label label2;
        private TextBox Key_TextBox;
    }
}