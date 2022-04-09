using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Models.F1Cars;
using Formula1.Repositories;
using Formula1.Repositories.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IPilot> pilots;
        private readonly IRepository<IRace> races;
        private readonly IRepository<IFormulaOneCar> bolids;
        public Controller()
        {
            this.pilots = new PilotRepository();
            this.races = new RaceRepository();
            this.bolids = new FormulaOneCarRepository();
        }
        public string CreatePilot(string fullName)
        {
            IPilot pilot = new Pilot(fullName);
            if (pilots.FindByName(fullName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }
            this.pilots.Add(pilot);
            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (bolids.FindByName(model) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }
            if (type != "Ferrari" && type != "Williams")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }
            IFormulaOneCar bolid = null;
            if (type == "Ferrari")
            {
                bolid = new Ferrari(model, horsepower, engineDisplacement);
            }
            else if (type == "Williams")
            {
                bolid = new Williams(model, horsepower, engineDisplacement);
            }
            this.bolids.Add(bolid);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (races.FindByName(raceName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }
            IRace race = new Race(raceName, numberOfLaps);
            races.Add(race);
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            IFormulaOneCar bolid = this.bolids.FindByName(carModel);
            IPilot pilot = this.pilots.FindByName(pilotName);
            if (pilot == null || pilot.CanRace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }
            if (bolid == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }
            pilot.AddCar(bolid);
            this.bolids.Remove(bolid);
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, bolid.GetType().Name.ToString(), carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IPilot pilot = this.pilots.FindByName(pilotFullName);
            IRace race = this.races.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            if (pilot == null || pilot.CanRace == false || race.Pilots.Contains(pilot))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            race.AddPilot(pilot);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string StartRace(string raceName)
        {
            IRace race = races.FindByName(raceName);
            if (races.FindByName(raceName) == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            if (races.FindByName(raceName).Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }
            if (race.TookPlace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }
            StringBuilder sb = new StringBuilder();
            //???????????????
            List<IPilot> pilotss = race.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps)).ToList();

            int counter = 1;
            foreach (var pilot in pilotss)
            {
                if (counter == 1)
                {
                    sb.AppendLine(string.Format(OutputMessages.PilotFirstPlace, pilot.FullName, raceName));
                    pilot.WinRace();
                    counter++;
                }
                else if (counter == 2)
                {
                    sb.AppendLine(string.Format(OutputMessages.PilotSecondPlace, pilot.FullName, raceName));
                    counter++;
                }
                else if (counter == 3)
                {
                    sb.AppendLine(string.Format(OutputMessages.PilotThirdPlace, pilot.FullName, raceName));
                    counter++;
                }
            }
            race.TookPlace = true;
            return sb.ToString().TrimEnd();
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();
            List<IPilot> pilotss = this.pilots.Models.OrderByDescending(x => x.NumberOfWins).ToList();
            foreach (var pilot in pilotss)
            {
                sb.AppendLine(pilot.ToString().Trim());
            }
            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();
            List<IRace> racess = this.races.Models.Where(x => x.TookPlace == true).ToList();
            foreach (var race in racess)
            {
                sb.AppendLine(race.RaceInfo().Trim());
            }
            return sb.ToString().TrimEnd();
        }

    }
}
