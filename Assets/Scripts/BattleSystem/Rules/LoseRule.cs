using System;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class LoseRule : IRule
    {
        private readonly Context _context;
        
        public LoseRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            for (int i = 0; i < 5; i++)
            {
                if (_context.Field[i] != null)
                {
                    return;
                }
            }
            Debug.Log($"<color=yellow>YOU LOSE!</color>");
            _context.ThrowLose();
        }
    }
}