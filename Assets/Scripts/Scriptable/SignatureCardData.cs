using System.Collections.Generic;
using BattleSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "SignatureCardData", menuName = "ScriptableObject/SignatureCardData", order = 1)]
public class SignatureCardData : HandCardData
{
    public List<AdditionalEffect> SpecialEffects;
}