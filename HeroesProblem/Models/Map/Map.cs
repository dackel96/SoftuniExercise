namespace Heroes.Models.Map
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Utilities;
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            string results = null;
            List<IHero> barbarians = players.Where(x => x.GetType().Name == "Barbarian").ToList();
            List<IHero> knights = players.Where(x => x.GetType().Name == "Knight").ToList();
            int barbariansDeathBodies = 0;
            int knightsDeathBodies = 0;
            while (true)
            {
                for (int i = 0; i < knights.Count; i++)
                {
                    if (barbarians[i].IsAlive)
                    {
                        barbarians[i].TakeDamage(knights[i].Weapon.DoDamage());
                        if (!barbarians[i].IsAlive)
                        {
                            barbariansDeathBodies++;
                            barbarians.Remove(barbarians[i]);
                        }
                    }
                }
                if (!barbarians.Any())
                {
                    results = string.Format(OutputMessages.KnightsWin, knightsDeathBodies);
                    break;
                }
                for (int i = 0; i < barbarians.Count; i++)
                {
                    if (knights[i].IsAlive)
                    {
                        knights[i].TakeDamage(barbarians[i].Weapon.DoDamage());
                        if (!knights[i].IsAlive)
                        {
                            knightsDeathBodies++;
                            knights.Remove(knights[i]);
                        }
                    }
                }
                if (!knights.Any())
                {
                    results = string.Format(OutputMessages.BarbariansWin, barbariansDeathBodies);
                    break;
                }
            }
            return results;
        }
    }
}
