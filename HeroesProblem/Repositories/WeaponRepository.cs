namespace Heroes.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Contracts;
    using Contracts;
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> weapons;
        public WeaponRepository()
        {
            this.weapons = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models
            => this.weapons.AsReadOnly();

        public void Add(IWeapon weapon)
            => this.weapons.Add(weapon);

        public IWeapon FindByName(string name)
            => this.weapons.FirstOrDefault(x => x.Name == name);

        public bool Remove(IWeapon weapon)
            => this.weapons.Remove(weapon);
    }
}
