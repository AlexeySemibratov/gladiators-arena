namespace GladiatorsArena.Arena
{
    /// <summary>
    /// Базовый интерфейс для участника арены
    /// </summary>
    internal interface IArenaEntity<E>
    {
        // Участник
        E Entity { get; }

        // Вызывается перед началом раунда, когда участники еще не проводили атаку
        void BeforeRound();

        /// <summary>
        /// Вызывается, когда участник совершает атаку по противнику.
        /// <param name="target">противник</param>
        /// </summary>
        void OnRound(E target);

        // Вызывается при окончании раунда, когда участники уже совершили атаку
        void AfterRound();
    }
}
