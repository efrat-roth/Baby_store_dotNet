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
        /// <summary>
        /// Take a list from the xml and copy it to list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        public static void saveListToXML(List<T?> list,string path)
        {
            XmlSerializer x = new XmlSerializer(list.GetType());
            string dir = "..\\xml\\";
            FileStream fs = new FileStream(dir+path, FileMode.Create);
            x.Serialize(fs, list);
        }

        /// <summary>
        /// Take a list from a list and copy it to xml
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<T?> loadListFromXML(string path)
        {
            List<T?> list;
            XmlSerializer x = new XmlSerializer(typeof(List<T?>));            
            string dir = "..\\xml\\";
            FileStream fs = new FileStream(dir + path, FileMode.Open);
            list = (List<T?>)x.Deserialize(fs);
            return list.ToList<T?>();
        }
    }
}
