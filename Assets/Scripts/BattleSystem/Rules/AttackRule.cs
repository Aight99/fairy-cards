using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class AttackRule : IRule
    {
        private static readonly int[] EnemySide = {5, 6, 7, 8, 9};
        private static readonly int[] PlayerSide = {0, 1, 2, 3, 4};
        private readonly Context _context;
        
        public AttackRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            var command = _context.CurrentCommand;
            if (!command.IsAttack())
            {
                return;
            }
            _context.Field[command.UserIndex].AttackCount++;
            
            var attack = _context.Field[command.UserIndex].CurrentAttack;
            var targets = (_context.IsPlayerTurn)
                ? GetTargets(command.UserIndex, command.TargetIndex, attack.AttackType)
                : _context.EnemyIntentions;
            DebugTargets(targets, attack.AttackType);
            
            _context.ApplyAdditionalEffects(targets, command.UserIndex, attack.AdditionalEffects, false);
            _context.StartHitAnimation(command.UserIndex, command.TargetIndex);
            foreach (var targetIndex in targets)
            {
                if (_context.Field[targetIndex] != null)
                {
                    _context.ApplyDamage(command.UserIndex, targetIndex, attack.Damage);
                }
            }
            _context.ApplyAdditionalEffects(targets, command.UserIndex, attack.AdditionalEffects, true);
        }

        private void DebugTargets(List<int> targets, AttackType type)
        {
            Debug.Log($"<color=red>Attack!</color> {type}: [{string.Join(",", targets)}]");
        }

        public static List<int> GetTargets(int userIndex, int aimIndex, AttackType attackType)
        {
            // Предполагаем, что юзер и цель всегда в разных командах
            var targetSide = (EnemySide.Contains(aimIndex)) ? EnemySide : PlayerSide;
            var relativeTargetIndex = (targetSide == EnemySide) ? aimIndex - 5 : aimIndex;
            var relativeUserIndex = (targetSide == EnemySide) ? userIndex : userIndex - 5;
            var targets = new List<int>();
            switch (attackType)
            {
                case AttackType.Single:
                    targets.Add(aimIndex);
                    break;
                case AttackType.Lunge:
                    targets.Add(targetSide[relativeUserIndex]);
                    break;
                case AttackType.Area:
                    targets.AddRange(targetSide);
                    break;
                case AttackType.Fan:
                    targets.Add(targetSide[relativeTargetIndex]);
                    if (relativeTargetIndex > 0)
                        targets.Add(targetSide[relativeTargetIndex - 1]);
                    if (relativeTargetIndex < 4)
                        targets.Add(targetSide[relativeTargetIndex + 1]);
                    break;
            }
            return targets;
        }
    }
}