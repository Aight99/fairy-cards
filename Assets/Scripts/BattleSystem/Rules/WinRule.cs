using System;

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
            if (_context.Enemies.Count == 0)
            {
                _context.ThrowWin();
            }
        }
    }
}