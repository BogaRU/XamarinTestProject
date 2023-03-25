using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MauiAppTst
{
    [XmlRoot("yml_catalog")]
    public class YmlCatalog
    {
        [XmlElement("shop")]
        public Shop Shop { get; set; }
    }
}
