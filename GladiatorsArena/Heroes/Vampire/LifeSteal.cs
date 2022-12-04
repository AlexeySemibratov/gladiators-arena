namespace GladiatorsArena.Heroes.Vampire
{
    internal class LifeSteal
    {
        private const float LifeStealMultiplier = 0.5f;
        private const float LifeStealAdditionalMultiplier = 2f;


        private Hero _hero;

        public LifeSteal(Hero hero)
        {
            _hero = hero;
        }

        public int GetLifeStealAmount(int enemyHP)
        {
            float lifeStealMultiplier = CalculateLifeStealMultiplier(_hero.CurrentHP, enemyHP);

            int healAmount = (int)(_hero.BaseAttackDamage.DamageAmount * lifeStealMultiplier);

            return healAmount;
        }

        private float CalculateLifeStealMultiplier(int heroHP, int enemyHP)
        {
            return heroHP < enemyHP ? LifeStealMultiplier * LifeStealAdditionalMultiplier : LifeStealMultiplier;
        }
    }
}
