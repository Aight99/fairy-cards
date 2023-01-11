using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class AttackRule : IRule
    {
        private static readonly int[] EnemySide = {5, 6, 7, 8, 9};
        // private static readonly int[] PlayerSide = {4, 3, 2, 1, 0};
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
            
            var attack = _context.Field[command.UserIndex].CurrentAttack;
            var targets = GetTargets(command.UserIndex, command.TargetIndex, attack.AttackType);
            DebugTargets(targets, attack.AttackType);
            foreach (var targetIndex in targets)
            {
                if (_context.Field[targetIndex] != null)
                {
                    ApplyDamage(targetIndex, attack.Damage);
                }
            }
        }

        private void DebugTargets(List<int> targets, AttackType type)
        {
            Debug.Log($"<color=red>Attack!</color> {type}: [{string.Join(",", targets)}]");
        }

        private List<int> GetTargets(int userIndex, int aimIndex, AttackType attackType)
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
        
        private void ApplyDamage(int targetIndex, int damage)
        {
            var target = _context.Field[targetIndex];
            if (damage <= 0)
            {
                return;
            }
            if (target.Shields != 0)
            {
                var shieldsDamage = (target.Shields >= damage) ? damage : target.Shields;
                target.Shields -= shieldsDamage;
                damage -= shieldsDamage;
                _context.SetStatusEffect(targetIndex, EffectType.Shield, target.Shields);
            }
            if (damage != 0)
            {
                target.Health -= damage;
                _context.ChangeHealth(targetIndex, target.Health);
            }
        }
    }
}