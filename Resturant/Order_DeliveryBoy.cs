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
    public partial class Order_DeliveryBoy : Form
    {
        order Order;
        Customer_order C_order;
        public static List<string[]> MY_list;
        DataTable Data;
        string Cost;
        string price = "0"; string quantity = "0"; string discount = "0";
        Customer_file C_file;
        File_Food food_up;
        int GlobalTotalCost = 0;
        string[] information_cutomer;
        private static string[] record_global;
        bool VIP = false;
        public Order_DeliveryBoy(List<string[]> my_list, string[] info_arr)
        {
            InitializeComponent();
            label3.Hide();
            MY_list = my_list;
            information_cutomer = info_arr;
            show_data();
            show_info();
            check_vip();
            totalcost_display();

        }
        void check_vip()
        {
            C_file = new Customer_file();
            if (C_file.vip_check(information_cutomer[1]))
            {
                VIP = true;
                label3.Show();
            }
        }
        void set_Gridview()
        {
            Data.Columns.Add("Name", typeof(string));
            Data.Columns.Add("price", typeof(string));
            Data.Columns.Add("Quantity", typeof(string));
            Data.Columns.Add("Discount", typeof(string));
            Data.Columns.Add("ID", typeof(string));
            my_order.DataSource = Data;
        }
        void show_info()
        {
            textBox1.Text = information_cutomer[0];
            textBox2.Text = information_cutomer[1];
            textBox3.Text = information_cutomer[2];
            textBox4.Text = information_cutomer[3];
        }
        void show_data()
        {
            Data = new DataTable();
            set_Gridview();
            string id, name, price, quantity, discount; // to recive the data
            for (int i = 0; i < MY_list.Count; i++)
            {
                record_global = MY_list[i];
                name = record_global[0];
                price = record_global[1];
                quantity = record_global[2];
                discount = record_global[3];
                id = record_global[4];
                Data.Rows.Add(name, price, quantity, discount, id);
            }
            my_order.DataSource = Data;
        }
        void totalcost_display()
        {
            if (VIP == true)
            {
                totalcost_vip();
            }
            else 
            {
                Display_totalcost_withoutVIB();
            }
        }
        void totalcost_vip()
        {
            for (int i = 0; i < MY_list.Count; i++)
            {
                record_global = MY_list[i];
                //price = my_order.Rows[i].Cells[1].Value.ToString(); //price
                //quantity = my_order.Rows[i].Cells[2].Value.ToString(); //Quantity
                price = record_global[1];
                quantity = record_global[2];
                discount = record_global[3];
                int int_price = int.Parse(price);
                int int_quantity = int.Parse(quantity);
                int int_discount = int.Parse(discount);
                GlobalTotalCost +=  int_quantity*(int_price*int_discount/100);
            }
            Cost = GlobalTotalCost.ToString();
            label2.Text = Cost;
        }
        void Display_totalcost_withoutVIB()
        {
            for (int i = 0; i < MY_list.Count; i++)
            {
                record_global = MY_list[i];
                //price = my_order.Rows[i].Cells[1].Value.ToString(); //price
                //quantity = my_order.Rows[i].Cells[2].Value.ToString(); //Quantity
                price = record_global[1];
                quantity = record_global[2];
                int int_price = int.Parse(price);
                int int_quantity = int.Parse(quantity);
                GlobalTotalCost += int_price * int_quantity;
            }
            Cost = GlobalTotalCost.ToString();
            label2.Text = Cost;
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            string Cost = GlobalTotalCost.ToString();
            C_file = new Customer_file();
            food_up = new File_Food();
            for (int i = 0; i < MY_list.Count; i++)
            {
                record_global = MY_list[i];
                food_up.update_file(record_global[4], record_global[2]);
            }
            if (C_file.check_user_notexist(information_cutomer[1]))
            {
                if (C_file.Add_new_customer(information_cutomer, Cost))
                {

                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("error with data base");
                }
            }
            else
            {
                // update the total cost
               if( C_file.update_totalCost(Cost,information_cutomer[1]))
                {
                MessageBox.Show("Done");
                }
               else
               {
                   MessageBox.Show("Error with data base");
               }
            }
            add_class();
            C_order = new Customer_order();
            C_order.add(Order);
            if (C_order.save())
            {
                MessageBox.Show("success");
            }
            else { MessageBox.Show("not saved"); }


        }
        void add_class()
    {
        Order = new order(information_cutomer[1],"123", MY_list);
    
    }

        private void button2_Click_1(object sender, EventArgs e)
        {
            user_form user = new user_form(information_cutomer);
            user.Show();
            this.Hide();
        }
    }
}
