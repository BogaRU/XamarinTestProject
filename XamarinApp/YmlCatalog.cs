using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XamarinAppTst
{
    [XmlRoot("yml_catalog")]
    public class YmlCatalog
    {
        [XmlElement("shop")]
        public Shop Shop { get; set; }

        [XmlAttribute("date")]
        public string Date { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("company")]
        public string Company { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlElement("currency")]
        public List<Currency> Currencies { get; set; }

        [XmlElement("category")]
        public List<Category> Categories { get; set; }

        [XmlElement("local_delivery_cost")]
        public string LocalDeliveryCost { get; set; }
    }
}
