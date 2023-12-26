using System.Xml.Serialization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Medicine")]
    public class ExportXmlDtoMedicine
    {
        [XmlAttribute]
        public string Category { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string Producer { get; set; } = null!;
        public string BestBefore { get; set; } = null!;
    }
}
