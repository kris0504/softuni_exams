using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ExportDto
{
    [XmlType("Boardgame")]
    public class ExportCreatorBoardGame
    {
        [XmlElement("BoardgameName")]
        public string BoardgameName { get; set; }
        [XmlElement("BoardgameYearPublished")]
        public int BoardgameYearPublished { get; set; }
    }
}
