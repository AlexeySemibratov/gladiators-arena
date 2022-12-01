namespace GladiatorsArena.Heroes
{
    internal interface IMage
    {
        public event Action<Hero> FrostFormActivated;

        public int MaxMana { get; }
        public int CurrentMana { get; }
    }
}
