namespace Heroes.Utilities
{
    public static class ExceptionMessages
    {
        public const string WeaponNullException = "Weapon type cannot be null or empty.";
        public const string DurabilityBelowZeroException = "Durability cannot be below 0.";

        public const string HeroNameNullException = "Hero name cannot be null or empty.";
        public const string HeroHpBelowZeroException = "Hero health cannot be below 0.";
        public const string HeroArmourBelowZeroException = "Hero armour cannot be below 0.";
        public const string HeroWeaponNullException = "Weapon cannot be null.";

        public const string AlredyExistingHero = "The hero {0} already exists."; // 0=> name
        public const string InvalidTypeHero = "Invalid hero type.";
        public const string InvalidWeaponType = "Invalid weapon type.";
        public const string AlredyExistWeapon = "The weapon {0} already exists."; //0=> name

        public const string InvalidHero = "Hero {0} does not exist.";//0=> name
        public const string InvalidWeapon = "Weapon {0} does not exist.";//0=> name
        public const string ArmedHero = "Hero {0} is well-armed.";//0=> name
    }
}
