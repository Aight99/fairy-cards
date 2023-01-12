using System;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class ManaCostRule : IRule
    {
        private readonly Context _context;
        
        public ManaCostRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            var command = _context.CurrentCommand;
            if (command.IsCard())
            {
                _context.CurrentMana -= command.Card.ManaUsage;
            }
            if (command.IsAttack())
            {
                _context.CurrentMana -= _context.Field[command.UserIndex].CurrentAttack.ManaUsage;
            }
            _context.ChangeMana(_context.CurrentMana);
            Debug.Log($"<color=blue>YOU HAVE {_context.CurrentMana} MANA!</color>");
        }
    }
}