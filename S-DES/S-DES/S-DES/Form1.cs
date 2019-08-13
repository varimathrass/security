using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S_DES
{
    public partial class Form1 : Form
    {
        private String tb1Text = "";
        private String tb4Text = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == tb1Text)
            {
                return;
            }
            if (textBox1.Text == "" && tb1Text != "")
            {
                tb1Text = textBox1.Text;
                return;
            }

            char value = textBox1.Text[textBox1.Text.Length - 1];

            if (CipherSupport.isBit(value) )
            {
                tb1Text = textBox1.Text;
                return;
            }
            else
            {
                textBox1.Text = tb1Text;
            }

            tb1Text = textBox1.Text;
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Keys keys = new Keys(textBox1.Text);

            keys.generateRoundKeys();

            textBox2.Text = keys.Key1;
            textBox3.Text = keys.Key2;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == tb4Text)
            {
                return;
            }
            if (textBox4.Text == "" && tb4Text != "")
            {
                tb4Text = textBox4.Text;
                return;
            }

            char value = textBox4.Text[textBox4.Text.Length - 1];

            if (CipherSupport.isBit(value) )
            {
                tb4Text = textBox4.Text;
                return;
            }
            else
            {
                textBox4.Text = tb4Text;
            }

            tb4Text = textBox4.Text;
            textBox4.SelectionStart = textBox4.Text.Length;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CipherSDES plainText = new CipherSDES(textBox4.Text, textBox2.Text, textBox3.Text);
            textBox5.Text = plainText.makeAction("Encrypt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CipherSDES cipherText = new CipherSDES(textBox5.Text, textBox2.Text, textBox3.Text);
            textBox6.Text = cipherText.makeAction("Decrypt");
        }
    }
}
