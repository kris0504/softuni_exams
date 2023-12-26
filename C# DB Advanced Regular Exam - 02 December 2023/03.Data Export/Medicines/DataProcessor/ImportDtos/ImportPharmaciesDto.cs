using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmaciesDto
    {
        [XmlAttribute("non-stop")]
        [Required]
        public string IsNonStop { get; set; }
      
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [StringLength(14)]
        [RegularExpression("\\(\\d\\d\\d\\) \\d\\d\\d-\\d\\d\\d\\d")]
        public string PhoneNumber { get; set; }
        [XmlArray("Medicines")]
        public ImportMedicinesDto[] Medicines { get; set; }

    }
}
