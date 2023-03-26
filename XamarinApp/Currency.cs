using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XamarinAppTst
{
    [Serializable]
    public class Currency
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("rate")]
        public string Rate { get; set; }

        [XmlIgnore]
        public string SomeExtraProperty { get; set; }
    }
}
