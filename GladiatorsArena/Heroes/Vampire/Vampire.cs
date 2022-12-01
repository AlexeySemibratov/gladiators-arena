﻿namespace GladiatorsArena.Heroes.Vampire
{
    /// <summary>
    /// Вампир.
    /// Урон от базовой атаки: физический.
    /// Способность - Кража Жизни. 
    /// Каждая атака излечивает вампира на долю, равную 50% от его базовой атаки.
    /// Вампиризм увеличвается вдвое, если текущее здоровье вампира меньше, чем у его противника.
    /// </summary>
    internal class Vampire : Hero, IVampire
    {
        private LifeSteal _lifeSteal;

        public Vampire(string name, int maxHP, Damage baseAttackDamage) : base(name, maxHP, baseAttackDamage)
        {
            _lifeSteal = new LifeSteal(this);
        }

        public override void DealDamage(Hero target)
        {
            base.DealDamage(target);
            _lifeSteal.ApplyLifeSteal(target.CurrentHP);
        }
    }
}
