using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RSA
{
    public partial class Form1 : Form
    {
        private String tbText = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader messageFile = new StreamReader(openFileDialog1.FileName))
                {
                    String message = messageFile.ReadLine();
                    CipherRSA RSA = new CipherRSA(long.Parse(textBox1.Text), long.Parse(textBox2.Text), message);

                    using (StreamWriter encrMessageFile = new StreamWriter("EncryptedMessage.txt"))
                    {
                        RSA.Encrypt(encrMessageFile);
                    }                  
                }
            }

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text == tbText)
            {
                return;
            }
            if (tb.Text == "" && tbText != "")
            {
                tbText = tb.Text;
                return;
            }

            char value = tb.Text[tb.Text.Length - 1];

            if (int.TryParse(value.ToString(), out int n))
            {
                tbText = tb.Text;
                return;
            }
            else
            {
                tb.Text = tbText;
            }

            tbText = tb.Text;
            tb.SelectionStart = tb.Text.Length;
        }

        private void pqTB_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tbText = tb.Text;
        }

        private void pqTB_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tbText == "")
            {
                return;
            }

            long number = int.Parse(tbText);

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    MessageBox.Show("Введене число - не просте, p і q мають бути простими числами! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbText = "";
                    tb.Text = "";

                    return;
                }
            }

            tbText = "";
        }

        private void button2_Click(object sender, EventArgs a)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader encrMessageFile = new StreamReader(openFileDialog1.FileName))
                {
                    String[] value = new String[4];
                    value = encrMessageFile.ReadLine().Split(',');

                    long n = long.Parse(value[0]);
                    long e = long.Parse(value[1]);
                    long message = long.Parse(value[2]);
                    long sign = long.Parse(value[3]);

                    CipherRSA cipherRSA = new CipherRSA();

                    textBox3.Text = cipherRSA.checkEncryptedMessage(n, e, message, sign).ToString();
                } 
            }
        }
    }
}
