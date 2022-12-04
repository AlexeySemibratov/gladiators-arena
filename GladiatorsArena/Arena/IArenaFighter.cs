using GladiatorsArena.DamageData;

namespace GladiatorsArena.Arena
{
    /// <summary>
    /// Базовый интерфейс для участника арены
    /// </summary>
    internal interface IArenaFighter
    {
        /// <summary>
        /// Вызывается перед началом раунда, когда участники еще не проводили атаку
        /// </summary>
        void OnRoundStarted();

        /// <summary>
        /// Вызывается, когда участник совершает атаку по противнику.
        /// <param name="target">противник</param>
        /// </summary>
        void PerformRound(IDamageTarget target);

        /// <summary>
        /// Вызывается при окончании раунда, когда участники уже совершили атаку
        /// </summary>
        void OnRoundFinished();
    }
}
