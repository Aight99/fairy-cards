using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class CreatureDieRule : IRule
    {
        private readonly Context _context;
        
        public CreatureDieRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            Debug.Log($"User index:  {_context.CurrentCommand.UserIndex}; target index: {_context.CurrentCommand.TargetIndex}");

            for (int i = 0; i < _context.Field.Length; i++)
            {
                if (_context.Field[i]?.Health <= 0)
                {
                    Debug.Log($"<color=red>DEATH!</color> On position {i}");
                    _context.Field[i] = null;
                }
            }
        }
    }
}