namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using Cadastre.Data.Enumerations;
    using Cadastre.Data.Models;
    using Cadastre.DataProcessor.ImportDtos;
    using Cadastre.SerializeDeserialize;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
           StringBuilder sb= new StringBuilder();
            List<ImportDistrictDto> importDistrictDtos = xmlDocument
                .DeserializeFromXml<List<ImportDistrictDto>>("Districts");
            List<District> districts = new List<District>();
            foreach (var district in importDistrictDtos)
            {
                if (!IsValid(district))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                District districtToAdd = new District()
                {
                    Name = district.Name,
                    PostalCode = district.PostalCode,
                    Region= (Region)Enum.Parse(typeof(Region), district.Region),
                };
                if (districts.Any(x => x.Name == districtToAdd.Name))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (var property in district.Properties)
                {
                    if (!IsValid(property))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Property propertyToAdd = new Property()
                    {
                        PropertyIdentifier = property.PropertyIdentifier,
                        Area = property.Area,
                        Details = property.Details,
                        Address = property.Address,
                       // DateOfAcquisition = DateTime.Parse(property.DateOfAcquisition)
                    };
                  
                    if (DateTime.TryParseExact(property.DateOfAcquisition, "dd/MM/yyyy", CultureInfo
                        .InvariantCulture, DateTimeStyles.None, out DateTime dateofac))
                    {
                        propertyToAdd.DateOfAcquisition = dateofac;
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (districtToAdd.Properties.Any(x=>x.PropertyIdentifier==propertyToAdd.PropertyIdentifier))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (districtToAdd.Properties.Any(x => x.Address == propertyToAdd.Address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    districtToAdd.Properties.Add(propertyToAdd);
                }
                districts.Add(districtToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedDistrict,districtToAdd.Name,districtToAdd.Properties.Count));
            }
            dbContext.Districts.AddRange(districts);
            dbContext.SaveChanges();
            return sb.ToString();
        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            StringBuilder sb = new StringBuilder();
            List<ImportCitizensDto> importCitizensDtos = jsonDocument
                .DeserializeFromJson<List<ImportCitizensDto>>();
            List<Citizen> citizens = new List<Citizen>();
            int[] propertyIds= dbContext.Properties.Select(x=>x.Id).ToArray();
            foreach (var citizen  in importCitizensDtos)
            {
                if (!IsValid(citizen))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Citizen citizenToAdd = new Citizen()
                {
                    FirstName = citizen.FirstName,
                    LastName = citizen.LastName,
                 //   BirthDate =DateTime.Parse(citizen.BirthDate),
                    
                };
                if (DateTime.TryParseExact(citizen.BirthDate, "dd-MM-yyyy", CultureInfo
                        .InvariantCulture, DateTimeStyles.None, out DateTime birth))
                {
                    citizenToAdd.BirthDate = birth;
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (citizen.MaritalStatus== "Unmarried"|| citizen.MaritalStatus == "Married" || citizen.MaritalStatus == "Divorced" || citizen.MaritalStatus == "Widowed")
                {
                    citizenToAdd.MaritalStatus = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), citizen.MaritalStatus);
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                foreach (var prop in citizen.Properties)
                {
                    citizenToAdd.PropertiesCitizens.Add(new PropertyCitizen()
                    {
                        Citizen=citizenToAdd,
                        PropertyId = prop
                    });
                }
                citizens.Add (citizenToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedCitizen,citizenToAdd.FirstName,citizenToAdd.LastName,citizenToAdd.PropertiesCitizens.Count));
            }
            dbContext.Citizens.AddRange (citizens);
           dbContext.SaveChanges();
            return sb.ToString ();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
