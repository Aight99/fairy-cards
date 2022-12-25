using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObject/HandCard", order = 1)]
public class HandCardData : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public int manaValue;
}
