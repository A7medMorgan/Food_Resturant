using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resturant
{
    public partial class Admin_form : Form
    {
        public Admin_form()
        {
            InitializeComponent();
            Slide.Height = button1.Height;
            Slide.Top = button1.Top;
           food_admin_form1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Slide.Height = button1.Height;
            Slide.Top = button1.Top;
            food_admin_form1.Hide_panel();
           food_admin_form1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Slide.Height = button2.Height;
            Slide.Top = button2.Top;
           deliveryBoy_admin_form1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login_admin login = new Login_admin();
            login.Show();
            this.Hide();
        }

    }
}
