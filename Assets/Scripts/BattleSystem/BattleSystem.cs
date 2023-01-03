using System;
using System.Collections.Generic;
using BattleSystem.Commands;
using BattleSystem.Rules;
using UnityEngine;

namespace BattleSystem
{
    public class BattleSystem : MonoBehaviour
    {
        private List<IRule> _rules;
        private Context _context;

        private void Awake()
        {
            _context = new Context();
            _rules = new List<IRule>()
            {
                // New EndTurnRule(_context)
                // new ModifyHealthRule(_context)
                // new UnitDiRule(_context)
            };
        }

        public void ExecuteCommand(ICommand command)
        {
            
        }

        private void UpdateRules()
        {
            foreach (var rule in _rules)
            {
                rule.ApplyRule();
            }
        }
    }
}