using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace BattleSystem
{
    public class Command
    {
        public bool IsEndingTurn { get; private set; }
        public HandCardData Card { get; private set; }
        public int UserIndex { get; private set; }
        public int TargetIndex { get; private set; }
        public int MoveIndex { get; private set; }

        private Command()
        {
            UserIndex = -1;
            TargetIndex = -1;
            MoveIndex = -1;
            IsEndingTurn = false;
        }

        public static Command CreateEmpty() => new Command();
        public static Command EndTurnCommand() => new Command()
            .SetTurnEnd();
        public static Command AttackCommand(int userIndex, int targetIndex) => new Command()
            .SetUser(userIndex)
            .SetTarget(targetIndex)
            .SetTurnEnd();

        public static Command MoveCommand(int card, int target) => new Command()
            .SetUser(card)
            .SetMoveTarget(target);

        public static Command PlayCardCommand(HandCardData handCard) => new Command()
            .SetCardToPlay(handCard);

        public Command SetTurnEnd()
        {
            IsEndingTurn = true;
            return this;
        }
        
        public Command SetCardToPlay(HandCardData card)
        {
            Card = card;
            return this;
        }

        public Command SetMoveTarget(int targetIndex)
        {
            MoveIndex = targetIndex;
            return this;
        }
        
        public Command SetUser(int userIndex)
        {
            UserIndex = userIndex;
            return this;
        }
        
        public Command SetTarget(int targetIndex)
        {
            TargetIndex = targetIndex;
            return this;
        }
        
    }
}