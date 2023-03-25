using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XamarinAppTst
{
    public class Offer
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("param")]
        public List<Param> Params { get; set; }

        [XmlElement("vendor")]
        public string Vendor { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}
