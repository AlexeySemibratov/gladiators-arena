using GladiatorsArena.DamageData;

namespace GladiatorsArena.Heroes.ChaosKnight
{
    internal class Chaos
    {
        private const float DamageReduceMultiplier = 0.8f;
        private const float ChaosSummonChance = 0.3f;

        private readonly Random _random = new Random();

        private Hero _hero;

        public Chaos(Hero hero)
        {
            _hero = hero;
        }

        public Damage GetReducedDamage(Damage damage)
        {
            if (damage.Type == DamageType.Magical)
            {
                var newAmount = (int)(damage.DamageAmount * DamageReduceMultiplier);
                var newDamage = new Damage(newAmount, damage.Type);
                return newDamage;
            } 
            else
            {
                return damage;
            }
        }

        public bool ShouldSummonChaosCopy()
        {
            return _random.NextDouble() <= ChaosSummonChance;
        }

        public void SummonChaosCopy(IDamageTarget target)
        {
            var summonDamage = new Damage(_hero.BaseAttackDamage.DamageAmount, DamageType.Magical);
            target.ReceiveDamage(_hero, summonDamage);
        }
    }
}
