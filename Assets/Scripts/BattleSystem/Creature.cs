﻿using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class Creature
    {
        public readonly bool IsPlayer;
        public CreatureData CreatureData { set; get; }
        private int _health;
        public int Health
        {
            get => _health;
            set => _health = (value <= 0 && EffectsDuration.ContainsKey(EffectType.Endurance)) ? 1 : value;
        }
        public string Name { get; set; }
        public int Shields { get; set; }
        public bool IsAwakened { get; set; }
        public Dictionary<EffectType, int> EffectsDuration { get; private set;}
        public int AttackCount { get; set; }

        public Creature(CreatureData data, bool isPlayer)
        {
            IsPlayer = isPlayer;
            Health = data.Health;
            Name = data.Name;
            Shields = 0;
            IsAwakened = false;
            CreatureData = data;
            EffectsDuration = new Dictionary<EffectType, int>();  
            AttackCount = 0;
        }

        public AttackData CurrentAttack => (IsAwakened) ? CreatureData.AwakenedAttack : CreatureData.NormalAttack;
        
        public override string ToString() => (Shields > 0) ? $"「{Name}」: {Health}+{Shields} HP" : $"「{Name}」: {Health} HP";
    }
}