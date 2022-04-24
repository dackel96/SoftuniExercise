namespace Heroes.Utilities
{
    public static class OutputMessages
    {
        public const string KnightsWin = "The knights took {0} casualties but won the battle."; // 0 => deadBodys
        public const string BarbariansWin = "The barbarians took {0} casualties but won the battle."; // 0 => deadBodys

        public const string CreatedKnight = "Successfully added Sir {0} to the collection."; //0=> name
        public const string CreatedBarbarian = "Successfully added Barbarian {0} to the collection."; //0=> name
        public const string HeroTakeWeapon ="Hero {0} can participate in battle using a {1}.";//0=> heroName 1=> weaponType
    }
}
