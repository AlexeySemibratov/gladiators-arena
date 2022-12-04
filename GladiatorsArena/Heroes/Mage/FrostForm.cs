using GladiatorsArena.DamageData;

namespace GladiatorsArena.Heroes
{
    internal class FrostForm
    {
        private const int ManaCost = 100;
        private const int DamageMultiplier = 2;

        private Hero _mage;

        private bool _isShieldActive = false;
        private bool _isDamageBoostActive = false;

        public FrostForm(Hero mage)
        {
            _mage = mage;
        }

        public int GetManaCost() => ManaCost;

        public void ActivateAbility()
        {
            _isShieldActive = true;
            _isDamageBoostActive = true;
        }

        public Damage GetBuffedDamage(Damage damage)
        {
            if (_isDamageBoostActive)
            {
                _isDamageBoostActive = false;

                Damage newDamage = new Damage(
                    DamageAmount: damage.DamageAmount * DamageMultiplier,
                    Type: damage.Type
                    );

                return newDamage;
            }

            return damage;
        }

        public Damage ReduceDamage(Damage incomingDamage)
        {
            if (_isShieldActive)
            {
                _isShieldActive = false;

                return new Damage(
                    DamageAmount: 0,
                    Type: incomingDamage.Type
                    );
            }

            return incomingDamage;
        }
    }
}
