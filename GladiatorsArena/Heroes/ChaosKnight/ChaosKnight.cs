using GladiatorsArena.DamageData;

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

        public ChaosKnight(string name, int maxHP, Damage baseAttackDamage) : base(name, maxHP, baseAttackDamage, HeroType.ChaosKnight)
        {
            _chaos = new Chaos(this);
        }

        public override void ReceiveDamage(IDamageTarget dealer, Damage damage)
        {
            base.ReceiveDamage(dealer, _chaos.GetReducedDamage(damage));
        }

        public override void DealDamage(IDamageTarget receiver)
        {
            base.DealDamage(receiver);
            if (_chaos.ShouldSummonChaosCopy())
            {
                ChaosCopySummoned?.Invoke(this);
                _chaos.SummonChaosCopy(receiver);
            }
        }
    }
}
