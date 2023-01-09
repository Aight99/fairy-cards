using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class Creature
    {
        public CreatureData CreatureData { set; get; }
        public int Health { get; set; }
        public string Name { get; set; }
        public int Shields { get; set; }
        public Dictionary<EffectType, int> EffectsDuration { get; private set;}

        public Creature()
        {
            Health = 10;
            Name = "lorem ipsum";
            Shields = 0;
            EffectsDuration = new Dictionary<EffectType, int>();
        }

        public Creature(CreatureData data)
        {
            Health = data.Health;
            Name = data.Name;
            Shields = 0;
            EffectsDuration = new Dictionary<EffectType, int>();  
        }

        public override string ToString() => $"「{Name}」: {Health} HP";
    }
}