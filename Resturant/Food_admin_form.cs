using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resturant
{
    public partial class Food_admin_form : UserControl
    {
        static bool Show_panel = false; // to show hide the panel
        static bool reload_gridview = false;
        private static File_Food file_food;
        DataTable Data = new DataTable();
        public static List<string[]> Data_list;
        private static string[] record;
        public Food_admin_form()
        {
            InitializeComponent();
            Delete.Hide();

        }
        void set_Gridview()
        {
            Data.Columns.Add("count", typeof(int));
            Data.Columns.Add("ID", typeof(string));
            Data.Columns.Add("Name", typeof(string));
            Data.Columns.Add("price", typeof(string));
            Data.Columns.Add("Quantity", typeof(string));
            Data.Columns.Add("Discount", typeof(string));
            dataGridView1.DataSource = Data;
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
            textBox4.Clear();
            textBox5.Clear();
            textBox1.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            show_panl();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide_panel();
        }

        private void Done_Click(object sender, EventArgs e)
        {

            file_food = new File_Food();

            file_food.ID = textBox1.Text;
            file_food.Name = textBox2.Text;
            file_food.Price = textBox3.Text;
            file_food.No_item = textBox4.Text;
            file_food.Discount = textBox5.Text;
            int int_dis=int.Parse(file_food.Discount);
            if (file_food.ID != "" && file_food.Name != "" && file_food.Price != "" && file_food.No_item != "" && file_food.Discount != ""&&int_dis<=100)
            {
                int Return = file_food.ADD_food(file_food.ID, file_food.Name, file_food.Price, file_food.No_item, file_food.Discount);
                if (Return == 1)
                {
                    MessageBox.Show("Done succefuly");
                    clear_panel();
                    Hide_panel();
                    // MessageBox.Show("Done !", "Text_Box", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Return == -1)
                {
                    MessageBox.Show("the item with ID is exist");
                }
                else { MessageBox.Show("some thing goes wrong with the database"); }
            }
            else { MessageBox.Show("please enter all the data or may be the discount large than 100"); }

        }

        private void Display_Click(object sender, EventArgs e)
        {
            if (reload_gridview == false)
            {
                set_Gridview();
                reload_gridview = true;
            }
            else
            { // to clear the gridviw and refesh it
                dataGridView1.DataSource = null;
                Data.Rows.Clear();
                dataGridView1.Refresh();
                // set_Gridview();

            }


            //Data = new DataTable();
            file_food = new File_Food();
            Data_list = file_food.Load(); //load the file
            string id, name, price, quantity, discount; // to recive the data
            for (int i = 0; i < Data_list.Count; i++)
            {
                record = Data_list[i];
                id = record[0];
                name = record[1];
                price = record[2];
                quantity = record[3];
                discount = record[4];
                Data.Rows.Add(i + 1, id, name, price, quantity, discount);
            }
            dataGridView1.DataSource = Data; //set the table
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            file_food = new File_Food();
            int selected_row = 0;

            if (MessageBox.Show("cofirm !", "Text_Box", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //  MessageBox.Show(dataGridView1.Rows.Count.ToString());
                if (dataGridView1.Rows.Count == 1) // the row of column that already exist
                { MessageBox.Show("NO data to delete"); }
                else
                {
                    selected_row = dataGridView1.CurrentCell.RowIndex;
                    dataGridView1.Rows.RemoveAt(selected_row);
                    if (selected_row > -1)
                    {
                        if (file_food.Delete(selected_row))
                        {
                            MessageBox.Show("Deleted successfuly");
                        }
                        else { MessageBox.Show("uncomplete statment"); }
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Delete.Show();
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char charr = e.KeyChar;
            if (!char.IsDigit(charr) && charr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Cannot be Characters");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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
                MessageBox.Show("Cannot be Characters");
            }
        }




    }
}
