﻿using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientsDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [EnumDataType(typeof(AgeGroup))]
        public AgeGroup AgeGroup { get; set;}
        [Required]
        [EnumDataType(typeof(Gender))]
        public int Gender { get; set; }
        public int[] Medicines { get; set; }
    }
}
