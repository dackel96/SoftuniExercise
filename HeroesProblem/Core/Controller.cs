using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using Heroes.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IHero> heroes;
        private readonly IRepository<IWeapon> weapons;
        private IMap map;
        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
            this.map = new Map();
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero currHero = null;
            string output = null;
            if (type == "Knight" || type == "Barbarian")
            {
                if (type == "Knight")
                {
                    currHero = new Knight(name, health, armour);
                    output = string.Format(OutputMessages.CreatedKnight, name);
                }
                else
                {
                    currHero = new Barbarian(name, health, armour);
                    output = string.Format(OutputMessages.CreatedBarbarian, name);
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidTypeHero);
            }
            if (heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.AlredyExistingHero, name));
            }
            else
            {
                this.heroes.Add(currHero);
            }
            return output;
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon currWeapon = null;
            if (type != "Claymore" && type != "Mace")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidWeaponType);
            }
            else
            {
                if (type == "Claymore")
                {
                    currWeapon = new Claymore(name, durability);
                }
                else
                {
                    currWeapon = new Mace(name, durability);
                }
            }
            if (weapons.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.AlredyExistWeapon, name));
            }
            this.weapons.Add(currWeapon);
            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (heroes.FindByName(heroName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidHero, heroName));
            }
            if (weapons.FindByName(weaponName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidWeapon, weaponName));
            }
            IHero currHero = heroes.FindByName(heroName);
            IWeapon currWeapon = weapons.FindByName(weaponName);
            if (currHero.Weapon != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ArmedHero, heroName));
            }
            currHero.AddWeapon(currWeapon);
            this.weapons.Remove(currWeapon);
            return string.Format(OutputMessages.HeroTakeWeapon, heroName, currWeapon.GetType().Name.ToString().ToLower());
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();
            List<IHero> sortedHeroes = this.heroes.Models.OrderBy(x => x.GetType().ToString()).ThenByDescending(x => x.Health).ThenBy(x => x.Name).ToList();
            foreach (var hero in sortedHeroes)
            {
                sb.AppendLine($"{hero.GetType().Name.ToString()}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                if (hero.Weapon == null)
                {
                    sb.AppendLine("--Weapon: Unarmed");
                }
                else
                {
                    sb.AppendLine($"--Weapon: {hero.Weapon.Name}");
                }
            }
            return sb.ToString().TrimEnd();
        }

        public string StartBattle()
        {
            List<IHero> geroi = this.heroes.Models.ToList();
            string rslt = this.map.Fight(geroi);
            return rslt;
        }
    }
}
