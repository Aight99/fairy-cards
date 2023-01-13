using System.Collections.Generic;
using BattleSystem;
using UnityEngine;

public enum CardTargetType
{
    None,
    Single,
    Side
}

[CreateAssetMenu(fileName = "HandCardData", menuName = "ScriptableObject/HandCardData", order = 1)]
public class HandCardData : ScriptableObject
{
    public string Name;
    public string EffectDescription; // Нормальное описанеи эфекта
    public string Description;
    public Sprite Image;
    public CardTargetType TargetType;
    public int ManaUsage = 1;
    public List<AdditionalEffect> Effects;

    public bool IsTargeting => (TargetType != CardTargetType.None);
}