using System;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class WinRule : IRule
    {
        private readonly Context _context;
        
        public WinRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            for (int i = 5; i < 10; i++)
            {
                if (_context.Field[i] != null)
                {
                    return;
                }
            }
            Debug.Log($"<color=green>YOU WON!</color>");
            _context.ThrowWin();
        }
    }
}