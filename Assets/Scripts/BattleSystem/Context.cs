using System;
using System.Collections.Generic;

namespace BattleSystem
{
    public class Context
    {
        public static event Action PlayerTurnEnded;
        public static event Action PlayerLost;
        public static event Action PlayerWon;
        // 0 - 4  PlayerCard
        // 5 - 9  EnemyCard
        public static event Action<int, int> HealthChanged;
        public static event Action<int> CharacterAwakened;
        public static event Action<int> CardReceived;
        public static event Action<int> ManaChanged;
        // Кому, какой, насколько
        public static event Action<int, EffectType, int> EffectChanged;
        // Кого, куда
        public static event Action<int, int> ChangeCardPosition;

        public Context()
        {
            Field = new Creature[10];
            MaxMana = 0;
            CurrentMana = 0;
        }

        public Creature[] Field { get; private set; }
        public int MaxMana { get; private set; }
        public int CurrentMana { get; private set; }
        public Command CurrentCommand { get; set; }

        public void EndTurn() => PlayerTurnEnded?.Invoke();
        public void ThrowWin() => PlayerWon?.Invoke();
        public void ThrowLose() => PlayerLost?.Invoke();
        public void ChangeHealth(int target, int amount) => HealthChanged?.Invoke(target, amount);
        public void ChangePosition(int from, int to) => ChangeCardPosition?.Invoke(from, to);
        public void AwakeCharacter(int target) => CharacterAwakened?.Invoke(target);
        public void ChangeMana(int currentManaCount) => ManaChanged?.Invoke(currentManaCount);
        public void AddCardsInHand(int amount) => CardReceived?.Invoke(amount);
        public void SetStatusEffect(int target, EffectType effect, int amount) => EffectChanged?.Invoke(target, effect, amount);
    }
}