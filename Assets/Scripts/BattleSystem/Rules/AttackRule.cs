using System;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class AttackRule : IRule
    {
        private readonly Context _context;
        
        public AttackRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            var command = _context.CurrentCommand;
            var target = _context.Field[command.TargetIndex];
            // if (command.Attack != null)
            // {
            //     target.Health -= command.Attack.Damage;
            //     Debug.Log($"Attack! {target}");
            // }
            // if (command.Card != null && command.Card.effectType != EffectType.Status)
            // {
            //     target.Health -= command.Card.effectPower;
            //     Debug.Log($"Card! {target}");
            // }
        }
    }
}