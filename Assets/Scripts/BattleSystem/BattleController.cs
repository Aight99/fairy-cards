﻿using System;
using System.Collections.Generic;
using BattleSystem.Rules;
using UnityEngine;

namespace BattleSystem
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance;
        
        private List<IRule> _rules;

        public Context Context { get; private set; }

        private void Awake()
        {
            Instance = (Instance == null) ? this : Instance;
            
            Context = new Context();
            _rules = new List<IRule>()
            {
                new AttackRule(Context),
                new CardUsageRule(Context),
                new CreatureDieRule(Context),
                new MoveRule(Context),
                new AwakeningRule(Context),
                new WinRule(Context),
                new LoseRule(Context),
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
                Context.Field[allySpawnOrder[spawnIndex]] = new Creature(ally);
                spawnIndex++;   
            }

            spawnIndex = 0;
            foreach (var enemy in battle.Enemies)
            {
                Context.Field[enemySpawnOrder[spawnIndex]] = new Creature(enemy);
                spawnIndex++;
            }

            PrintCurrentTable();
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
            PrintCurrentTable();
        }

        private void PrintCurrentTable()
        {
            var side = "\t";
            for (int i = 5; i < 10; i++)
            {
                if (Context.Field[i] != null)
                {
                    side += $"<color=#f54248>[{Context.Field[i].Name}: {Context.Field[i].Health}hp]</color>\t";
                }
                else
                {
                    side += "[]\t";
                }
            }
            side += "\n\t\t";
            for (int i = 0; i < 5; i++)
            {
                if (Context.Field[i] != null)
                {
                    side += $"<color=#f59e42>[{Context.Field[i].Name}: {Context.Field[i].Health}hp]</color>\t";
                }
                else
                {
                    side += "[]\t";
                }
            }
            Debug.Log(side);
        }
    }
}