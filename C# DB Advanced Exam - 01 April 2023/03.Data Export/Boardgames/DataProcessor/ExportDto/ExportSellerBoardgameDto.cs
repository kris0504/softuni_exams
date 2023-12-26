using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.DataProcessor.ExportDto
{
    public class ExportSellerBoardgameDto
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Mechanics { get; set; }
        public CategoryType Category { get; set;}
        
    }
}
