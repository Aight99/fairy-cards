using System;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class EnemyTargetingRule : IRule
    {
        private readonly Context _context;
        
        public EnemyTargetingRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            
        }
    }
}