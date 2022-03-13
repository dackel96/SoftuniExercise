using System;
using System.Collections.Generic;
using System.Linq;

namespace Raiding
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int totalHitPoints = 0;
            Queue<BaseHero> heroRaid = new Queue<BaseHero>();
            for (int i = 0; i < n; i++)
            {
                string name = Console.ReadLine();
                string type = Console.ReadLine();
                BaseHero hero = TypeOfHero(type, name);
                if (hero == null)
                {
                    Console.WriteLine("Invalid hero!");
                    i--;
                    continue;
                }
                heroRaid.Enqueue(hero);
                totalHitPoints += hero.Power;
            }
            int bossHp = int.Parse(Console.ReadLine());
            while (heroRaid.Any())
            {
                BaseHero currHero = heroRaid.Dequeue();
                currHero.CastAbility();
            }
            if (totalHitPoints >= bossHp)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
        public static BaseHero TypeOfHero(string type, string name)
        {
            if (type == "Druid")
            {
                return new Druid(name);
            }
            else if (type == "Paladin")
            {
                return new Paladin(name);
            }
            else if (type == "Rogue")
            {
                return new Rogue(name);
            }
            else if (type == "Warrior")
            {
                return new Warrior(name);
            }
            return null;
        }

    }
}
