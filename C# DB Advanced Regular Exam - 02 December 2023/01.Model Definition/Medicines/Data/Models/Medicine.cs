using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.Data.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [Range(0.01,1000)]
        public decimal	Price  { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public DateTime ProductionDate  { get; set; }
        [Required]
        public DateTime ExpiryDate  { get; set;}
        [Required]
        [MaxLength(100)]
        public string Producer  { get; set; }
        [Required]
        [ForeignKey(nameof(PharmacyId))]
        public int PharmacyId  { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public ICollection<PatientMedicine> PatientsMedicines { get; set; }=new HashSet<PatientMedicine>();
        
    }
}
