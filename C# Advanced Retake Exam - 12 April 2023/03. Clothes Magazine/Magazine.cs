using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClothesMagazine
{
    public class Magazine
    {
        public Magazine(string type, int capacity)
        {
            Type = type;
            Capacity = capacity;
            Clothes = new List<Cloth>(Capacity);
        }

        public string Type { get; set; }
        public int Capacity { get; set;  }
        public List<Cloth> Clothes { get; set; }
        public void AddCloth(Cloth cloth)
        {
            Clothes.Add(cloth);
        }
        public bool RemoveCloth(string color)
        {
            Cloth a = Clothes.Find(x => x.Color == color);
            return Clothes.Remove(a);
        }
        public Cloth GetSmallestCloth()
        {
            List<Cloth> a = Clothes.OrderBy(x => x.Size).ToList();

            return a[0];
        }
        public Cloth GetCloth(string color)
        {
            return Clothes.Find(x => x.Color == color);
        }
        public int GetClothCount()
        {
            return Clothes.Count();
        }
        public string Report()
        {
            List<Cloth> ordered=Clothes.OrderBy(x => x.Size).ToList();
            StringBuilder a = new StringBuilder();
            a.AppendLine($"{Type} magazine contains:");
            foreach (var item in ordered)
            {
                a.AppendLine(item.ToString());
            }
            return a.ToString().Trim();
        }

    }
}
