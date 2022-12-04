using GladiatorsArena.Heroes;

namespace GladiatorsArena.ArenaModule
{
    internal class Arena
    {
        public event Action BattleFinished;

        private Hero _firstFighter;
        private Hero _secondFighter;

        private ArenaCommentator _commentator;

        public Arena(Hero firstHero, Hero secondHero)
        {
            _firstFighter = firstHero;
            _secondFighter = secondHero;

            _commentator = new ArenaCommentator(firstHero, secondHero);
        }

        public void StartBattle()
        {
            _commentator.CommentBattleStart();

            int roundNumber = 1;

            while (_firstFighter.CheckIsDead() == false && _secondFighter.CheckIsDead() == false)
            {
                _commentator.CommentRoundStart(roundNumber++);

                StartRound();

                PerformRound();

                EndRound();
            }
            _commentator.CommentBattleEnd();

            BattleFinished?.Invoke();
        }

        private void StartRound()
        {
            _commentator.CommentBeforeRoundStarted();

            _firstFighter.OnRoundStarted();
            _secondFighter.OnRoundStarted();
        }


        private void PerformRound()
        {
            _commentator.CommentOnRoundStarted();

            _firstFighter.PerformRound(_secondFighter);
            _secondFighter.PerformRound(_firstFighter);
        }

        private void EndRound()
        {
            _firstFighter.OnRoundFinished();
            _secondFighter.OnRoundFinished();

            _commentator.CommentRoundEnded();
        }
    }
}
