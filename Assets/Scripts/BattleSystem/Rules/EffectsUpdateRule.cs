using System;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class EffectsUpdateRule : IRule
    {
        private readonly Context _context;
        
        public EffectsUpdateRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            var borders = (_context.IsPlayerTurn) ? (0, 5) : (5, 10);
            for (int i = borders.Item1; i < borders.Item2; i++)
            {
                var creature = _context.Field[i];
                if (creature == null) continue;
                if (creature.Shields > 0)
                {
                    creature.Shields = 0;
                    _context.ChangeShields(i, 0);
                }
                foreach (var pair in creature.EffectsDuration)
                {
                    if (pair.Value > 0)
                    {
                        creature.EffectsDuration[pair.Key]--;
                        _context.SetStatusEffect(i, pair.Key, creature.EffectsDuration[pair.Key]);
                    }
                }
            }
        }
    }
}