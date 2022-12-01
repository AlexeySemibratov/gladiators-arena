namespace GladiatorsArena.Heroes
{
    internal class FrostForm
    {
        private const int ManaCost = 100;
        private const int DamageMultiplier = 2;

        private Mage _mage;

        private bool _isShieldActive = false;
        private bool _isDamageBoostActive = false;

        public FrostForm(Mage mage)
        {
            _mage = mage;
        }

        public bool IsEnoughMana()
        {
            return _mage.CurrentMana >= ManaCost;
        }

        public void ActivateAbility()
        {
            _mage.RemoveMana(ManaCost);

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
