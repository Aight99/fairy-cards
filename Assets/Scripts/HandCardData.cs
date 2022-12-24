using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObject/HandCard", order = 1)]
public class HandCardData : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public int manaValue;
}
