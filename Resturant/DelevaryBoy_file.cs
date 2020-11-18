using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
namespace Resturant
{
    class DelevaryBoy_file
    {
        XmlWriter writer;
        XmlDocument doc;
        XmlNodeList lists;
        private const string file_delv_boy_path = "Delivery Boy.xml";
        private const string file_delv_path = "Delivery.xml";
        //public string idboy { get; set; }
        //public string name_boy { get; set; }
        //public string Phone_boy { get; set; }
        //public string areacode { get; set; }
        // to set the element
        public string id = "ID"; public string name = "Name"; public string phone = "phone"; public string area_code = "AreaCode";

        public bool AddBoy(string idboy, string name_boy, string phone_boy, string areacode)
        {
            string a = areacode;
            string b = idboy;
            try
            {
                if (!File.Exists(file_delv_boy_path))
                {
                    writer = XmlWriter.Create(file_delv_boy_path);
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Table");
                    writer.WriteStartElement("DeliveryBoy");

                    writer.WriteStartElement(id);
                    writer.WriteString(idboy);
                    writer.WriteEndElement();

                    writer.WriteStartElement(name);
                    writer.WriteString(name_boy);
                    writer.WriteEndElement();

                    writer.WriteStartElement(phone);
                    writer.WriteString(phone_boy);
                    writer.WriteEndElement();

                    writer.WriteStartElement(area_code);
                    writer.WriteString(areacode);
                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteEndDocument();
                    writer.Close();
                }
                else
                {
                    doc = new XmlDocument();
                    XmlElement boy = doc.CreateElement("DeliveryBoy");
                    XmlElement node = doc.CreateElement(id);
                    node.InnerText = idboy;
                    boy.AppendChild(node);

                    node = doc.CreateElement(name);
                    node.InnerText = name_boy;
                    boy.AppendChild(node);

                    node = doc.CreateElement(phone);
                    node.InnerText = phone;
                    boy.AppendChild(node);

                    node = doc.CreateElement(area_code);
                    node.InnerText = areacode;
                    boy.AppendChild(node);

                    doc.Load(file_delv_boy_path);
                    XmlElement root = doc.DocumentElement;
                    root.AppendChild(boy);
                    doc.Save(file_delv_boy_path);
                }
                boy_areacode(b,a);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {

            }
        }
        public bool delete_node(string id)
        {
            doc = new XmlDocument();
            try
            {
                doc.Load(file_delv_boy_path);
                foreach (XmlNode xmlnod in doc.SelectNodes("Table/DeliveryBoy"))
                    if (xmlnod.SelectSingleNode(this.id).InnerText == id) xmlnod.ParentNode.RemoveChild(xmlnod);
                doc.Save(file_delv_boy_path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool check(string ID_check)
        {
            int a = 0;
            if (File.Exists(file_delv_boy_path))
            {
                doc = new XmlDocument();
                doc.Load(file_delv_boy_path);
                lists = doc.GetElementsByTagName(id);
                for (int i = 0; i < lists.Count; i++)
                {
                    string idvalue = lists[i].InnerText;
                    if (idvalue.Equals(ID_check))//it exists
                    { a = 1; }
                }
            }
            else { a = 0; }
            if (a == 1) { return false; }
            else { return true; }
        }
        public bool boy_areacode(string boy,string area)
        {
            try
            {
                if (!File.Exists(file_delv_path))
                {
                    writer = XmlWriter.Create(file_delv_path);
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Deliverys_area");
                    writer.WriteStartElement(area); //to write the element with the name of area code

                    writer.WriteStartElement(id);
                    writer.WriteString(boy);
                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteEndDocument();
                    writer.Close();
                }
                else
                {
                    doc = new XmlDocument();
                    XmlElement Boy = doc.CreateElement(area);

                    XmlElement node = doc.CreateElement(id);
                    node.InnerText = boy;
                    Boy.AppendChild(node);

                    doc.Load(file_delv_path);
                    XmlElement root = doc.DocumentElement;
                    root.AppendChild(Boy);
                    doc.Save(file_delv_path);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}