using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Resturant
{
    class Customer_file
    {
        XmlWriter writer1;
        XmlDocument doc;
        XmlNodeList lists;
        private const string customer_path = "customer.XML";
        public string name = "Name"; public string phone = "phone"; public string adress = "Adress"; public string area_code = "AreaCode"; public string total_cost = "Total_cost";
        //public bool Add_new_customer(Customer user_info)
        //{
        //    try
        //    {
        //        user_info.totalcost = "";
        //        FileStream file = new FileStream(customer_path , FileMode.Append);
        //        ser = new XmlSerializer(user_info.GetType());

        //        //using (TextWriter strem = new StreamWriter(customer_path))
        //        {
        //            ser.Serialize(file, user_info);
        //            file.Close();
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return true;
        //    }
        //}
        public bool Add_new_customer(string[] arr, string totalcost)
        {
            string n = arr[0];
            string p = arr[1];
            string a = arr[2];
            string c = arr[3];

            try
            {

                if (!File.Exists(customer_path))
                {
                    writer1 = XmlWriter.Create(customer_path);
                    writer1.WriteStartDocument();
                    writer1.WriteStartElement("Information");
                    writer1.WriteStartElement("Customer");

                    writer1.WriteStartElement(name);
                    writer1.WriteString(n);
                    writer1.WriteEndElement();

                    writer1.WriteStartElement(phone);
                    writer1.WriteString(p);
                    writer1.WriteEndElement();

                    writer1.WriteStartElement(adress);
                    writer1.WriteString(a);
                    writer1.WriteEndElement();

                    writer1.WriteStartElement(area_code);
                    writer1.WriteString(c);
                    writer1.WriteEndElement();

                    writer1.WriteStartElement(total_cost);
                    writer1.WriteString(totalcost);
                    writer1.WriteEndElement();

                    writer1.WriteEndElement();

                    writer1.WriteEndElement();

                    writer1.WriteEndDocument();
                    writer1.Close();
                }
                else
                {
                    doc = new XmlDocument();
                    XmlElement boy = doc.CreateElement("Customer");
                    XmlElement node = doc.CreateElement(name);
                    node.InnerText = n;
                    boy.AppendChild(node);

                    node = doc.CreateElement(phone);
                    node.InnerText = p;
                    boy.AppendChild(node);

                    node = doc.CreateElement(adress);
                    node.InnerText = a;
                    boy.AppendChild(node);

                    node = doc.CreateElement(area_code);
                    node.InnerText = c;
                    boy.AppendChild(node);

                    node = doc.CreateElement(total_cost);
                    node.InnerText = totalcost;
                    boy.AppendChild(node);

                    doc.Load(customer_path);
                    XmlElement root = doc.DocumentElement;
                    root.AppendChild(boy);
                    doc.Save(customer_path);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool check_user_notexist(string phone_check)
        {
            int a = 0;
            if (File.Exists(customer_path))
            {
                doc = new XmlDocument();
                doc.Load(customer_path);
                lists = doc.GetElementsByTagName(phone);
                for (int i = 0; i < lists.Count; i++)
                {
                    string phonevalue = lists[i].InnerText;
                    if (phonevalue.Equals(phone_check))//it exists
                    { a = 1; }
                }
            }
            else { a = 0; }
            if (a == 1) { return false; }
            else { return true; }
        }
        public bool update_totalCost(string Total_cost_enter, string phone_check)
        {
            string[] arr = new string[4];
            int bool_check = 0; int c = 0; int p = 0;
            int appendValue;
            c = int.Parse(Total_cost_enter);
            doc.Load(customer_path);
            lists = doc.GetElementsByTagName("Customer");
            for (int i = 0; i < lists.Count; i++)
            {
                XmlNodeList children = lists[i].ChildNodes;

                string namevalue = children[0].InnerText;
                string phonevalue = children[1].InnerText;
                string addresvalue = children[2].InnerText;
                string areavalue = children[3].InnerText;
                string costvalue = children[4].InnerText;
                p = int.Parse(costvalue);
                if (phonevalue.Equals(phone_check))
                {
                    arr[0] = namevalue;
                    arr[1] = phonevalue;
                    arr[2] = addresvalue;
                    arr[3] = areavalue;
                    appendValue = c + p;
                    if (delete_node(phone_check))
                    {
                        Add_new_customer(arr, appendValue.ToString());
                    }
                    bool_check = 1;
                }
                else { bool_check = 0; }
            }
            if (bool_check == 1) { return true; }
            else { return false; }

        }
        public bool delete_node(string Phone_update_info)
        {
            doc = new XmlDocument();
            try
            {
                doc.Load(customer_path);
                foreach (XmlNode xmlnod in doc.SelectNodes("Information/Customer"))
                    if (xmlnod.SelectSingleNode(this.phone).InnerText == Phone_update_info) xmlnod.ParentNode.RemoveChild(xmlnod);
                doc.Save(customer_path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool vip_check(string phone_check)
        {
            int bool_check = 0;
            if (File.Exists(customer_path))
            {
                doc = new XmlDocument();
                doc.Load(customer_path);
                lists = doc.GetElementsByTagName("Customer");
                for (int i = 0; i < lists.Count; i++)
                {
                    XmlNodeList children = lists[i].ChildNodes;

                    string phonevalue = children[1].InnerText;
                    string costvalue = children[4].InnerText;
                    int p = int.Parse(costvalue);
                    if (phonevalue.Equals(phone_check))
                    {
                        if (p > 500)
                        {
                            bool_check = 1;
                        }
                    }
                    else { bool_check = 0; }
                }
            }

            if (bool_check == 1) { return true; }
            else { return false; }
        }
    }
}
