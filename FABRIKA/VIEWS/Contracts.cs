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
    public partial class Contracts : Form
    {
        public Contracts()
        {
            InitializeComponent();
        }
        INFO.FUN funct = new INFO.FUN();

        void obnova()
        {
            
            int id = 0; if(dataGridView1.CurrentCell!=null)
                id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            string x1 = $"select Dogovor.id_dog, ordered,date_d,opl,otpr from Dogovor,Oplata,Otpravka,Ordered " +
                 $"where Dogovor.id_ord=Ordered.id_ord and Oplata.id_op=Dogovor.id_op" +
                 $" and Otpravka.id_otp=Dogovor.id_otp";
            if (checkBox1.Checked) x1 += $" and Dogovor.id_ord = {comboBox1.SelectedValue}";

            string zakaz = $"select Dogovor.id_dog,prod,Zakaz.amount,((price*Zakaz.amount)) " +
                $"from Zakaz,Product,Dogovor,Calculate " +
                $"Where Zakaz.id_prod=Product.id_prod and Dogovor.id_dog=Zakaz.id_dog " +
                $"and Dogovor.id_dog={id} and Calculate.id_prod = Product.id_prod ";
            if (checkBox1.Checked) zakaz += $" and Dogovor.id_ord = {comboBox1.SelectedValue}";
            if (checkBox2.Checked) zakaz += $" and Zakaz.id_prod = {comboBox2.SelectedValue}";

            dataGridView1.DataSource = funct.GetBSQuery(x1);
            dataGridView2.DataSource = funct.GetBSQuery(zakaz);

            string[] name = { "Договор №", "Заказчик","Дата заключения","Оплата","Отправка"};
            string[] name1 = { " ","Товар", "Количество","Цена"};
            rename(name,name1);
        }

        void rename(string[] name, string[] name1)
        {
            for (int i = 0; i < name.Length; i++)
                dataGridView1.Columns[i].HeaderText = name[i];

            for (int i = 0; i < name1.Length; i++)
                dataGridView2.Columns[i].HeaderText = name1[i];
        }

        private void Contracts_Load(object sender, EventArgs e)
        {
            obnova();
            dataGridView2.Columns[0].Visible = false;

            comboBox1.DataSource = funct.GetBS("Ordered");
            comboBox2.DataSource = funct.GetBS("Product");
            comboBox1.DisplayMember = "ordered";
            comboBox1.ValueMember = "id_ord";
            comboBox2.DisplayMember = "prod";
            comboBox2.ValueMember = "id_prod";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox1.Checked;
            obnova();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = checkBox2.Checked;
            obnova();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            obnova();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            obnova();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ADD.AddContract form = new ADD.AddContract();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            funct.DeleteFrom(id, "Zakaz", "id_dog");
            funct.DeleteFrom(id, "Dogovor", "id_dog");
            MessageBox.Show("Данные о договоре, и заказах связаных с договором, были успешно удалены!","Удалено!");
            obnova();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Доход form = new Доход();
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = 0; if (dataGridView1.CurrentCell != null)
                id = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (dataGridView1.CurrentCell != null)
                {
                    string zakaz = $"select Dogovor.id_dog,prod,Zakaz.amount,((price*Zakaz.amount)) " +
                    $"from Zakaz,Product,Dogovor,Calculate " +
                $"Where Zakaz.id_prod=Product.id_prod and Dogovor.id_dog=Zakaz.id_dog " +
                $"and Dogovor.id_dog={id} and Calculate.id_prod = Product.id_prod ";
                    if (checkBox1.Checked) zakaz += $" and Dogovor.id_ord = {comboBox1.SelectedValue}";
                    if (checkBox2.Checked) zakaz += $" and Zakaz.id_prod = {comboBox2.SelectedValue}";

                    dataGridView2.DataSource = funct.GetBSQuery(zakaz);

                    string[] name = { "Договор №", "Заказчик", "Дата заключения", "Оплата", "Отправка" };
                    string[] name1 = { " ", "Товар", "Количество","Цена (грн)"};
                    rename(name, name1);
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            obnova();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int id = 0; if (dataGridView1.CurrentCell != null)
                id = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (dataGridView1.CurrentCell != null)
                {
                    string zakaz = $"select Dogovor.id_dog,prod,Zakaz.amount,((price*Zakaz.amount)) " +
                    $"from Zakaz, Product, Dogovor, Calculate " + 
                $"Where Zakaz.id_prod=Product.id_prod and Dogovor.id_dog=Zakaz.id_dog " +
                $"and Dogovor.id_dog={id} and Calculate.id_prod = Product.id_prod ";
                    if (checkBox1.Checked) zakaz += $" and Dogovor.id_ord = {comboBox1.SelectedValue}";
                    if (checkBox2.Checked) zakaz += $" and Zakaz.id_prod = {comboBox2.SelectedValue}";

                    dataGridView2.DataSource = funct.GetBSQuery(zakaz);

                    string[] name = { " ", "Договор №", "Дата заключения", "Оплата", "Отправка" };
                    string[] name1 = { " ", "Товар", "Количество" };
                    rename(name, name1);
                }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
