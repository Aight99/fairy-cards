using System;
using UnityEngine;

namespace BattleSystem
{
    public class TestUtilsBS : MonoBehaviour
    {
        [SerializeField] private BattleSystem battleSystem;
        [SerializeField] private Battle battleInfo;
        [SerializeField] private HandCardData card;
        [SerializeField] private Attack attack;

        private Context _context;

        private void Awake()
        {
            _context = battleSystem.Context;
            battleSystem.LoadBattle(battleInfo);
            Context.PlayerTurnEnded += () => Debug.Log("Player turn ended");
            Context.PlayerWon += () => Debug.Log("VICTORY!!!");
        }
        
        public void EndTurn() => battleSystem.ExecuteCommand(
            Command
                .CreateEmpty()
                .SetTurnEnd()
        );

        public void PlayCard() => battleSystem.ExecuteCommand(
            Command
                .CreateEmpty()
                .AddTarget(_context.Enemies[^1])
                .SetCardToPlay(card)
        );

        public void Attack() => battleSystem.ExecuteCommand(
            Command
                .CreateEmpty()
                .SetAttack(attack)
                .SetUser(_context.Allies[^1])
                .AddTarget(_context.Enemies[^1])
                .SetTurnEnd()
        );
    }
}