using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("Property")]
    public class ImportPropertiesDto
    {
        [MinLength(16)]
        [MaxLength(20)]
        [Required]
        public string PropertyIdentifier { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Area { get; set; }
        [MinLength(5)]
        [MaxLength(500)]
        public string Details { get; set; }
        [MinLength(5)]
        [MaxLength(200)]
        [Required]
        public string Address { get; set; }
        [Required]
        public string DateOfAcquisition { get; set; }
    }
}
//•	Id – integer, Primary Key
//•	PropertyIdentifier – text with length [16, 20] (required)
//•	Area – int not negative (required)
//•	Details - text with length[5, 500] (not required)
//•	Address – text with length [5, 200] (required)
//•	DateOfAcquisition – DateTime (required)
//•	DistrictId – integer, foreign key (required)
//•	District – District
//•	PropertiesCitizens - collection of type PropertyCitizen