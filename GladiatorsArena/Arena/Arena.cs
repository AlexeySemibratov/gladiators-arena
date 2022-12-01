using GladiatorsArena.Heroes;
using GladiatorsArena.Heroes.AncientGolem;
using GladiatorsArena.Heroes.ChaosKnight;
using GladiatorsArena.Heroes.Vampire;

namespace GladiatorsArena.Arena
{
    internal class Arena
    {

        private IArenaEntity<Hero> _first_entity;
        private IArenaEntity<Hero> _second_entity;

        private ArenaCommentator _commentator;

        public Arena(IArenaEntity<Hero> firstEntity, IArenaEntity<Hero> secondEntity)
        {
            _first_entity = firstEntity;
            _second_entity = secondEntity;

            _commentator = new ArenaCommentator(firstEntity.Entity, secondEntity.Entity);
        }

        public void StartBattle()
        {
            _commentator.CommentBattleStart();

            int roundNumber = 1;

            while (_first_entity.Entity.IsDead() == false && _second_entity.Entity.IsDead() == false)
            {
                _commentator.CommentRoundStart(roundNumber++);

                _commentator.CommentBeforeRoundStarted();

                _first_entity.BeforeRound();
                _second_entity.BeforeRound();

                _commentator.CommentOnRoundStarted();

                _first_entity.OnRound(_second_entity.Entity);
                _second_entity.OnRound(_first_entity.Entity);

                _first_entity.AfterRound();
                _second_entity.AfterRound();

                _commentator.CommentRoundEnded();
            }
            _commentator.CommentBattleEnd();
        }

    }
}
