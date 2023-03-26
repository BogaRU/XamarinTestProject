using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XamarinAppTst
{
    public class DeliveryOptions
    {
        [XmlAttribute("cost")]
        public string Cost { get; set; }

        [XmlAttribute("days")]
        public string Days { get; set; }
    }
}
