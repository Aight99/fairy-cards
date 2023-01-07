using System;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class ModifyHealthRule : IRule
    {
        private readonly Context _context;
        
        public ModifyHealthRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            var command = _context.CurrentCommand;
            foreach (var target in command.Targets)
            {
                if (command.Attack != null)
                {
                    target.Health -= command.Attack.Damage;
                    Debug.Log($"Attack! {target}");
                }
                if (command.Card != null && command.Card.effectType != EffectType.Status)
                {
                    target.Health -= command.Card.effectPower;
                    Debug.Log($"Card! {target}");
                }
            }
        }
    }
}