using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace Resturant
{
    class Customer_order
    {
        order my_order;
        FileStream file;
        public Dictionary<string, order> dic_old;
        public Dictionary<string, order> dic_new;
        BinaryFormatter Bianary;
        public string order_path = "order.txt";
        public Customer_order()
        {
            dic_old = new Dictionary<string, order>();
            dic_new = new Dictionary<string, order>();
            load();
        
        }
        void add_order()
        { 

        
        }
        public void load()
        {
            dic_old.Clear();
            Bianary=new BinaryFormatter();
            if (File.Exists(order_path))
            {
                file = new FileStream(order_path, FileMode.Open);
                while (file.Position != file.Length)
                {
                    Dictionary<string, order> tmp = (Dictionary<string, order>)Bianary.Deserialize(file);
                    for (int i = 0; i < tmp.Count; i++)
                    {
                        dic_old.Add(tmp.ElementAt(i).Key, tmp.ElementAt(i).Value);

                    }

                }
                file.Close();
            }
            else 
            {
                file = new FileStream(order_path, FileMode.Create);
                file.Close();
            }
        }
        public bool save()
        {
            try
            {
                Bianary = new BinaryFormatter();
                file = new FileStream(order_path, FileMode.Append);
                Bianary.Serialize(file, dic_new);
                file.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            
            }
        
        }
        public void add(order O)
    {
        if (!dic_old.ContainsKey(O.phone) && !dic_new.ContainsKey(O.phone))
        {
            dic_new.Add(O.phone, O);
        }
        
    
    
    }
    }
}
