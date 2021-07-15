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
    public partial class AddContract : Form
    {
        INFO.FUN f = new INFO.FUN();
        public AddContract()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string nameDB = Properties.Settings.Default.Connect;
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();
                string sql = "INSERT INTO Dogovor (id_ord,date_d,id_otp,id_op) " +
                    "values (@id_ord,@date_d,@id_otp,@id_op)";

            using (SqlCommand myCommand = new SqlCommand(sql, conn))
            {
                myCommand.Parameters.AddWithValue("@id_ord", comboBox1.SelectedValue);
                myCommand.Parameters.AddWithValue("@date_d", DateTime.Now);
                myCommand.Parameters.AddWithValue("@id_otp", comboBox2.SelectedValue);
                myCommand.Parameters.AddWithValue("@id_op", comboBox3.SelectedValue);
                myCommand.ExecuteNonQuery();
            }

            int mm = f.GetMax("select id_dog from Dogovor","id_dog");
            AddZakaz form = new AddZakaz(mm);
            form.ShowDialog();
        }

        private void AddContract_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = f.GetBS("Ordered");
            comboBox1.DisplayMember = "ordered";
            comboBox1.ValueMember = "id_ord";

            comboBox2.DataSource = f.GetBS("Otpravka");
            comboBox2.DisplayMember = "otpr";
            comboBox2.ValueMember = "id_otp";

            comboBox3.DataSource = f.GetBS("Oplata");
            comboBox3.DisplayMember = "opl";
            comboBox3.ValueMember = "id_op";
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
