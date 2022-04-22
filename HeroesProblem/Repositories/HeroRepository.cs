namespace Heroes.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Contracts;
    using Contracts;
    public class HeroRepository : IRepository<IHero>
    {
        private readonly List<IHero> heroes;
        public HeroRepository()
        {
            this.heroes = new List<IHero>();
        }
        public IReadOnlyCollection<IHero> Models
            => this.heroes.AsReadOnly();

        public void Add(IHero hero)
            => this.heroes.Add(hero);

        public IHero FindByName(string name)
            => this.heroes.FirstOrDefault(x => x.Name == name);

        public bool Remove(IHero hero)
            => this.heroes.Remove(hero);
    }
}
