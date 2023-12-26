using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Patient")]
    public class ExportPatientsDto
    {
        [XmlAttribute("Gender")]
        public string Gender { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string AgeGroup { get; set; } = null!;
        [XmlArray("Medicines")]
        public ExportXmlDtoMedicine[] Medicines { get; set; } = new ExportXmlDtoMedicine[0];
    }
}
