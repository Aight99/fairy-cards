using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleSystem;

public class GameManager : MonoBehaviour
{

    [SerializeField] Battle battleInfo;
    [SerializeField] BattleController battleController;


    [SerializeField] CardOnTable cardOnTablePrefab;


    [SerializeField] List<CreatureData> creatureData;

    void Start()
    {
        battleController.LoadBattle(battleInfo);

        foreach(var data in battleController.Context.Field.Where(i => i != null)) {
            SpawnCreture(data.CreatureData);
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
