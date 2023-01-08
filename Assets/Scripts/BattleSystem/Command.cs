using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace BattleSystem
{
    public class Command
    {
        public bool IsEndingTurn { get; private set; }
        // public HandCardData Card { get; private set; }
        // public Attack Attack { get; private set; }
        
        public int UserIndex { get; private set; }
        public int TargetIndex { get; private set; }

        private Command()
        {
            IsEndingTurn = false;
        }
        
        public static Command CreateEmpty() => new Command();
        public static Command EndTurnCommand() => new Command()
            .SetTurnEnd();
        public static Command AttackCommand(int userIndex, int targetIndex) => new Command()
            .SetUser(userIndex)
            .SetTarget(targetIndex)
            .SetTurnEnd();
        
        public static Command PlayCardCommand() => new Command()
            .SetTurnEnd();

        public Command SetTurnEnd()
        {
            IsEndingTurn = true;
            return this;
        }
        
        // public Command SetCardToPlay(HandCardData card)
        // {
        //     Card = card;
        //     return this;
        // }
        //
        // public Command SetAttack(Attack attack)
        // {
        //     Attack = attack;
        //     return this;
        // }

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