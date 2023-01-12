using System;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class TurnGainRule : IRule
    {
        private readonly Context _context;
        private readonly int _cardGainPerTurn = 2;
        private readonly int _startingManaValue = 3;
        
        public TurnGainRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            _context.CurrentMana = _startingManaValue;
            _context.ChangeMana(_context.CurrentMana);
            _context.AddCardsInHand(_cardGainPerTurn);
        }
    }
}