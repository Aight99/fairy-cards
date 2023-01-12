using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleSystem.Rules
{
    public class EnemyTargetingRule : IRule
    {
        private readonly Context _context;
        private int[] _enemySide = {5, 6, 7, 8, 9};
        private int[] _playerSide = {0, 1, 2, 3, 4};

        public EnemyTargetingRule(Context context)
        {
            _context = context;
        }

        public void ApplyRule()
        {
            _enemySide = _enemySide.OrderBy(x => Random.Range(0, 10)).ToArray();
            _playerSide = _playerSide.OrderBy(x => Random.Range(0, 10)).ToArray();
            foreach (var angryIndex in _enemySide)
            {
                if (_context.Field[angryIndex] == null) continue;
                var forward = angryIndex - 5;
                var attackType = _context.Field[angryIndex].CurrentAttack.AttackType;
                if (attackType == AttackType.Lunge)
                {
                    if (_context.Field[forward] == null)
                    {
                        continue;
                    }
                    _context.NextEnemyToAttackIndex = angryIndex;
                    _context.EnemyIntentions = AttackRule.GetTargets(angryIndex, forward, attackType);
                    _context.SetEnemyIntentions(_context.EnemyIntentions);
                    return;
                }

                foreach (var defendIndex in _playerSide)
                {
                    if (_context.Field[defendIndex] == null) continue;
                    _context.NextEnemyToAttackIndex = angryIndex;
                    _context.EnemyIntentions = AttackRule.GetTargets(angryIndex, defendIndex, attackType);
                    _context.SetEnemyIntentions(_context.EnemyIntentions);
                    return;
                }
            }
        }
    }
}