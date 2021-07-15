using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FABRIKA.VIEWS
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }
        INFO.FUN funct = new INFO.FUN();
        int summ(string[] arr)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
                sum += Convert.ToInt32(arr[i]);
            return sum;
        }
        void rename(string[] name)
        {
            for (int i = 0; i < name.Length; i++)
                dataGridView1.Columns[i].HeaderText = name[i];
        }
        string getValue(string column, int id)
        {
            string sql = $"select {column} from Calculate where id_prod = {id}";
            comboBox1.DataSource = funct.GetBSQuery(sql);
            comboBox1.DisplayMember = column;
            comboBox1.ValueMember = column;
            return comboBox1.SelectedValue.ToString();
        }
        void calculate()
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string x1, x2, x3, x4, x5, x6; int x7;
            string[] name = { "raw", "amo", "zp", "upo", "mat", "zat"};

            x1 = getValue(name[0], id);
            x2 = getValue(name[1], id);
            x3 = getValue(name[2], id);
            x4 = getValue(name[3], id);
            x5 = getValue(name[4], id);
            x6 = getValue(name[5], id);

            string[] arr = { x1, x2, x3, x4, x5, x6 };
            x7 = summ(arr);

            label2.Text = $"Сырье - {x1}грн";
            label3.Text = $"Амортизация - {x2}грн";
            label4.Text = $"Зарплата - {x3}грн";
            label5.Text = $"Упаковка - {x4}грн";
            label6.Text = $"Материалы - {x5}грн";
            label7.Text = $"Другие затраты - {x6}грн";
            label8.Text = $"Итого - {x7}грн";
        }
        void load()
        {
            string sql = "select id_prod, prod,fiz,price,amount from Product,Fized Where Product.id_fiz=Fized.id_fiz ";
            int min = Convert.ToInt32(numericUpDown1.Value);
            int max = Convert.ToInt32(numericUpDown2.Value);
            if (checkBox1.Checked) sql += $"and price >= {min}";
            if (checkBox2.Checked) sql += $"and price <= {max}";
            dataGridView1.DataSource = funct.GetBSQuery(sql);
            dataGridView1.Columns[0].Visible = false;

            string[] name = { "Код", "Товар", "Физ ед", "Цена (грн)", "Кол-во на складе" };
            for (int i = 0; i < name.Length; i++)
                dataGridView1.Columns[i].HeaderText = name[i];

        }

        private void Products_Load(object sender, EventArgs e)
        {
            load();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            load();
            numericUpDown1.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            load();
            numericUpDown2.Enabled = checkBox2.Checked;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            calculate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            load();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                string[] name = { "raw", "amo", "zp", "upo", "mat", "zat" };

                string x1 = getValue(name[0], id);
                string x2 = getValue(name[1], id);
                string x3 = getValue(name[2], id);
                string x4 = getValue(name[3], id);
                string x5 = getValue(name[4], id);
                string x6 = getValue(name[5], id);

                string[] arr = { x1, x2, x3, x4, x5, x6 };
                int x7 = summ(arr);
                if(Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value) < x7)
                {
                    funct.DeleteFrom(id, "Calculate", "id_prod");
                    funct.DeleteFrom(id, "Product", "id_prod");
                }

                load();
            }

            MessageBox.Show("Готово!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                string[] name = { "raw", "amo", "zp", "upo", "mat", "zat" };

                string x1 = getValue(name[0], id);
                string x2 = getValue(name[1], id);
                string x3 = getValue(name[2], id);
                string x4 = getValue(name[3], id);
                string x5 = getValue(name[4], id);
                string x6 = getValue(name[5], id);

                string[] arr = { x1, x2, x3, x4, x5, x6 };
                int x7 = summ(arr);
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value) > x7)
                    dataGridView1.Rows[i].Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Visible = true;
            }
        }

        private void добавитьТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProduct form = new AddProduct();
            form.ShowDialog();
        }

        private void обновитьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(dataGridView1.CurrentCell != null)
            {
                if(e.KeyChar == 8)
                {
                    int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    funct.DeleteFrom(id, "Calculate", "id_prod");
                    funct.DeleteFrom(id, "Product", "id_prod");
                    load();
                }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                funct.DeleteFrom(id, "Zakaz", "id_prod");
                funct.DeleteFrom(id, "Calculate", "id_prod");
                funct.DeleteFrom(id, "Product", "id_prod");
            }
            load();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                AddProduct form = new AddProduct();
                form.edit(id);
                form.ShowDialog();
            }

            load();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
