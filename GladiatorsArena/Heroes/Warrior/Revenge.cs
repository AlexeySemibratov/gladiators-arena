namespace GladiatorsArena.Heroes
{
    internal class Revenge
    {
        private const float RestorationRate = 0.15f;

        private Warrior _hero;

        private bool _wasActivated = false;

        public Revenge(Warrior warrior)
        {
            _hero = warrior;
        }

        public bool ShouldActivate()
        {
            return _wasActivated == false && _hero.IsDead();
        }

        public void Activate(Damage incomingDamage)
        {
            _wasActivated = true;
            ApplyBuffsToHero(incomingDamage.DamageAmount);
        }

        private void ApplyBuffsToHero(int damageToAdd)
        {
            int newHP = (int)(_hero.MaxHP * RestorationRate);
            
            Damage currentDamage = _hero.BaseAttackDamage;
            Damage newDamage = new Damage(
                DamageAmount: currentDamage.DamageAmount + damageToAdd,
                Type: currentDamage.Type
            );

            _hero.Heal(newHP);
            _hero.SetDamage(newDamage);
        }
    }
}
