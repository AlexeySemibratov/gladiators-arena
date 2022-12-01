namespace GladiatorsArena.Heroes.ChaosKnight
{
    /// <summary>
    /// Рыцарь хаоса.
    /// Урон от базовой атаки: физический.
    /// Способность - Хаос. 
    /// Рыцарь облачен в броню хаоса, которая уменьшает весь входящий магический урон на 20%.
    /// Каждая атака имеет 30% шанс призвать копию рыцаря из хаос мира, которая нанесет противнику магический урон, равный 100% 
    /// от базовой атаки. Копия исчезает сразу после атаки.
    /// </summary>
    internal class ChaosKnight : Hero, IChaosKnight
    {
        public event Action<Hero> ChaosCopySummoned;

        private Chaos _chaos;

        public ChaosKnight(string name, int maxHP, Damage baseAttackDamage) : base(name, maxHP, baseAttackDamage)
        {
            _chaos = new Chaos(this);
        }

        public override void RecieveDamage(Hero target, Damage damage)
        {
            base.RecieveDamage(target, _chaos.GetReducedDamage(damage));
        }

        public override void DealDamage(Hero target)
        {
            base.DealDamage(target);
            if (_chaos.ShouldSummonChaosCopy())
            {
                ChaosCopySummoned?.Invoke(this);
                _chaos.SummonChaosCopy(target);
            }
        }
    }
}
