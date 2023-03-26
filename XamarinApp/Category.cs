using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XamarinAppTst
{
    [Serializable]
    public class Category
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("parentId")]
        public string ParentId { get; set; }

        [XmlText]
        public string Name { get; set; }

        [XmlElement("picture")]
        public List<string> Pictures { get; set; }
    }
}
