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
    public partial class user_form : Form
    {
        private  File_Food file_food;
        DataTable Data;
        public static List<string[]> Data_list;
        public static List<string[]> list_food;
        public  List<int> selected_row = new List<int>();
        int o;
        private static string[] record_global;
        private static string[] record_order;
        private string name_customer;
        private string phone;
        private string address;
        private string Area_code;
        int counter_one_item = 0;
        public user_form(string [] information)
        {
            InitializeComponent();
            name_customer=information[0];
            phone = information[1];
            address = information[2];
            Area_code = information[3];
            load_list_foad();
            hide_panel();
            textBox4.Hide();
            list_food = new List<string[]>();
        }
        bool check_item()
        {
            int i = 0;
            for (int q = 0; q < selected_row.Count; q++)
            {
                if (o == selected_row[q])
                {
                    i = 1;
                }
            
            }
            if (i == 1) { i = 0;  return true; }
            else { return false; }
           
        }
        void total_cost()
        {
            int p =int.Parse(textBox2.Text);
            string no = textBox3.Text;
            int n = int.Parse(no);
            textBox4.Text = (p * n).ToString();
        }

        void hide_panel()
        {
            panel1.Hide();
            textBox1.Clear();
            textBox2.Clear();
        }
        void set_Gridview()
        {
            Data.Columns.Add("Name", typeof(string));
            Data.Columns.Add("price", typeof(string));
            Data.Columns.Add("Quantity", typeof(string));
            Data.Columns.Add("Discount", typeof(string));
            Data.Columns.Add("ID", typeof(string));
            List_food.DataSource = Data;
        }
        void load_list_foad()
        {
            Data = new DataTable();
            set_Gridview();
            file_food = new File_Food();
            Data_list = file_food.Load(); //load the file
            string id, name, price, quantity, discount; // to recive the data
            for (int i = 0; i < Data_list.Count; i++)
            {
                record_global = Data_list[i];
                id = record_global[0];
                name = record_global[1];
                price = record_global[2];
                quantity = record_global[3];
                discount = record_global[4];
                Data.Rows.Add(name, price, quantity, discount,id);
            }
            List_food.DataSource = Data; //set the table
        }
        private void back_Click(object sender, EventArgs e)
        {
            Main_form main = new Main_form();
            main.Show();
            this.Hide();
        }

        private void List_food_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            record_order = new string[5];
            o=int.Parse(e.RowIndex.ToString());
            record_order[0]=List_food.CurrentRow.Cells[0].Value.ToString(); //name
            record_order[1]=List_food.CurrentRow.Cells[1].Value.ToString(); //price
            record_order[2]=List_food.CurrentRow.Cells[2].Value.ToString(); //Quantity
            record_order[3]=List_food.CurrentRow.Cells[3].Value.ToString(); //discount
            record_order[4]=List_food.CurrentRow.Cells[4].Value.ToString(); //id
            if (check_item())
            {
                MessageBox.Show("you have added it");
                hide_panel();
            }
            else
            {
                panel1.Show();
                textBox3.Focus();
                textBox1.Text = record_order[0];
                textBox2.Text = record_order[1];
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

        private void button3_Click(object sender, EventArgs e)
        {
            int i =int.Parse(textBox3.Text);
            int ii =int.Parse(record_order[2]);
            if (i > ii)
            {
                MessageBox.Show("Error");
            }
            else
            {
                textBox4.Show();
                total_cost();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i =int.Parse(textBox3.Text);
            int ii = int.Parse(record_order[2]);
            if (i < 1 || i > ii)
            {
                MessageBox.Show("Error");
            }
            else
            {
                counter_one_item += 1;
                // complete the quatity that i write
                record_order[2] = textBox3.Text;
                list_food.Add(record_order);
                label1.Text =(counter_one_item ).ToString();
                selected_row.Add(o);
                hide_panel();
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (list_food.Count == 0) { MessageBox.Show("Select item"); }
            else
            {
                string[] info = { name_customer, phone, address, Area_code };
                Order_DeliveryBoy order = new Order_DeliveryBoy(list_food, info);
                order.Show();
                this.Hide();
            }
        }
    }
}
