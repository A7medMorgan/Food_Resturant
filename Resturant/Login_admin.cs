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
    public partial class Login_admin : Form
    {
        public Login_admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin ADMIN = new Admin();
            string email = textBox1.Text;
            string pass = textBox2.Text;
            if (ADMIN.login(email, pass))
            {
                Admin_form form_open = new Admin_form();
                this.Hide();
                form_open.Show();
            }
            else 
            {
                MessageBox.Show("Email or Password Inccorect");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main_form Main = new Main_form();
            Main.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
