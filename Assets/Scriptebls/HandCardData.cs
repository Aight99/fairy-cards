using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public enum TargetType
{
    Single,
    Area,
    Self
}

public enum EffectType
{
    Damage = 1,
    Heal = -1,
    Status = 0
}

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObject/HandCard", order = 1)]
public class HandCardData : ScriptableObject
{
    [Header("Card Info")]
    public string name;
    public string description;
    public string specialSpeech;
    public Character SpecialUser;
    [Header("General Effect")]
    public int effectPower;
    public EffectType effectType;
    public TargetType targetType;
    public int manaCost;
    [Header("Special Effects")]
    public int cardsGain;
    public int manaGain;

 
    
    public void ApplyEffect()
    {
        if (SpecialUser != Character.None && GameManager.PlayerCard.character == SpecialUser)
        {
            HandleSpecialUsers();
            return;
        }
        var targets = SetTarget();
        Debug.Log(targets.Count);
        foreach (var targetCard in targets)
        {
            targetCard.TakeDamage(effectPower * (int)effectType);
        }
        
        // Add Mana 
        // Add Cards
    }

    private void HandleSpecialUsers()
    {
        var enemyList = GameManager.EnemyCardsList;
        switch (SpecialUser)
        {
            case Character.Kolobok:
                enemyList[Random.Range(0, enemyList.Count)].TakeDamage(effectPower);
                GameManager.PlayerCard.TakeDamage(effectPower * -1);
                break;
            case Character.HanselGretel:
                // Run
                break;
            case Character.SnowQueen:
                enemyList[Random.Range(0, enemyList.Count)].TakeDamage((int) (effectPower * 1.5f));
                GameManager.PlayerCard.TakeDamage((int) (effectPower * .3f));
                break;
            case Character.Koschei:
                // Mana Bonus
                // Card Bonus
                break;
            case Character.Mermaid:
                foreach (var enemy in enemyList)
                {
                    enemy.TakeDamage(effectPower);
                    // skip turn
                }
                break;
            case Character.RedHood:
                // Armor
                // Endurance
                break;
        }
        Debug.Log(specialSpeech);
    }

    private List<Card> SetTarget()
    {
        var targetList = new List<Card>();
        var enemyList = GameManager.EnemyCardsList;
        switch (targetType)
        {
            case TargetType.Single:
                targetList.Add(enemyList[Random.Range(0, enemyList.Count)]);
                break;
            case TargetType.Area:
                targetList.AddRange(enemyList);
                break;
            case TargetType.Self:
                targetList.Add(GameManager.PlayerCard);
                break;
        }
        return targetList;
    }
}
