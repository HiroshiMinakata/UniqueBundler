using System.Security.Cryptography;
using System.Text;

namespace UniqueBundler
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public byte[] key;
        public byte[] iv;

        private void OK_Button_Click(object sender, EventArgs e)
        {
            if (Key_TextBox.Text == "") return;
            if (IV_TextBox.Text == "") return;
            key = CreateKeyFromPassword(Key_TextBox.Text);
            iv = CreateIVFromPassword(IV_TextBox.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static byte[] CreateKeyFromPassword(string password, int keyBytes = 32)
        {
            var key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("KeyPass"), 10000);
            return key.GetBytes(keyBytes);
        }

        public static byte[] CreateIVFromPassword(string password, int blockSize = 16)
        {
            var key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("IVPass"), 10000);
            return key.GetBytes(blockSize);
        }
    }
}
