using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnCodeRSA
{
    public partial class GiaiMa : Form
    {
        RSACryptoServiceProvider rsa;
        public GiaiMa(RSACryptoServiceProvider key)
        {
            InitializeComponent();
            rsa = key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtEncrypted.Text = "";
            txtDecrypted.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu đã mã hóa từ TextBox thứ hai
                string encryptedText = txtEncrypted.Text;

                // Giải mã dữ liệu và gán kết quả vào TextBox thứ ba
                txtDecrypted.Text = DecryptData(encryptedText, rsa.ToXmlString(true));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private string DecryptData(string encryptedText, string privateKey)
        {
            try
            {
                // Chuyển đổi chuỗi Base64 thành mảng byte đã mã hóa
                byte[] encryptedData = Convert.FromBase64String(encryptedText);

                // Giải mã dữ liệu bằng khóa riêng tư
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(privateKey);
                    byte[] decryptedData = rsa.Decrypt(encryptedData, false);
                    return Encoding.UTF8.GetString(decryptedData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi giải mã dữ liệu: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Tạo một instance mới của form mới
            Form1 newForm = new Form1();

            // Hiển thị form mới
            newForm.Show();
        }
    }
}
