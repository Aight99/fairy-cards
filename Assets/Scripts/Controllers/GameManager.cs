using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] Battle battleInfo;
    [SerializeField] BattleSystem.BattleSystem battleSystem;
    [SerializeField] TableConroller tableConroller;


    [SerializeField] CardOnTable cardOnTablePrefab;


    [SerializeField] List<CreatureData> creatureData;

    void Start()
    {
        battleSystem.LoadBattle(battleInfo);



         
        foreach (var creatureData in battleSystem.Context.Field[0..4].Where(i => i != null).Select(i => i.creatureData))  
        {
            var newCard = Instantiate(cardOnTablePrefab);
            newCard.LoadFromCreatureData(creatureData);

            tableConroller.playerCards.Add(newCard);

            tableConroller.palyerCardsDict[creatureData] = newCard;
        };

        foreach (var creatureData in battleSystem.Context.Field[5..9].Where(i => i != null).Select(i => i.creatureData))  
        {
            var newCard = Instantiate(cardOnTablePrefab);
            newCard.LoadFromCreatureData(creatureData);

            tableConroller.enemyCards.Add(newCard);

            tableConroller.enemyCardsDict[creatureData] = newCard;
        };


    }


}
