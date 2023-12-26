using Cadastre.Data;
using Cadastre.DataProcessor.ExportDtos;
using Cadastre.SerializeDeserialize;
using System.Globalization;
using System.Net.Http.Headers;

namespace Cadastre.DataProcessor
{
    public class Serializer
    {
        public static string ExportPropertiesWithOwners(CadastreContext dbContext)
        {
            ExportPropertyJson[] properties = dbContext.Properties
                .Where(x => x.DateOfAcquisition >= DateTime.Parse("01/01/2000"))
                .OrderByDescending(x => x.DateOfAcquisition)
                .ThenBy(x => x.PropertyIdentifier)
                .Select(x => new ExportPropertyJson()
                {
                    PropertyIdentifier = x.PropertyIdentifier,
                    Area = x.Area,
                    Address = x.Address,
                    DateOfAcquisition = x.DateOfAcquisition.ToString("dd/MM/yyyy", CultureInfo
                        .InvariantCulture),
                    Owners = x.PropertiesCitizens.Select(x => new ExportOwnerDto()
                    {
                        LastName = x.Citizen.LastName,
                        MaritalStatus = x.Citizen.MaritalStatus.ToString()
                    }).OrderBy(x => x.LastName).ToArray()
                }).ToArray();
                return SerializeDeserialize.JsonSerializationExtension.SerializeToJson(properties);
        }

        public static string ExportFilteredPropertiesWithDistrict(CadastreContext dbContext)
        {
            ExportPropertiesDto[] props= dbContext.Properties
                .Where(x=>x.Area>=100)
                .OrderByDescending(x=>x.Area)
                .ThenBy (x => x.DateOfAcquisition)
                .Select (x => new ExportPropertiesDto()
                {
                    PostalCode=x.District.PostalCode,
                    PropertyIdentifier=x.PropertyIdentifier,
                    Area=x.Area,
                    DateOfAcquisition=x.DateOfAcquisition.ToString("dd/MM/yyyy", CultureInfo
                        .InvariantCulture)

                }).ToArray();
            return props.SerializeToXml<ExportPropertiesDto[]>("Properties");
        }
    }
}
