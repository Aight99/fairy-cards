using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class AwakeningRule : IRule
    {
        private readonly Context _context;
        
        public AwakeningRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
        }
    }
}