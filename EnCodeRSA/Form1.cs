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
    public partial class Form1 : Form
    {
        RSACryptoServiceProvider rsa;
        public Form1()
        {
            InitializeComponent();
            rsa = new RSACryptoServiceProvider();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy khóa công khai từ đối tượng RSA
                string publicKey = rsa.ToXmlString(false);

                // Lấy dữ liệu cần mã hóa từ TextBox thứ nhất
                string plainText = txtInput.Text;

                // Mã hóa dữ liệu bằng khóa công khai
                string encryptedText = EncryptData(plainText, publicKey);

                // Gán chuỗi đã mã hóa vào TextBox thứ hai
                txtEncrypted.Text = encryptedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                txtInput.Text = "";
                txtEncrypted.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // Tạo một instance mới của form mới
            GiaiMa newForm = new GiaiMa(rsa);

            // Hiển thị form mới
            newForm.Show();
        }

        private string EncryptData(string data, string publicKey)
        {
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                byte[] encryptedData = rsa.Encrypt(dataToEncrypt, false);
                return Convert.ToBase64String(encryptedData);
            }
        }
    }
}
