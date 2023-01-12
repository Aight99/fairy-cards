using System;
using System.Collections.Generic;
using BattleSystem.Rules;
using UnityEngine;

namespace BattleSystem
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance;

        private List<IRule> _rules;
        private List<IRule> _startTurnRules;

        public Context Context { get; private set; }

        private void Awake()
        {
            Instance = (Instance == null) ? this : Instance;
            Context = new Context();
            _startTurnRules = new List<IRule>()
            {
                new TurnGainRule(Context),
                new EffectsUpdateRule(Context),
            };
            _rules = new List<IRule>()
            {
                new ManaCostRule(Context),
                new AttackRule(Context),
                new CardUsageRule(Context),
                new EnemyAttackRule(Context),
                new CreatureDieRule(Context),
                new MoveRule(Context),
                new AwakeningRule(Context),
                new EnemyTargetingRule(Context),
                new WinRule(Context),
                new LoseRule(Context),
                new EndTurnRule(Context),
            };
            Context.TurnEnded += ChangeTurn;
        }

        private void ChangeTurn()
        {
            foreach (var rule in _startTurnRules)
            {
                rule.ApplyRule();
            }
            if (!Context.IsPlayerTurn)
            {
                Debug.Log($"<color=yellow>ENEMY TURN!</color>");
                ExecuteCommand(Command
                    .CreateEmpty()
                    .SetTarget(Context.EnemyIntentions[0])
                    .SetUser(Context.NextEnemyToAttackIndex)
                    .SetTurnEnd()
                    );
            }
            else
            {
                Debug.Log($"<color=yellow>PLAYER TURN!</color>");
            }
        }

        private void Start()
        {
            Context.ChangeMana(Context.CurrentMana);
            // Костыль, не менять порядок
            // EnemyTargetingRule, чтобы игрок сразу видел, куда прут
            _rules[7].ApplyRule();
        }

        public void LoadBattle(Battle battle)
        {
            int[] allySpawnOrder = {2, 1, 3, 0, 4};
            int[] enemySpawnOrder = {7, 6, 8, 5, 9};
            var spawnIndex = 0;

            foreach (var ally in battle.Allies)
            {
                Context.Field[allySpawnOrder[spawnIndex]] = new Creature(ally, true);
                spawnIndex++;
            }

            spawnIndex = 0;
            foreach (var enemy in battle.Enemies)
            {
                Context.Field[enemySpawnOrder[spawnIndex]] = new Creature(enemy, false);
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
                    side += (Context.Field[i].Shields == 0)
                        ? $"<color=#f54248>[{Context.Field[i].Name}: {Context.Field[i].Health}hp]</color>\t"
                        : $"<color=#f54248>[{Context.Field[i].Name}: {Context.Field[i].Health}({Context.Field[i].Shields})hp]</color>\t";
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
                    side += (Context.Field[i].Shields == 0)
                        ? $"<color=#f59e42>[{Context.Field[i].Name}: {Context.Field[i].Health}hp]</color>\t"
                        : $"<color=#f59e42>[{Context.Field[i].Name}: {Context.Field[i].Health}({Context.Field[i].Shields})hp]</color>\t";
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