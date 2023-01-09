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
        
        public void ApplyRule()
        {
        }
    }
}