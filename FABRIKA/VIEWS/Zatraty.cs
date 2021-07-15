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
    public partial class Zatraty : Form
    {
        public Zatraty()
        {
            InitializeComponent();
        }
        INFO.FUN funct = new INFO.FUN();

        void rename(string[] name)
        {
            for (int i = 0; i < name.Length; i++)
                dataGridView1.Columns[i].HeaderText = name[i];
        }
        private void Zatraty_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = funct.GetBSQuery("select prod,raw,amo,zp,upo,mat,zat from Calculate,Product where Calculate.id_prod = Product.id_prod");
            string[] name = {"Товар","Сырье(грн)", "Амортизация(грн)", "Зарплата(грн)", "Упаковка(грн)", "Материалы(грн)", "Другие затраты(грн)" };
            rename(name);
        }
    }
}
