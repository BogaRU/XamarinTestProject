using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XamarinAppTst
{
    public class Shop
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("company")]
        public string Company { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("currencies")]
        public List<Currency> Currencies { get; set; }

        [XmlElement("categories")]
        public List<Category> Categories { get; set; }

        [XmlElement("delivery-options")]
        public DeliveryOptions DeliveryOptions { get; set; }

        [XmlElement("local_delivery_cost")]
        public string LocalDeliveryCost { get; set; }

        [XmlElement("offers")]
        public Offers Offers { get; set; }
    }
}
