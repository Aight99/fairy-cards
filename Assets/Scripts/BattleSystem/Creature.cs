using System;
using UnityEngine;

namespace BattleSystem
{
    public class Creature
    {
        public int Health { get; set; }
        public string Name { get; set; }
        
        public Creature()
        {
            Health = 10;
            Name = "lorem ipsum";
        }

        // Нужен ли Null Object?
        // public static Creature NullObject() => new Creature();
        
        // public Creature(GameObject cardPrefab)
        // {
            // var card = cardPrefab.GetComponent<Card>();
            // Health = card.stats.healthPoints;
            // Name = card.character.ToString();
        // }

        public override string ToString()
        {
            return $"「{Name}」: {Health} HP";
        }
    }
}