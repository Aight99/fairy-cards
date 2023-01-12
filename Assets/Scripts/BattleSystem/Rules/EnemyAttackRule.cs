using System;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class EnemyAttackRule : IRule
    {
        private readonly Context _context;
        
        public EnemyAttackRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            
        }
    }
}