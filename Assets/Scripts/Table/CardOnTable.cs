using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardOnTable : Card
{

    [SerializeField] private TextMeshPro cardName;
    [SerializeField] private TextMeshPro cardHealth;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void LoadFromCreatureData(CreatureData creatureData)
    {
        Debug.Log(cardName);
        Debug.Log(creatureData);
        cardName.text = creatureData.name;
        cardHealth.text = creatureData.Health.ToString();
        spriteRenderer.sprite = creatureData.Sprite;
        
    }

    public void SetHealth(int health)
    {
        cardHealth.text = health.ToString();    
    }


}
