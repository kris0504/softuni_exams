namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ExportDto;
    using Boardgames.Extensions;
    using Newtonsoft.Json;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            var creators = context.Creators
                
                .Where(x => x.Boardgames.Count > 0)
                .Select(x => new ExportCreatorDto()
                {
                    BoardgamesCount = x.Boardgames.Count,
                    CreatorName = x.FirstName + " " + x.LastName,
                    Boardgames = x.Boardgames
                    .Select(x => new ExportCreatorBoardGame()
                    {
                        BoardgameName = x.Name,
                        BoardgameYearPublished = x.YearPublished
                    })
                   .OrderBy(x => x.BoardgameName).ToArray()

                })
                .OrderByDescending(x => x.BoardgamesCount)
                .ThenBy(x => x.CreatorName).ToArray();
              
            return creators.SerializeToXml<ExportCreatorDto[]>("Creators");
        }
        
        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                .Where(x => x.BoardgamesSellers
                .Any(b => context.Boardgames.
                Any(boardgame => boardgame.Id == b.BoardgameId && boardgame.YearPublished >=year)))
                .AsEnumerable()
                .Select (x => new ExportSellersDto()
                {
                    Name = x.Name,
                    Website = x.Website,
                    Boardgames=x.BoardgamesSellers.Where(x=>x.Boardgame.Rating<=rating&&x.Boardgame.YearPublished>=year)
                    .ToArray()
                    .Select(c => new ExportSellerBoardgameDto()
                    {
                        Name=c.Boardgame.Name,
                        Rating=c.Boardgame.Rating,
                        Mechanics=c.Boardgame.Mechanics,
                        Category=c.Boardgame.CategoryType
                    }).OrderByDescending(x=>x.Rating)
                    .ThenBy(x=>x.Name).ToArray()
                }).OrderByDescending(x => x.Boardgames.Length)
                .ThenBy(x=>x.Name).Take(5).ToArray();
            return sellers.SerializeToJson<ExportSellersDto[]>();
        }
    }
}