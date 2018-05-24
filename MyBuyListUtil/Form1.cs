using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBuyListUtil
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MBLService.Service mblService = new MBLService.Service();
            DataSet ds = mblService.GetUsers();
            dataGridView1.DataSource = ds.Tables["Users"];
        }
    }
}
