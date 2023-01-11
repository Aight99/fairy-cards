using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class MoveRule : IRule
    {
        private readonly Context _context;
        
        public MoveRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            var command = _context.CurrentCommand;
            // TODO Добавить проверку на то, чтобы индексы были от одной стороны
            if (command.MoveIndex != -1 && _context.Field[command.MoveIndex] == null)
            {
                _context.Field[command.MoveIndex] = _context.Field[command.UserIndex];
                _context.Field[command.UserIndex] = null;
                _context.ChangePosition(command.UserIndex, command.MoveIndex);
                if (_context.IsPlayerTurn)
                {
                    _context.CreatureMoveCount++;
                }
            }
        }
    }
}