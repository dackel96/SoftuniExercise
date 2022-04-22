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
    }
}
