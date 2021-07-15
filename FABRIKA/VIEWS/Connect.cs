using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FABRIKA
{
    public partial class Connect : Form
    {
        public Connect()
        {
            InitializeComponent();
        }

        private string filename="", connectionStr;

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                textBox1.Text = filename;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(filename.Length>3)
            if (filename[filename.Length - 1] == 'f')
            {
                connectionStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + filename + @""";Integrated Security=True;Connect Timeout=30";
                Properties.Settings.Default["Connect"] = connectionStr;
                Properties.Settings.Default.Save();
                MessageBox.Show("Подключено");
                this.Hide();
                GlavnoeMenu form2 = new GlavnoeMenu();
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Некоректный ввод пути!");
            }
        }
    }
}
