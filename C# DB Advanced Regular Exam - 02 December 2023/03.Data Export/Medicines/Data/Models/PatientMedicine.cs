﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.Data.Models
{
    public class PatientMedicine
    {
        [ForeignKey(nameof(PatientId))]
        public int PatientId  { get; set; }
        public Patient Patient { get; set; }
        [ForeignKey(nameof(MedicineId))]
        public int MedicineId  { get; set; }
        public Medicine Medicine { get; set; }
    }
}
