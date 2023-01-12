using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BattleSystem.Rules
{
    public class MoveRule : IRule
    {
        private readonly int[] _allySpawnOrder = {2, 1, 3, 0, 4};
        private readonly int[] _enemySpawnOrder = {7, 6, 8, 5, 9};
        private readonly Context _context;
        
        public MoveRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            var command = _context.CurrentCommand;
            if (command.MoveIndex != -1)
            {
                var isTargetPlayerSide = command.MoveIndex < 5;
                if (_context.Field[command.UserIndex].IsPlayer != isTargetPlayerSide)
                {
                    throw new ArgumentException();
                }
                var targetIndex = (_context.Field[command.MoveIndex] == null) ? command.MoveIndex : FindClearPlace();
                _context.Field[command.MoveIndex] = _context.Field[command.UserIndex];
                _context.Field[command.UserIndex] = null;
                _context.ChangePosition(command.UserIndex, command.MoveIndex);
                if (_context.IsPlayerTurn)
                {
                    _context.CreatureMoveCount++;
                }
            }
        }

        private int FindClearPlace()
        {
            var targetSide = (_context.Field[_context.CurrentCommand.MoveIndex].IsPlayer) ? _allySpawnOrder : _enemySpawnOrder;
            foreach (var i in targetSide)
            {
                if (_context.Field[i] == null)
                {
                    return i;
                }
            }
            throw new ArgumentException();
        }
    }
}