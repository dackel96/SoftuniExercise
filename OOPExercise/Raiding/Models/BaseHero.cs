using System;
using System.Collections.Generic;
using System.Text;

namespace Raiding
{
    public class BaseHero : IHero
    {
        public BaseHero(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public virtual int Power { get; protected set; }

        public virtual void CastAbility()
        {
            if (GetType().Name == "Paladin" || GetType().Name == "Druid")
            {
                Console.WriteLine($"{GetType().Name} - {this.Name} healed for {this.Power}");
            }
            else if (GetType().Name == "Warrior" || GetType().Name == "Rogue")
            {
                Console.WriteLine($"{GetType().Name} - {this.Name} hit for {this.Power} damage");
            }
        }
       
    }
}
