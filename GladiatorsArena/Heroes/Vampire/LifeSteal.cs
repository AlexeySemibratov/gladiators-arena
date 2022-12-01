namespace GladiatorsArena.Heroes.Vampire
{
    internal class LifeSteal
    {
        private const float LifeStealMultiplier = 0.5f;
        private const float LifeStealAdditionalMultiplier = 2f;


        private Vampire _hero;
        public LifeSteal(Vampire hero)
        {
            _hero = hero;
        }

        public void ApplyLifeSteal(int enemyHP)
        {
            if (_hero.IsDead())
            {
                return;
            }

            float lifeStealMultiplier = CalculateLifeStealMultiplier(_hero.CurrentHP, enemyHP);

            int healAmount = (int)(_hero.BaseAttackDamage.DamageAmount * lifeStealMultiplier);

            _hero.Heal(healAmount);
        }

        private float CalculateLifeStealMultiplier(int heroHP, int enemyHP)
        {
            return heroHP < enemyHP ? LifeStealMultiplier * LifeStealAdditionalMultiplier : LifeStealMultiplier;
        }
    }
}
