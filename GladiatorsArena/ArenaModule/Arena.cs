using GladiatorsArena.Heroes;

namespace GladiatorsArena.ArenaModule
{
    internal class Arena
    {
        private Hero _firstFighter = null;
        private Hero _secondFighter = null;

        private ArenaCommentator _commentator;

        public Arena()
        {
        }

        public void SetFirstFighter(Hero hero)
        {
            _firstFighter = hero;
        }

        public void SetSecondFighter(Hero hero)
        {
            _secondFighter = hero;
        }

        public void StartBattle()
        {
            if (CheckHeroesExist() == false)
            {
                Console.WriteLine("Невозможно начать битву когда отсутствует хотя бы один из героев!");
                return;
            }

            _commentator = new ArenaCommentator(_firstFighter, _secondFighter);

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

        private bool CheckHeroesExist()
        {
            return _firstFighter != null && _secondFighter != null;
        }
    }
}
