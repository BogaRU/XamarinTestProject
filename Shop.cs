using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MauiAppTst
{
    public class Shop
    {
        [XmlElement("offers")]
        public Offers Offers { get; set; }
    }
}
