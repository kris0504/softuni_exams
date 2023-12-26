using System.Text;

namespace AutomotiveRepairShop
{
    public class RepairShop
    {
        public RepairShop(int capacity)
        {
            Capacity = capacity;
            Vehicles = new List<Vehicle>(Capacity);
        }

        public int Capacity { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public void AddVehicle(Vehicle vehicle)
        {
            if (Capacity > Vehicles.Count)
            {
                Vehicles.Add(vehicle);
            }
        }
        public bool RemoveVehicle(string vin)
        {
            Vehicle found = Vehicles.Find(x => x.VIN == vin);
            return Vehicles.Remove(found);
        }
        public int GetCount()
        {
            return Vehicles.Count;
        }
        public Vehicle GetLowestMileage()
        {
          Vehicle minveh= Vehicles.MinBy(x => x.Mileage);
            return minveh;
        }
        public string Report()
        {
            StringBuilder sb=new StringBuilder();
            sb.AppendLine("Vehicles in the preparatory:");
            foreach (var item in Vehicles)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString().Trim();
        }
    }
}
