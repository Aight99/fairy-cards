using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class Context
    {
        public static event Action TurnEnded;
        public static event Action PlayerLost;
        public static event Action PlayerWon;
        // 0 - 4  PlayerCard
        // 5 - 9  EnemyCard
        public static event Action<int, int> HealthChanged;
        public static event Action<int, int> ShieldsChanged;
        public static event Action<int> CharacterAwakened;
        public static event Action<int> CardReceived;
        public static event Action<int> ManaChanged;
        public static event Action<List<int>> EnemyIntentionsSet;
        public static event Action<int, int> AttackPerformed;
        // Кому, какой, насколько
        public static event Action<int, EffectType, int> EffectChanged;
        // Кого, куда
        public static event Action<int, int> ChangeCardPosition;

        private int _currentMana;

        public Context()
        {
            Field = new Creature[10];
            MaxMana = 5;
            CurrentMana = 3;
            DamageApplyCount = 0;
            IsPlayerTurn = true;
            TurnNumber = 1;
            UsedManaCount = 0;
            CreatureMoveCount = 0;
        }
        
        public bool IsPlayerTurn { get; set; }
        public Creature[] Field { get; private set; }
        public int MaxMana { get; private set; }
        public int CurrentMana { get => _currentMana; set => _currentMana = (int)MathF.Min( Mathf.Max(value, 0), MaxMana); }
        public Command CurrentCommand { get; set; }
        public int DamageApplyCount { get; set; }
        public int UsedManaCount { get; set; }
        public int CreatureMoveCount { get; set; }
        public int TurnNumber { get; set; }
        public int NextEnemyToAttackIndex { get; set; }
        public List<int> EnemyIntentions { get; set; }

        public void EndTurn() => TurnEnded?.Invoke();
        public void ThrowWin() => PlayerWon?.Invoke();
        public void ThrowLose() => PlayerLost?.Invoke();
        public void ChangeHealth(int target, int amount) => HealthChanged?.Invoke(target, amount);
        public void ChangeShields(int target, int amount) => ShieldsChanged?.Invoke(target, amount);
        public void ChangePosition(int from, int to) => ChangeCardPosition?.Invoke(from, to);
        public void AwakeCharacter(int target) => CharacterAwakened?.Invoke(target);
        public void ChangeMana(int currentManaCount) => ManaChanged?.Invoke(currentManaCount);
        public void AddCardsInHand(int amount) => CardReceived?.Invoke(amount);
        public void SetEnemyIntentions(List<int> targets) => EnemyIntentionsSet?.Invoke(targets);
        public void StartHitAnimation(int from, int to) => AttackPerformed?.Invoke(from, to);
        public void SetStatusEffect(int target, EffectType effect, int amount) => EffectChanged?.Invoke(target, effect, amount);
        
        
        // Этот ужас можно отрефкторить красиво, но делать я этого, конечно, не буду
        public void ApplyAdditionalEffects(List<int> targets, int user, List<AdditionalEffect> effects, bool isAfterAttack)
        {
            foreach (var effect in effects)
            {
                if (effect.IsAfterAttack != isAfterAttack)
                {
                    continue;
                }
                if (effect.EffectType == EffectType.ManaGain)
                {
                    CurrentMana += effect.EffectParameter;
                    ChangeMana(CurrentMana);
                }
                else if (effect.EffectType == EffectType.CardGain)
                {
                    AddCardsInHand(effect.EffectParameter);
                }
                else if (effect.EffectType == EffectType.FreeAttack)
                {
                    CurrentCommand.SetTurnEnd(false);
                }
                else if (effect.EffectType == EffectType.Move)
                {
                    CurrentCommand.MoveIndex = CurrentCommand.UserIndex;
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
                        if (Field[user] == null) continue;
                        Field[user].Health += effect.EffectParameter;
                        ChangeHealth(user, Field[user].Health);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            if (Field[target] == null) continue;
                            Field[target].Health += effect.EffectParameter;
                            ChangeHealth(target, Field[target].Health);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.Heal)
                {
                    if (effect.IsSelfTarget)
                    {
                        if (Field[user] == null) continue;
                        Field[user].Health += effect.EffectParameter;
                        ChangeHealth(user, Field[user].Health);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            if (Field[target] == null) continue;
                            Field[target].Health += effect.EffectParameter;
                            ChangeHealth(target, Field[target].Health);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.Shield)
                {
                    if (effect.IsSelfTarget)
                    {
                        if (Field[user] == null) continue;
                        Field[user].Shields += effect.EffectParameter;
                        ChangeShields(user, Field[user].Shields);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            if (Field[target] == null) continue;
                            Field[target].Shields += effect.EffectParameter;
                            ChangeShields(target, Field[target].Shields);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.HalfLife)
                {
                    if (effect.IsSelfTarget)
                    {
                        ApplyDamage(user, user, Field[user].Health / 2);
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            ApplyDamage(user, target, Field[target].Health / 2);
                        }
                    }
                }
                else if (effect.EffectType == EffectType.Vampire)
                {
                    if (effect.IsSelfTarget)
                    {
                        if (Field[user] == null) continue;
                        Field[user].EffectsDuration[effect.EffectType] += effect.EffectParameter;
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            if (Field[target] == null) continue;
                            Field[target].EffectsDuration[effect.EffectType] += effect.EffectParameter;
                        }
                    }
                }
                else if (effect.EffectType == EffectType.Silence)
                {
                    if (effect.IsSelfTarget)
                    {
                        if (Field[user] == null) continue;
                        Field[user].EffectsDuration[effect.EffectType] += effect.EffectParameter;
                    }
                    else
                    {
                        foreach (var target in targets)
                        {
                            if (Field[target] == null) continue;
                            Field[target].EffectsDuration[effect.EffectType] += effect.EffectParameter;
                        }
                    }
                }
            }
        }

        public void ApplyDamage(int userIndex, int targetIndex, int damage)
        {
            var user = Field[userIndex];
            var target = Field[targetIndex];
            if (target == null)
            {
                return;
            }
            if (damage <= 0)
            {
                return;
            }
            if (target.Shields != 0)
            {
                var shieldsDamage = (target.Shields >= damage) ? damage : target.Shields;
                target.Shields -= shieldsDamage;
                damage -= shieldsDamage;
                ChangeShields(targetIndex, target.Shields);
            }
            if (damage != 0)
            {
                target.Health -= damage;
                if (user != null && user.EffectsDuration.ContainsKey(EffectType.Vampire))
                {
                    user.Health += damage;
                    ChangeHealth(userIndex, user.Health);
                }
                ChangeHealth(targetIndex, target.Health);
            }
            if (IsPlayerTurn)
            {
                DamageApplyCount++;
            }
        }
    }
}