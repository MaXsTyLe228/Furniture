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

namespace FABRIKA
{
    public partial class AddProduct : Form
    {
        INFO.FUN func = new INFO.FUN();
        bool ed = false;int id = 0;
        public AddProduct()
        {
            InitializeComponent();
        }
        public void edit(int x)
        {
            ed = true;
            id = x;
            button1.Text = "Редактировать!";
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = func.GetBS("Fized");
            comboBox1.DisplayMember = "fiz";
            comboBox1.ValueMember = "id_fiz";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(func.nameDB);
            conn.Open();

            if (textBox1.Text.Length > 0)
            {
                string sql;
                if (!ed) {
                    sql = "INSERT INTO Product (prod,id_fiz,price,amount) " +
                    "values (@prod,@id_fiz,@price,@amount)";
                }
                else
                {
                    sql = $"Update Product SET prod=@prod,id_fiz=@id_fiz,price=@price,amount=@amount where id_prod={id}";
                }

                using (SqlCommand myCommand = new SqlCommand(sql, conn))
                {
                    myCommand.Parameters.AddWithValue("@prod", textBox1.Text);
                    myCommand.Parameters.AddWithValue("@id_fiz", comboBox1.SelectedValue);
                    myCommand.Parameters.AddWithValue("@price", (int)numericUpDown7.Value);
                    myCommand.Parameters.AddWithValue("@amount", (int)numericUpDown8.Value);
                    myCommand.ExecuteNonQuery();
                }
                


                if (!ed)
                {
                    id = func.GetMax("select * from Product", "id_prod");
                    sql = "INSERT INTO Calculate (id_prod,raw,amo,zp,upo,mat,zat) " +
                    "values (@id_prod,@raw,@amo,@zp,@upo,@mat,@zat)";
                }
                else
                {
                    sql = "Update Calculate SET raw=@raw,amo=@amo,zp=@zp,upo=@upo,mat=@mat,zat=@zat where id_prod=@id_prod";
                }
                
                using (SqlCommand myCommand = new SqlCommand(sql, conn))
                {
                    myCommand.Parameters.AddWithValue("@id_prod", id);
                    myCommand.Parameters.AddWithValue("@raw", numericUpDown1.Value);
                    myCommand.Parameters.AddWithValue("@amo", numericUpDown2.Value);
                    myCommand.Parameters.AddWithValue("@zp", numericUpDown3.Value);
                    myCommand.Parameters.AddWithValue("@upo", numericUpDown4.Value);
                    myCommand.Parameters.AddWithValue("@mat", numericUpDown5.Value);
                    myCommand.Parameters.AddWithValue("@zat", numericUpDown6.Value);
                    myCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Изменения успешно сохранены!");
            }
            else
            {
                MessageBox.Show("Введите название товара!");
            }

            if (ed) this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            numericUpDown6.Value = 0;
            numericUpDown7.Value = 1;
            numericUpDown8.Value = 0;
        }
    }
}
