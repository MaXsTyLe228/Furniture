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
    public partial class Доход : Form
    {
        public Доход()
        {
            InitializeComponent();
        }
        INFO.FUN func = new INFO.FUN();
        void upload()
        {
            string sql = $"select Dogovor.id_dog as \"Код договора\",prod as \"Товар\",Zakaz.amount" +
                $" as \"Кол-во\",((price*Zakaz.amount)-((raw+amo+zp+upo+mat+zat)*Zakaz.amount)) as \"Сума (грн)\", date_d as \"Дата\" " +
                $"from Zakaz,Product,Dogovor, Calculate Where Product.id_prod=Calculate.id_prod " +
                $"and Zakaz.id_prod=Product.id_prod and Dogovor.id_dog=Zakaz.id_dog";
            string sql1 = $"select ((price*Zakaz.amount)-((raw+amo+zp+upo+mat)*Zakaz.amount)) as \"SUME\" " +
                $"from Zakaz,Product,Dogovor, Calculate Where Product.id_prod=Calculate.id_prod " +
                $" and Zakaz.id_prod=Product.id_prod and Dogovor.id_dog=Zakaz.id_dog";
            DateTime x1 = dateTimePicker1.Value;
            DateTime x2 = dateTimePicker2.Value;
            DateTime x3 = dateTimePicker3.Value;
            if (checkBox3.Checked)
            {
                sql += $" and date_d = '{x3.Year}-{x3.Month}-{x3.Day}'";
                sql1 += $" and date_d = '{x3.Year}-{x3.Month}-{x3.Day}'";
            }
            else
            {
                if (checkBox1.Checked) { sql += $" and date_d >= '{x1.Year}-{x1.Month}-{x1.Day}'"; sql1 += $" and date_d >= '{x1.Year}-{x1.Month}-{x1.Day}'"; }
                if (checkBox2.Checked) { sql += $" and date_d <= '{x2.Year}-{x2.Month}-{x2.Day}'"; sql1 += $" and date_d <= '{x2.Year}-{x2.Month}-{x2.Day}'"; }
            }

            int x = func.GetSum(sql1,"SUME");
            dataGridView1.DataSource = func.GetBSQuery(sql);
            label1.Text = $"Доход: {x}грн";
        }

        private void Доход_Load(object sender, EventArgs e)
        {
            upload();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            upload();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            upload();
        }
    }
}
