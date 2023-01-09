using System.Collections.Generic;
using BattleSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "HandCardData", menuName = "ScriptableObject/HandCardData", order = 1)]
public class HandCardData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Image;
    public int ManaUsage = 1;
    public List<AdditionalEffect> Effects;
}