using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class AwakeningRule : IRule
    {
        private readonly Context _context;
        private readonly Dictionary<string, Func<int, bool>> _awakeningPredicates;

        public AwakeningRule(Context context)
        {
            _context = context;
            _awakeningPredicates = new Dictionary<string, Func<int, bool>>();
            FillPredicates();
        }
        
        public void ApplyRule()
        {
            for (int i = 0; i < 10; i++)
            {
                var creature = _context.Field[i];
                if (creature != null && !creature.IsAwakened && _awakeningPredicates[creature.Name].Invoke(i))
                {
                    Debug.Log($"<color=blue>AWAKE!</color> On position {i}");
                    creature.IsAwakened = true;
                    _context.AwakeCharacter(i);
                }
            }
        }

        private void FillPredicates()
        {
            _awakeningPredicates.Add("Гретель", (i) => _context.TurnNumber >= 4);
            _awakeningPredicates.Add("Гензель", (i) => _context.TurnNumber >= 4);
            _awakeningPredicates.Add("Колобок", (i) => _context.CreatureMoveCount >= 5);
            _awakeningPredicates.Add("Кощей", (i) => _context.UsedManaCount >= 12);
            _awakeningPredicates.Add("Русалочка", (i) => _context.DamageApplyCount >= 10);
            _awakeningPredicates.Add("Красная Шапочка", (i) => _context.Field[i].AttackCount >= 3);
            _awakeningPredicates.Add("Снежная Королева", (i) => _context.Field[i].Health <= _context.Field[i].CreatureData.Health / 2);
            _awakeningPredicates.Add("Супер Кощей", (i) => false);
            _awakeningPredicates.Add("Колобок 2007", (i) => false);
        }
    }
}