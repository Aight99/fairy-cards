using System;

namespace BattleSystem.Rules
{
    public class EndTurnRule : IRule
    {
        private readonly Context _context;
        
        public EndTurnRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            if (_context.CurrentCommand.IsEndingTurn)
            {
                _context.EndTurn();
            }
        }
    }
}