using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FABRIKA.ADD
{
    public partial class AddClient : Form
    {
        public AddClient()
        {
            InitializeComponent();
        }
        INFO.FUN func = new INFO.FUN();int ii = 0;

        public void edit(string ord,int x=0)
        {
            ii = x;
            textBox1.Enabled = false;
            textBox1.Text = ord;
            button1.Text = "Редактировать";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Если такой уже есть
            // Если поля пустые

            proverka();
        }

        bool proverka()
        {
            if (textBox1.Text == "") { label1.ForeColor = Color.Red; return false; }
            else label1.ForeColor = Color.Black;

            if (textBox2.Text == "") { label2.ForeColor = Color.Red; return false; }
            else label2.ForeColor = Color.Black;

            if (textBox3.Text == "") { label4.ForeColor = Color.Red; return false; }
            else label4.ForeColor = Color.Black;

            if (comboBox1.Text == "") { label3.ForeColor = Color.Red; return false; }
            else label3.ForeColor = Color.Black;

            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (proverka())
            {
                string x1, x2, x3, x4;
                x1 = textBox1.Text;
                x2 = textBox2.Text;
                x3 = comboBox1.Text;
                x4 = textBox3.Text;
                if (textBox1.Enabled) func.Add(x1, x2, x3, x4, !textBox1.Enabled);
                else func.Add(x1, x2, x3, x4, !textBox1.Enabled, ii);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (!char.IsLetter(c) && c != 8 && !Char.IsPunctuation(c)) 
                e.Handled = true;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (!char.IsLetterOrDigit(c) && c != 8)
                e.Handled = true;
        }

        private void AddClient_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = func.GetBS("Bank");
            comboBox1.DisplayMember = "bank_n";
        }
    }
}
