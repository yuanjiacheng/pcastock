using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockSwitch
{
    public partial class Reslut : Form
    {
        labrary.DataHelper DH = new labrary.DataHelper();
        public Reslut()
        {
            InitializeComponent();
        }

        private void Reslut_Load(object sender, EventArgs e)
        {
            string result = DH.GetResult();
            Lab_Result.Text = result;
        }

        private void Btn_Fresh_Click(object sender, EventArgs e)
        {
            string result = DH.GetResult();
            Lab_Result.Text = result;
        }
    }
}
