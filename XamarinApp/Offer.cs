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

        [XmlAttribute("available")]
        public bool Available { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("vendor")]
        public string Vendor { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("picture")]
        public string Picture { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("vendorCode")]
        public string VendorCode { get; set; }

        [XmlElement("categoryId")]
        public string CategoryId { get; set; }

        [XmlElement("delivery")]
        public bool Delivery { get; set; }

        [XmlElement("pickup")]
        public bool Pickup { get; set; }

        [XmlElement("store")]
        public bool Store { get; set; }

        [XmlElement("manufacturer_warranty")]
        public bool ManufacturerWarranty { get; set; }

        [XmlElement("country_of_origin")]
        public string CountryOfOrigin { get; set; }

        [XmlElement("currencyId")]
        public string CurrencyId { get; set; }
    }
}
