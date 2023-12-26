using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.Data.Models.Enums
{
    public enum CategoryType
    {
        [XmlEnum("Abstract")]
        Abstract =0,
        [XmlEnum("Children")]
        Children =1,
        [XmlEnum("Family")]
        Family =2,
        [XmlEnum("Party")]
        Party =3,
        [XmlEnum("Strategy")]
        Strategy =4
    }
}
