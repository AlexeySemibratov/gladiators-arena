using GladiatorsArena.DamageData;

namespace GladiatorsArena.Heroes
{
    internal class Revenge
    {
        private const float RestorationRate = 0.15f;

        private Hero _hero;

        private bool _wasActivated = false;

        public Revenge(Hero warrior)
        {
            _hero = warrior;
        }

        public bool ShouldActivate()
        {
            return _wasActivated == false && _hero.CheckIsDead();
        }

        public void Activate()
        {
            _wasActivated = true;
        }

        public int GetHPRestorationAmount()
        {
            return (int)(_hero.MaxHP * RestorationRate);
        }

        public Damage GetBuffedDamage(Damage incomingDamage)
        {
            Damage currentDamage = _hero.BaseAttackDamage;
            Damage newDamage = new Damage(
                DamageAmount: currentDamage.DamageAmount + incomingDamage.DamageAmount,
                Type: currentDamage.Type
            );

            return newDamage;
        }
    }
}
