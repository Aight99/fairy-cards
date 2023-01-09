using System.Collections.Generic;
using BattleSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObject/AttackData", order = 1)]
public class AttackData : ScriptableObject
{
    public string Name;
    public string Description;
    public int Damage = 5;
    public int ManaUsage = 2;
    public AttackType AttackType;
    public List<AdditionalEffect> AdditionalEffects;
}