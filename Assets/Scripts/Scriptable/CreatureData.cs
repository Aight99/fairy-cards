using UnityEngine;

[CreateAssetMenu(fileName = "CreatureData", menuName = "ScriptableObject/CreatureData", order = 1)]
public class CreatureData : ScriptableObject
{
    public int Health;
    public string Name;
    public string Description;
    public Sprite Sprite;
    public AttackData NormalAttack;
    public AttackData AwakenedAttack;
    public SignatureCardData SignatureCard;
}