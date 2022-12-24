using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObject/HandCard", order = 1)]
public class HandCardData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string description;
}
