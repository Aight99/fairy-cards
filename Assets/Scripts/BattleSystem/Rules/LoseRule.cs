using System;

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
            _context.ThrowLose();
        }
    }
}