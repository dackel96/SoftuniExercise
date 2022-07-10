using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drones
{
    public class Airfield
    {
        private List<Drone> drones;
        private string name;
        private int capacity;
        private double landingstrip;
        public Airfield(string name, int capacity, double landingStrip)
        {
            Name = name;
            Capacity = capacity;
            LandingStrip = landingStrip;
            Drones = new List<Drone>();
        }
        public int Count => drones.Count;
        public List<Drone> Drones
        {
            get { return drones; }
            set { drones = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
        public double LandingStrip
        {
            get { return landingstrip; }
            set { landingstrip = value; }
        }

        public string AddDrone(Drone drone)
        {
            if (string.IsNullOrEmpty(drone.Name) || string.IsNullOrEmpty(drone.Brand))
            {
                return "Invalid drone.";
            }
            if (drone.Range < 5 || drone.Range > 15)
            {
                return "Invalid drone.";
            }
            if (drones.Count == capacity)
            {
                return "Airfield is full.";
            }
            drones.Add(drone);
            return $"Successfully added {drone.Name} to the airfield.";
        }
        public bool RemoveDrone(string name)
        {
            Drone drone = drones.FirstOrDefault(x => x.Name == name);
            return drones.Remove(drone);
        }
        public int RemoveDroneByBrand(string brand)
        {
            List<Drone> dronesToRemove = drones.Where(x => x.Brand == brand).ToList();
            int count = dronesToRemove.Count;
            if (count == 0)
            {
                return 0;
            }
            foreach (var drone in dronesToRemove)
            {
                drones.Remove(drone);
            }
            return count;
        }
        public Drone FlyDrone(string name)
        {
            Drone drone = drones.FirstOrDefault(x => x.Name == name);
            if (drone == null)
            {
                return null;
            }
            drone.Available = false;
            return drone;
        }
        public List<Drone> FlyDronesByRange(int range)
        {
            List<Drone> rangedDrones = drones.Where(x => x.Range >= range).ToList();
            foreach (var drone in rangedDrones)
            {
                drone.Available = false;
            }
            return rangedDrones;
        }
        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Drones available at {name}:");
            foreach (var item in drones.Where(x => x.Available))
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
