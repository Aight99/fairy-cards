using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle", menuName = "ScriptableObject/Battle", order = 1)]
public class Battle : ScriptableObject
{
    public List<GameObject> Enemies;
    public List<GameObject> Allies;
    // List of special rules
    // List of special cards
}