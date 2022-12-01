using GladiatorsArena.Heroes.AncientGolem;

namespace GladiatorsArena.Heroes.AncientGolem
{
    internal class StoneForm
    {
        private const float DamageReflectionMultiplier = 0.25f;
        private const int DamageDispersionPercent = 50;

        private const int MaximumPercent = 100;

        private readonly Random _random = new Random();

        private AncientGolem _hero;

        private bool _wasDamageReflected = false;

        public StoneForm(AncientGolem hero)
        {
            _hero = hero;
        }

        public void BeforeNewRound()
        {
            _wasDamageReflected = false;
        }

        public Damage CalculateDispersionDamage(Damage damage)
        {
            int randomDispersionPercent = _random.Next(-DamageDispersionPercent, DamageDispersionPercent);
            int randomDamage = damage.DamageAmount + (damage.DamageAmount * randomDispersionPercent / MaximumPercent);

            return new Damage(randomDamage, damage.Type);
        }

        public bool ShouldReflectDamage(Damage incomingDamage)
        {
            return incomingDamage.Type == DamageType.Physical && _wasDamageReflected == false;
        }

        public void ReflectDamage(Hero target, Damage incomingDamage)
        {
             _wasDamageReflected = true;

             int reflectedDamageAmount = (int)(incomingDamage.DamageAmount * DamageReflectionMultiplier);
             var reflectedDamage = new Damage(reflectedDamageAmount, incomingDamage.Type);

             target.RecieveDamage(_hero, reflectedDamage);
        }
    }
}
