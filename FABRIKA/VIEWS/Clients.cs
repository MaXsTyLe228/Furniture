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
    public partial class Clients : Form
    {
        private Size small = new Size(650, 329);
        private Size big = new Size(650, 454);
        private bool bigForm = false;
        private INFO.FUN function = new INFO.FUN();


        public Clients()
        {
            InitializeComponent();
            Size = small;
        }

        void load()
        {
            string sql = $"select * from Ordered";
            dataGridView1.DataSource = function.GetBSQuery(sql);
            string[] name = { "Код", "Заказчик", "Адресс", "Банк", "Р/с" };
            rename(name);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bigForm = !bigForm;
            if(bigForm) Size = big;
            else Size = small;
        }

        void rename(string[] name)
        {
            for (int i = 0; i < name.Length; i++)
                dataGridView1.Columns[i].HeaderText = name[i];
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ADD.AddClient form = new ADD.AddClient();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ADD.AddClient form = new ADD.AddClient();
            string ord = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            int ii = (int)dataGridView1.CurrentRow.Cells[0].Value;
            MessageBox.Show(ord);
            form.edit(ord,ii);
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            if (MessageBox.Show("Вы уверены, что хотите удалить заказчика?", "Удаление", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                function.DeleteFrom(id, "Ordered", "id_ord");
                function.DeleteFrom(id, "Dogovor", "id_ord");
            }
                
            else MessageBox.Show("Действие отменено!", "Отменено");
            load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            load();
        }



        void updateInfo()
        {
            string x1, x2, x3, x4, x5, x6,sql;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            x1 = function.GetCount($"select * from Dogovor Where Dogovor.id_ord={id}").ToString();

            sql = $"select ((price*Zakaz.amount)) as \"SUM\"  " +
                $"from Zakaz,Dogovor,Product,Calculate Where Dogovor.id_ord={id} " +
                $"and Dogovor.id_dog=Zakaz.id_dog and Product.id_prod=Zakaz.id_prod" +
                $" and Calculate.id_prod = Product.id_prod";
            x2 = function.GetSum(sql,"SUM").ToString();
            x3 = function.GetMax(sql, "SUM").ToString();
            x6 = function.GetMin(sql, "SUM").ToString();
            x5 = function.GetMinDate($"select * from Dogovor Where Dogovor.id_ord={id}", "date_d").ToString();
            x4 = function.GetMaxDate($"select * from Dogovor Where Dogovor.id_ord={id}", "date_d").ToString();

            label1.Text = $"Заключено договоров: {x1}";
            label2.Text = $"Сумма договоров: {x2}грн";
            label3.Text = $"Самый дорогой: {x3}грн";
            label6.Text = $"Самый недорогой: {x6}грн";
            label5.Text = $"Первый договор: {x5}";
            label4.Text = $"Крайний договор: {x4}";
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updateInfo();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
