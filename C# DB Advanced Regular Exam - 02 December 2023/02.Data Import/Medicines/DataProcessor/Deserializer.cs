namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Medicines.Extensions;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder sb= new StringBuilder();
            List<ImportPatientsDto> importPatientsDtos = jsonString
                .DeserializeFromJson<List<ImportPatientsDto>>();
            List<Patient> patients = new List<Patient>();
            int[] medicineids=context.Medicines.Select(p => p.Id).ToArray();
            foreach (var patient in importPatientsDtos)
            {
                if (!IsValid(patient))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                Patient patienttoimp = new Patient()
                {
                    AgeGroup = (AgeGroup)patient.AgeGroup,
                    FullName= patient.FullName,
                    Gender = (Gender)patient.Gender,
                    
                };
                foreach (var medicineId in patient.Medicines)
                {
                    if (medicineids.Contains(medicineId) && !patienttoimp.PatientsMedicines.Any(x=>x.MedicineId==medicineId) )
                    {
                        
                        patienttoimp.PatientsMedicines.Add(new PatientMedicine()
                        {
                            MedicineId = medicineId
                        });

                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                }
                patients.Add(patienttoimp);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, patienttoimp.FullName, patienttoimp.PatientsMedicines.Count));
            }
            context.Patients.AddRange(patients);
            context.SaveChanges();
            return sb.ToString();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            List<ImportPharmaciesDto> importPharmaciesDtos = xmlString
                .DeserializeFromXml<List<ImportPharmaciesDto>>("Pharmacies");
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            foreach (var pharmacy in importPharmaciesDtos)
            {
                if (!IsValid(pharmacy))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Pharmacy pharmacyToAdd = new Pharmacy()
                {
                    Name = pharmacy.Name,
                    PhoneNumber = pharmacy.PhoneNumber
                    
                };
                bool testbool = bool.TryParse(pharmacy.IsNonStop, out testbool);
                if (!testbool)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                } 
                pharmacyToAdd.IsNonStop = testbool;
                foreach (var medicine in pharmacy.Medicines)
                {
                    if (!IsValid(medicine))
                    {
                       sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Medicine medicineToAdd = new Medicine()
                    {
                        Name = medicine.Name,
                        Price = medicine.Price,
                        Producer = medicine.Producer,
                        Category=(Category)medicine.Category
                        
                    };
                    if (pharmacyToAdd.Medicines.Any(x=>x.Name==medicineToAdd.Name&&x.Producer==medicineToAdd.Producer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                     
                    if (DateTime.TryParse(medicine.ProductionDate,out DateTime proddatemed))
                    {
                        medicineToAdd.ProductionDate = proddatemed;
                    }
                    else
                    {
                       sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (DateTime.TryParse(medicine.ExpiryDate, out DateTime expdatemed))
                    {
                        medicineToAdd.ExpiryDate = expdatemed;
                    }
                    else
                    {
                       sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if(medicineToAdd.ProductionDate<medicineToAdd.ExpiryDate){
                        pharmacyToAdd.Medicines.Add(medicineToAdd);
                        continue;
                    }
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                pharmacies.Add(pharmacyToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy,pharmacyToAdd.Name,pharmacyToAdd.Medicines.Count));
                
            }
            context.Pharmacies.AddRange(pharmacies);
            context.SaveChanges();
            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
