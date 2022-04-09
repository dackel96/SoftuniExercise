using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string name;
        private bool canRace = false;
        private IFormulaOneCar car;
        private int wins;


        public Pilot(string fullName)
        {
            FullName = fullName;
        }

        public string FullName
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                }
                this.name = value;
            }
        }

        public IFormulaOneCar Car
        {
            get
            {
                return this.car;
            }
            private set
            {
                this.car = value;
                if (this.car == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCarForPilot);
                }
            }
        }

        public int NumberOfWins
            => this.wins;

        public bool CanRace
            => this.canRace;

        public void AddCar(IFormulaOneCar car)
        {
            this.Car = car;
            this.canRace = true;
        }

        public void WinRace()
        {
            this.wins++;
        }
        public override string ToString()
        {
            return $"Pilot {this.FullName} has {this.NumberOfWins} wins.";
        }
    }
}
