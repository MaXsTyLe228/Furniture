using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FABRIKA.REPORTS
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }
        INFO.FUN fun = new INFO.FUN();
        bool flag = false;
        private void Reports_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = fun.GetBS("Dogovor");
            comboBox1.DisplayMember = "id_dog";
            comboBox1.ValueMember = "id_dog";
            dataSet1.EnforceConstraints = false;
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.DataTable3". При необходимости она может быть перемещена или удалена.
            this.dataTable3TableAdapter.Fill(this.dataSet1.DataTable3);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.DataTable2". При необходимости она может быть перемещена или удалена.
            this.dataTable2TableAdapter.Fill(this.dataSet1.DataTable2);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.DataTable1". При необходимости она может быть перемещена или удалена.
            this.dataTable1TableAdapter.Fill(this.dataSet1.DataTable1);

            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer3.RefreshReport();
            flag = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                dataTable1BindingSource.Filter = $"id_dog = {comboBox1.SelectedValue}";
                this.dataTable1TableAdapter.Fill(this.dataSet1.DataTable1);
            }

            this.reportViewer1.RefreshReport();
        }
    }
}
