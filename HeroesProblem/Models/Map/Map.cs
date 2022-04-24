namespace Heroes.Models.Map
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Utilities;
    public class Map : IMap
    {
        public Map()
        {
        }
        public string Fight(ICollection<IHero> players)
        {
            string results = string.Empty;
            List<IHero> barbarians = players.Where(x => x.GetType().Name == "Barbarian").ToList();
            List<IHero> knights = players.Where(x => x.GetType().Name == "Knight").ToList();
            bool isSomeoneWin = false;
            while (!isSomeoneWin)
            {
                foreach (var knight in knights)
                {
                    foreach (var barbarian in barbarians)
                    {
                        if (barbarian.IsAlive && barbarian.Weapon != null && knight.Weapon != null)
                        {
                            barbarian.TakeDamage(knight.Weapon.DoDamage());
                        }
                    }
                }
                if (barbarians.All(x => x.IsAlive == false))
                {
                    int deaths = 0;
                    foreach (var knight in knights)
                    {
                        if (knight.IsAlive == false)
                        {
                            deaths++;
                        }
                    }
                    results = string.Format(OutputMessages.KnightsWin, deaths);
                    isSomeoneWin = true;
                }
                foreach (var barbarian in barbarians)
                {
                    foreach (var knight in knights)
                    {
                        if (knight.IsAlive && knight.Weapon != null && barbarian.Weapon != null)
                        {
                            knight.TakeDamage(barbarian.Weapon.DoDamage());
                        }
                    }
                }
                if (knights.All(x => x.IsAlive == false))
                {
                    int deaths = 0;
                    foreach (var barbarian in barbarians)
                    {
                        if (barbarian.IsAlive == false)
                        {
                            deaths++;
                        }
                    }
                    results = string.Format(OutputMessages.BarbariansWin, deaths);
                    isSomeoneWin = true;
                }
            }
            return results;
        }
    }
}
