using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace Resturant
{
    public partial class DeliveryBoy_admin_form : UserControl
    {
        static bool Show_panel = false; // to show hide the panel
        static bool reload_gridview = false;
        static DelevaryBoy_file D_file;
        public const string file_delv_path = "Delivery Boy.xml";
        XmlDocument doc;
        XmlDataDocument xmldata;
        public DeliveryBoy_admin_form()
        {
            InitializeComponent();
            Hide_panel();
        }
        void show_panl()
        {
            if (Show_panel == false)
            {
                panel1.Show();
                Show_panel = true;
            }
        }
        public void Hide_panel()
        {
            panel1.Hide();
            clear_panel();
            Show_panel = false;
        }
        private void clear_panel()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
          //  textBox4.Clear();
            textBox1.Focus();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                xmldata = new XmlDataDocument();
                xmldata.DataSet.ReadXml(Application.StartupPath + "\\Delivery Boy.xml");
                dataGridView_boy.DataSource = xmldata.DataSet;
                dataGridView_boy.DataMember = "DeliveryBoy";
            }
            catch (Exception)
            {
                MessageBox.Show("NO data to show");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            show_panl();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide_panel();
        }

        private void Done_Click(object sender, EventArgs e)
        {
            D_file = new DelevaryBoy_file();

            //D_file.idboy = textBox1.Text;
            //D_file.name_boy= textBox2.Text;
            //D_file.phone = textBox3.Text;
            //D_file.areacode = textBox4.Text;
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" &&comboBox1.Text != "Area code")
            {
                if (D_file.check(textBox1.Text))
                {
                   bool Return = D_file.AddBoy(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.SelectedItem.ToString());
                   if (Return == true)
                   {
                       MessageBox.Show("Done succefuly");
                       clear_panel();
                       Hide_panel();
                       // MessageBox.Show("Done !", "Text_Box", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
                   else { MessageBox.Show("some thing goes wrong with the database"); }
                }
                else { MessageBox.Show("id is exist"); }
            }
            else { MessageBox.Show("please enter all the data"); clear_panel(); }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            // doc = new XmlDocument();
            D_file = new DelevaryBoy_file();
            if (txt_delete.Text == "")
            {
                MessageBox.Show("please enter the data");
            }
            else
            {
                if (D_file.delete_node(txt_delete.Text))
                {
                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("no connection with data base");
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char charr = e.KeyChar;
            if (!char.IsDigit(charr) && charr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Cannot be Characters");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char charr = e.KeyChar;
            if (char.IsDigit(charr) && charr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Cannot be number");
            }
        }
    }
}

