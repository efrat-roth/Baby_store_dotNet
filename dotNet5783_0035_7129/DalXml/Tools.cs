using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal
{
    internal static class Tools<T>
    {

        static XElement? Root;
        static string? Path;
        public static void LoadData()
        {
            try
            {
                Root = XElement.Load(Path);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        public static void saveListToXML(List<T?> list,string path, XElement? root)
        {
            Root=root;
            Path = path;
            LoadData();
            XmlSerializer x = new XmlSerializer(list.GetType());
            string dir = "..\\xml\\";
            FileStream fs = new FileStream(dir+path, FileMode.Create);
            x.Serialize(fs, list);
        }

        public static List<T?> loadListFromXML(string path, XElement? root)
        {

            Root = root;
            Path = path;
            LoadData();
            List<T?> list;
            XmlSerializer x = new XmlSerializer(typeof(List<T?>));            
            string dir = "..\\xml\\";
            FileStream fs = new FileStream(dir + path, FileMode.Open);
            list = (List<T?>)x.Deserialize(fs);
            return list.ToList<T?>();

        }
    }
}
