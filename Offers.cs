using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MauiAppTst
{
    public class Offers
    {
        [XmlElement("offer")]
        public List<Offer> OfferList { get; set; }
    }
}
