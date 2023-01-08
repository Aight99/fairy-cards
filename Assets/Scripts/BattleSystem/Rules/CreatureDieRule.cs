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
            for (int i = 0; i < _context.Field.Length; i++)
            {
                if (_context.Field[i].Health <= 0)
                {
                    Debug.Log($"DIE! On position {i}");
                    _context.Field[i] = null;
                }
            }
        }
    }
}