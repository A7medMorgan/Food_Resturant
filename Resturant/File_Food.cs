using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Resturant
{
    class File_Food
    {
        private const string Filepath = "Food.txt";
        private const string temppath = "tmp.txt";
        FileStream File_strem;
        StreamReader stream_R;
        StreamWriter stream_W;
        FileStream File_strem_tmp;
        StreamReader stream_R_tmp;
        StreamWriter stream_W_tmp;
        public static List<string[]> Food_list;
        private List<string> List = new List<string>();
        private string Record;
        public string ID; public string Name; public string Price; public string No_item; public string Discount = "0";
        string record = null;
        //File_Food() // constractor to load the file
        //{

        //}

        public int ADD_food(string id, string name, string price, string no_item, string discount)
        {
            int Nu_toreturn = 0;
            try
            {
                if (check_if_exist(id)) // check if its exist
                {
                    File_strem = new FileStream(Filepath, FileMode.Append);
                    stream_W = new StreamWriter(File_strem);

                    record = id + '@' + name + '@' + price + '@' + no_item + '@' + discount;
                    stream_W.WriteLine(record);
                    Nu_toreturn = 1; //mean that the item never exit befor
                    stream_W.Close();
                    File_strem.Close();
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            //return what haben with the file
            if (Nu_toreturn == 1) return 1;
            else if (Nu_toreturn == -1) return -1; //item is exist
            else { return 0; } // that mean Exption had been thrown

        }
        bool check_if_exist(string id)
        {
            int q = 0;
            Food_list = new List<string[]>();
            try
            {
                File_strem = new FileStream(Filepath, FileMode.Open);
                stream_R = new StreamReader(File_strem);
                while (stream_R.Peek() != -1)
                {
                    Record = stream_R.ReadLine();
                    // to delete based on position
                    List.Add(Record);
                    string[] arr_of_item = Record.Split('@');
                    if (id.Equals(arr_of_item[0]))
                    {
                        q = 1;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream_R.Close();
                File_strem.Close();
            }
            if (q == 1) { return false; }
            else { return true; }

        }

        public List<string[]> Load()
        {
            Food_list = new List<string[]>();
            try
            {
                File_strem = new FileStream(Filepath, FileMode.Open);
                stream_R = new StreamReader(File_strem);
                while (stream_R.Peek() != -1)
                {
                    Record = stream_R.ReadLine();
                    // to delete based on position
                    List.Add(Record);
                    string[] arr_of_item = Record.Split('@');
                    Food_list.Add(arr_of_item);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream_R.Close();
                File_strem.Close();
            }
            return Food_list;
        }
        public bool Delete(int position)
        {
            try
            {
                Load();
                File_strem = new FileStream(Filepath, FileMode.Create);
                stream_W = new StreamWriter(File_strem);
                for (int i = 0; i < List.Count; i++)
                {
                    if (i == position)
                    {
                        continue;
                    }
                    else { stream_W.WriteLine(List[i]); }


                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                stream_W.Close();
                File_strem.Close();
            }

        }
        public void update_file(string id, string new_quantity)
        {
            try
            {
                int int_quantity = int.Parse(new_quantity);
                File_strem = new FileStream(Filepath, FileMode.Open);
                File_strem_tmp = new FileStream(temppath, FileMode.Create);
                stream_R = new StreamReader(File_strem);
                stream_W_tmp = new StreamWriter(File_strem_tmp);
                while (stream_R.Peek() != -1)
                {
                    Record = stream_R.ReadLine();
                    string[] arr_of_item = Record.Split('@');
                    if (id.Equals(arr_of_item[0]))
                    {
                        int int_old_quntity = int.Parse(arr_of_item[3]);
                        string str_quantity = (int_old_quntity - int_quantity).ToString();
                        stream_W_tmp.WriteLine(arr_of_item[0] + '@' + arr_of_item[1] + '@' + arr_of_item[2] + '@' + str_quantity + '@' + arr_of_item[4]);
                    }
                    else
                    {
                        stream_W_tmp.WriteLine(arr_of_item[0] + '@' + arr_of_item[1] + '@' + arr_of_item[2] + '@' + arr_of_item[3] + '@' + arr_of_item[4]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream_R.Close();
                File_strem.Close();
                stream_W_tmp.Close();
                File_strem_tmp.Close();
                rePrint_file();
            }
        }
        void rePrint_file()
        {
            try
            {
                File_strem = new FileStream(Filepath, FileMode.Create);
                File_strem_tmp = new FileStream(temppath, FileMode.Open);
                stream_W = new StreamWriter(File_strem);
                stream_R_tmp = new StreamReader(File_strem_tmp);
                while (stream_R_tmp.Peek() != -1)
                {
                    record = stream_R_tmp.ReadLine();
                    stream_W.WriteLine(record);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream_R_tmp.Close();
                File_strem_tmp.Close();
                stream_W.Close();
                File_strem.Close();
            }

        }

    }
}