using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class CardUsageRule : IRule
    {
        private readonly Context _context;
        
        public CardUsageRule(Context context)
        {
            _context = context;
        }
        
        // Если нет конкретной единичной цели / это не эффект сигны, то нельзя использовать SelfTarget эффекты 
        
        public void ApplyRule()
        {
            var command = _context.CurrentCommand;
            if (command.IsCard())
            {
                ApplyCardEffects(command);
            }
        }

        private void ApplyCardEffects(Command command)
        {
            var targets = GetTargets(command, out var user);
            var effects = command.Card.Effects;
            if (command.Card is SignatureCardData card)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (_context.Field[i].CreatureData.SignatureCard == card)
                    {
                        user = i;
                        Debug.Log($"<color=green>Special Card!</color> {card.Name} with {_context.Field[i].Name}");
                        effects = card.SpecialEffects;
                    }
                }
            }
            _context.ApplyAdditionalEffects(targets, user, effects, false);
            _context.ApplyAdditionalEffects(targets, user, effects, true);
        }

        private List<int> GetTargets(Command command, out int user)
        {
            switch (command.Card.TargetType)
            {
                case CardTargetType.None:
                    user = -1;
                    return new List<int>(new [] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9});
                case CardTargetType.Single:
                    user = command.TargetIndex;
                    return new List<int>(1) {command.TargetIndex};
                case CardTargetType.Side:
                    user = -1;
                    return (command.TargetIndex < 5)
                        ? new List<int>(new[] {0, 1, 2, 3, 4})
                        : new List<int>(new[] {5, 6, 7, 8, 9});
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}