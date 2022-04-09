using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string name;
        private int laps;
        private bool place = false;
        private List<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            this.pilots = new List<IPilot>();
        }

        public string RaceName
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRaceName, value));
                }
                this.name = value;
            }
        }

        public int NumberOfLaps
        {
            get
            {
                return this.laps;
            }
            private set
            {
                this.laps = value;
                if (this.laps < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidLapNumbers, value));
                }
            }
        }

        public bool TookPlace
        {
            get
            {
                return this.place;
            }
            set
            {
                this.place = value;
            }
        }

        public ICollection<IPilot> Pilots
            => this.pilots;

        public void AddPilot(IPilot pilot)
        {
            this.pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            //"The { race name } race has:
            //Participants: { number of participants }
            //Number of laps: { number of laps }
            //Took place: { Yes / No }
            //"
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The {this.RaceName} race has:");
            sb.AppendLine($"Participants: {this.Pilots.Count}");
            sb.AppendLine($"Number of laps: {this.NumberOfLaps}");
            string yesNo = "No";
            if (this.TookPlace)
            {
                yesNo = "Yes";
            }
            sb.AppendLine($"Took place: {yesNo}");
            return sb.ToString().TrimEnd();
        }
    }
}
