using System;
using System.Collections.Generic;

namespace BattleSystem
{
    public class Context
    {
        public static event Action PlayerTurnEnded;
        public static event Action PlayerWon;

        public Context()
        {
            Enemies = new List<Creature>();
            Allies = new List<Creature>();
            MaxMana = 0;
            CurrentMana = 0;
        }

        public List<Creature> Enemies { get; private set; }
        public List<Creature> Allies { get; private set; }
        public int MaxMana { get; private set; }
        public int CurrentMana { get; private set; }
        public Command CurrentCommand { get; set; }
        
        // Hand info
        // etc

        public void EndTurn() => PlayerTurnEnded?.Invoke();

        public void ThrowWin() => PlayerWon?.Invoke();
    }
}