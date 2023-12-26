using Cadastre.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Cadastre.Data.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string PropertyIdentifier { get; set; }
        [Required]
        public int Area { get; set; }
    
        [MaxLength(500)]
        public string Details { get; set; }
        [MaxLength(200)]
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime DateOfAcquisition { get; set; }
        [Required]
        [ForeignKey(nameof(DistrictId))]
        public int DistrictId { get; set; }
        public District District { get; set; }
        public ICollection<PropertyCitizen> PropertiesCitizens { get; set; } = new HashSet<PropertyCitizen>();

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
