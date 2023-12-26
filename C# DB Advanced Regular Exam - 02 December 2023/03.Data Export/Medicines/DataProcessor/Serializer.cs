namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ExportDtos;
    using Medicines.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {   
            DateTime givendate= DateTime.Parse(date);
            ExportPatientsDto[] patients = context.Patients
                .Where(x => x.PatientsMedicines.Any(x => x.Medicine.ProductionDate > givendate))
                .Select(y => new ExportPatientsDto()
                {
                    Name = y.FullName,
                    AgeGroup = y.AgeGroup.ToString(),
                    Gender = y.Gender.ToString().ToLower(),
                    Medicines = y.PatientsMedicines
                    .Where(x=>x.Medicine.ProductionDate>=givendate)
                    .OrderByDescending(x => x.Medicine.ExpiryDate).ThenBy(x => x.Medicine.Price).Select(x => x.Medicine).Select(x => new ExportXmlDtoMedicine()
                    {
                        Category = x.Category.ToString().ToLower(),
                        Name = x.Name,
                        Price = x.Price.ToString("F2"),
                        Producer = x.Producer,
                        BestBefore = x.ExpiryDate.ToString("yyyy-MM-dd")
                    }).ToArray()
                }).OrderByDescending(x => x.Medicines.Length).ThenBy(x=>x.Name).ToArray();
            string result = patients.SerializeToXml<ExportPatientsDto[]>("Patients");
            return result;
                
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {

            ExportMedicinesDto[] meds=context.Medicines
                .Where(x=>x.Category==(Category)medicineCategory&&x.Pharmacy.IsNonStop==true)
                .OrderBy(x => x.Price).ThenBy(x => x.Name)
                .Select(x=> new ExportMedicinesDto()
                {
                    Name = x.Name,
                    Price=x.Price.ToString("f2"),
                    Pharmacy= new ExportPharmacyDto() 
                    {
                        Name=x.Pharmacy.Name,
                        PhoneNumber=x.Pharmacy.PhoneNumber
                    }
                }).ToArray();
            return meds.SerializeToJson<ExportMedicinesDto[]>();
        }
    }
}
