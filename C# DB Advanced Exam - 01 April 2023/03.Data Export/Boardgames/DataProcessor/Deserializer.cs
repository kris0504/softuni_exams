namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.DataProcessor.ImportDto;
    using Boardgames.Extensions;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb= new StringBuilder();
            List<ImportCreatorDto> creatorsDto = xmlString
                .DeserializeFromXml<List<ImportCreatorDto>>("Creators");
            List<Creator> creators= new List<Creator>();
            foreach (var creator in creatorsDto)
            {
                if (!IsValid(creator))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var creatortoadd = new Creator()
                {
                    FirstName = creator.FirstName,
                    LastName = creator.LastName

                };
                foreach (var boardgame in creator.Boardgames)
                {
                    if (!IsValid(boardgame))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    else
                    {
                        creatortoadd.Boardgames.Add(new Boardgame()
                        {
                            Name = boardgame.Name,
                            Rating = boardgame.Rating,
                            YearPublished = boardgame.YearPublished,
                            CategoryType = (Data.Models.Enums.CategoryType)boardgame.CategoryType,
                            Mechanics = boardgame.Mechanics
                        });
                    }
                }
                creators.Add(creatortoadd);
                sb.AppendLine(string.Format(SuccessfullyImportedCreator, creator.FirstName, creator.LastName, creatortoadd.Boardgames.Count));
                
            }
            context.Creators.AddRange(creators);
            context.SaveChanges();
            return sb.ToString();

        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<ImportSellerDto> importSellerDtos = jsonString
                .DeserializeFromJson<List<ImportSellerDto>>();
            List<Seller> sellers = new List<Seller>();
            int[] boardgameids = context.Boardgames.Select(x => x.Id).ToArray();
            foreach (var item in importSellerDtos)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Seller seller = new Seller()
                { Name = item.Name,
                    Address = item.Address,
                    Country = item.Country,
                    Website = item.Website,
                };
                foreach (var item2 in item.Boardgames.Distinct())
                {
                    if (!boardgameids.Contains(item2))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    seller.BoardgamesSellers.Add(new BoardgameSeller()
                    {
                        BoardgameId = item2
                    });
                    
                }
                sellers.Add(seller);
                sb.AppendLine(string.Format(SuccessfullyImportedSeller, seller.Name, seller.BoardgamesSellers.Count));
            }
            context.Sellers.AddRange(sellers);
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
