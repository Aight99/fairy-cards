using System;
using System.Collections.Generic;
using BattleSystem.Rules;
using UnityEngine;

namespace BattleSystem
{
    public class BattleSystem : MonoBehaviour
    {
        private List<IRule> _rules;

        public Context Context { get; private set; }

        private void Awake()
        {
            Context = new Context();
            _rules = new List<IRule>()
            {
                new ModifyHealthRule(Context),
                new CreatureDieRule(Context),
                new WinRule(Context),
                new EndTurnRule(Context),
            };
        }

        public void LoadBattle(Battle battle)
        {
            foreach (var ally in battle.Allies)
            {
                Context.Allies.Add(new Creature(ally));
                Debug.Log($"Loaded {Context.Allies[^1].Name} with {Context.Allies[^1].Health} HP");
            }
            foreach (var enemy in battle.Enemies)
            {
                Context.Enemies.Add(new Creature(enemy));
                Debug.Log($"Loaded {Context.Enemies[^1].Name} with {Context.Enemies[^1].Health} HP");
            }
        }

        public void ExecuteCommand(Command command)
        {
            Context.CurrentCommand = command;
            UpdateRules();
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