namespace Heroes.Models.Heroes
{
    using Contracts;
    using Utilities;
    using System;
    public abstract class Hero : IHero
    {
        private string name;
        private int hp;
        private int armour;
        private IWeapon weapon;

        protected Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
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
                    throw new ArgumentException(ExceptionMessages.HeroNameNullException);
                }
                this.name = value;
            }
        }

        public int Health
        {
            get
            {
                return this.hp;
            }
            private set
            {
                this.hp = value;
                if (this.hp < 0)
                {
                    this.hp = 0;
                    throw new ArgumentException(ExceptionMessages.HeroHpBelowZeroException);
                }
            }
        }

        public int Armour
        {
            get
            {
                return this.armour;
            }
            private set
            {
                this.armour = value;
                if (this.armour < 0)
                {
                    this.armour = 0;
                    throw new ArgumentException(ExceptionMessages.HeroArmourBelowZeroException);
                }
            }
        }

        public IWeapon Weapon
        {
            get
            {
                return this.weapon;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.HeroWeaponNullException);
                }
                this.weapon = value;
            }
        }

        public bool IsAlive
            => this.Health == 0 ? true : false;

        public void AddWeapon(IWeapon weapon)
        {
            //!!!!!!!!!!!!!!!!!!!!!
            if (this.Weapon == null)
            {
                this.Weapon = weapon;
            }
        }

        public void TakeDamage(int points)
        {
            if (this.Armour > 0)
            {
                this.Armour -= points;
            }
            else if (this.Armour == 0 && this.Health > 0)
            {
                this.Health -= points;
            }
        }
    }
}
