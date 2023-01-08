using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle", menuName = "ScriptableObject/Battle", order = 1)]
public class Battle : ScriptableObject
{
    public List<CreatureData> Enemies;
    public List<CreatureData> Allies;
    // List of special rules
    // List of special cards
}