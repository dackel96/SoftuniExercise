namespace Heroes.Models.Weapons
{
    using Contracts;
    using Utilities;
    using System;
    public abstract class Weapon : IWeapon
    {
        private string name;
        private int durability;

        protected Weapon(string name, int durability)
        {
            Name = name;
            Durability = durability;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.WeaponNullException);
                }
                this.name = value;
            }
        }

        public int Durability
        {
            get
            {
                return this.durability;
            }
            protected set
            {
                this.durability = value;
                if (this.durability < 0)
                {
                    throw new ArgumentException(ExceptionMessages.DurabilityBelowZeroException);
                }
            }
        }

        public abstract int DoDamage();
    }
}
