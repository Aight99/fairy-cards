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
            var targets = GetTargets(command.UserIndex, command.TargetIndex, attack.AttackType);
            DebugTargets(targets, attack.AttackType);
            
            ApplyAdditionalEffects(targets, command.UserIndex, attack.AdditionalEffects, false);
            foreach (var targetIndex in targets)
            {
                if (_context.Field[targetIndex] != null)
                {
                    ApplyDamage(command.UserIndex, targetIndex, attack.Damage);
                }
            }
            ApplyAdditionalEffects(targets, command.UserIndex, attack.AdditionalEffects, true);
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

        // Этот ужас можно отрефкторить красиво, но делать я этого, конечно, не буду
        private void ApplyAdditionalEffects(List<int> targets, int user, List<AdditionalEffect> effects, bool isAfterAttack)
        {
            foreach (var effect in effects)
            {
                if (effect.IsAfterAttack != isAfterAttack)
                {
                    continue;
                }
                if (effect.EffectType == EffectType.ManaGain)
                {
                    _context.CurrentMana += effect.EffectParameter;
                    _context.ChangeMana(_context.CurrentMana);
                }
                else if (effect.EffectType == EffectType.CardGain)
                {
                    _context.AddCardsInHand(effect.EffectParameter);
                }
                else if (effect.EffectType == EffectType.FreeAttack)
                {
                    _context.CurrentCommand.SetTurnEnd(false);
                }
                else if (effect.EffectType == EffectType.Move)
                {
                    _context.CurrentCommand.MoveIndex = _context.CurrentCommand.UserIndex;
                }
                else if (effect.EffectType == EffectType.Damage)
                {
                    if (effect.IsSelfTarget)
                    {
                        ApplyDamage(user, user, effect.EffectParameter);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            ApplyDamage(user, target, effect.EffectParameter);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.Heal)
                {
                    if (effect.IsSelfTarget)
                    {
                        _context.Field[user].Health += effect.EffectParameter;
                        _context.ChangeHealth(user, _context.Field[user].Health);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            _context.Field[target].Health += effect.EffectParameter;
                            _context.ChangeHealth(target, _context.Field[target].Health);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.Heal)
                {
                    if (effect.IsSelfTarget)
                    {
                        _context.Field[user].Health += effect.EffectParameter;
                        _context.ChangeHealth(user, _context.Field[user].Health);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            _context.Field[target].Health += effect.EffectParameter;
                            _context.ChangeHealth(target, _context.Field[target].Health);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.Shield)
                {
                    if (effect.IsSelfTarget)
                    {
                        _context.Field[user].Shields += effect.EffectParameter;
                        _context.ChangeShields(user, _context.Field[user].Shields);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            _context.Field[target].Shields += effect.EffectParameter;
                            _context.ChangeShields(target, _context.Field[target].Shields);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.HalfLife)
                {
                    if (effect.IsSelfTarget)
                    {
                        ApplyDamage(user, user, _context.Field[user].Health / 2);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            ApplyDamage(user, target, _context.Field[target].Health / 2);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.Vampire)
                {
                    if (effect.IsSelfTarget)
                    {
                        _context.Field[user].EffectsDuration.Add(EffectType.Vampire, effect.EffectParameter);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            _context.Field[target].EffectsDuration.Add(EffectType.Vampire, effect.EffectParameter);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.Silence)
                {
                    if (effect.IsSelfTarget)
                    {
                        _context.Field[user].EffectsDuration.Add(EffectType.Silence, effect.EffectParameter);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            _context.Field[target].EffectsDuration.Add(EffectType.Silence, effect.EffectParameter);
                        }
                    }
                }
            }
        }

        private void ApplyDamage(int userIndex, int targetIndex, int damage)
        {
            var user = _context.Field[userIndex];
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
                _context.ChangeShields(targetIndex, target.Shields);
            }
            if (damage != 0)
            {
                target.Health -= damage;
                if (user.EffectsDuration.ContainsKey(EffectType.Vampire))
                {
                    user.Health += damage;
                }
                _context.ChangeHealth(targetIndex, target.Health);
            }
            if (_context.IsPlayerTurn)
            {
                _context.DamageApplyCount++;
            }
        }
    }
}