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
            int[] allySpawnOrder = {2, 1, 3, 0, 4};
            int[] enemySpawnOrder = {7, 6, 8, 5, 9};
            var spawnIndex = 0;
            foreach (var ally in battle.Allies)
            {
                Context.Field[allySpawnOrder[spawnIndex]] = new Creature();
                Debug.Log($"Loaded ally in {allySpawnOrder[spawnIndex]} place");
            }
            spawnIndex = 0;
            foreach (var enemy in battle.Enemies)
            {
                Context.Field[enemySpawnOrder[spawnIndex]] = new Creature();
                Debug.Log($"Loaded enemy in {enemySpawnOrder[spawnIndex]} place");
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