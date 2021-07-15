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
    public partial class GlavnoeMenu : Form
    {
        public GlavnoeMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VIEWS.Clients form = new VIEWS.Clients();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VIEWS.Products form = new VIEWS.Products();
            form.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            VIEWS.Zatraty form = new VIEWS.Zatraty();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VIEWS.Contracts form = new VIEWS.Contracts();
            form.ShowDialog();
        }

        private void GlavnoeMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REPORTS.Reports form = new REPORTS.Reports();
            form.ShowDialog();
        }
    }
}
