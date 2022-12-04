namespace GladiatorsArena.DamageData
{
    /// <summary>
    /// Базовый интерфейс для цели, которая может наносить и принимать урон.
    /// </summary>
    public interface IDamageTarget
    {
        /// <summary>
        /// Максимальное количество очков здоровья цели
        /// </summary>
        public int MaxHP { get; }

        /// <summary>
        /// Текущее количество очков здоровья цели
        /// </summary>
        public int CurrentHP { get; }

        /// <summary>
        /// Вызывается, когда объект наносит урон по цели
        /// </summary>
        /// <param name="receiver">Цель,по которой наносится урон</param>
        void DealDamage(IDamageTarget receiver);


        /// <summary>
        /// Вызывается, когда объект получает урон.
        /// </summary>
        /// <param name="dealer">Цель, которая наносит урон</param>
        /// <param name="damage">Входящий урон</param>
        void ReceiveDamage(IDamageTarget dealer, Damage damage);
    }
}
