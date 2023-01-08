using System;
using System.Collections.Generic;

namespace BattleSystem
{
    public class Command
    {
        public bool IsEndingTurn { get; private set; }
        // public HandCardData Card { get; private set; }
        // public Attack Attack { get; private set; }
        
        public Creature User { get; private set; }
        public List<Creature> Targets { get; private set; }

        public Command()
        {
            IsEndingTurn = false;
            Targets = new List<Creature>();
        }
        
        public static Command CreateEmpty() => new Command();

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

        public Command SetUser(Creature user)
        {
            User = user;
            return this;
        }
        
        public Command AddTarget(Creature target)
        {
            Targets.Add(target);
            return this;
        }
        
    }
}