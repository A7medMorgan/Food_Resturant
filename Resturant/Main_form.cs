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
    public partial class Main_form : Form
    {
        private string [] information;
        public Main_form()
        {
            InitializeComponent();
        }
        void get_data()
        {
            information = new string[4];
            information[0] = textBox1.Text;
            information[1] = textBox2.Text;
            information[2] = textBox3.Text;
            information[3] = comboBox1.SelectedItem.ToString();
        }

        private void Admin_Click(object sender, EventArgs e)
        {
            Login_admin Admin = new Login_admin();
            Admin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "Area code")
            {
                get_data();
                user_form user = new user_form(information);
                user.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("you should enter all your informtion");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char charr = e.KeyChar;
            if (!char.IsDigit(charr) && charr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Cannot be Characters");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char charr = e.KeyChar;
            if (char.IsDigit(charr) && charr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Cannot be number");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
