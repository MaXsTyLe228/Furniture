using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FABRIKA.ADD
{
    public partial class AddZakaz : Form
    {
        public int id; bool start = false;
        INFO.FUN f = new INFO.FUN();
        public AddZakaz(int i)
        {
            InitializeComponent();
            id = i;
        }

        private void AddZakaz_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = f.GetBS("Product"); start = false;
            comboBox1.DisplayMember = "prod"; start = false;
            comboBox1.ValueMember = "id_prod"; start = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (start)
            {
                int k = (int)comboBox1.SelectedValue;
                int i = f.GetSum($"select * from Product where id_prod={k}", "amount");
                numericUpDown1.Value = numericUpDown1.Minimum;
                numericUpDown1.Maximum = i;
            }
            else start = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (numericUpDown1.Maximum > 1)
            {
                string nameDB = Properties.Settings.Default.Connect;
                SqlConnection conn = new SqlConnection(nameDB);
                conn.Open();
                string sql = "INSERT INTO Zakaz (id_dog,id_prod,amount) " +
                    "values (@id_dog,@id_prod,@amount)";

                using (SqlCommand myCommand = new SqlCommand(sql, conn))
                {
                    myCommand.Parameters.AddWithValue("@id_dog", id);
                    myCommand.Parameters.AddWithValue("@id_prod", comboBox1.SelectedValue);
                    myCommand.Parameters.AddWithValue("@amount", (int)numericUpDown1.Value);
                    myCommand.ExecuteNonQuery();
                }

                sql = "Update Product SET amount=amount-@x1 " +
                    "where id_prod=@x2";

                using (SqlCommand myCommand = new SqlCommand(sql, conn))
                {
                    myCommand.Parameters.AddWithValue("@x1", numericUpDown1.Value);
                    myCommand.Parameters.AddWithValue("@x2", comboBox1.SelectedValue);
                    myCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Товар был успешно записан в договор!","Добавлено!");
            }

            numericUpDown1.Value = 1;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int k = (int)comboBox1.SelectedValue;
            int i = f.GetSum($"select * from Product where id_prod={k}", "amount");
            numericUpDown1.Maximum = i;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
