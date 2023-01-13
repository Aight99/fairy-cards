using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleSystem;

public class GameManager : MonoBehaviour
{

    [SerializeField] Battle battleInfo;
    [SerializeField] BattleController battleController;


    [SerializeField] TableConroller tableConroller;

    [SerializeField] CardOnTable cardOnTablePrefab;
    [SerializeField] List<CreatureData> creatureData;

    private void Awake()
    {
        // battleController.LoadBattle(battleInfo);
        battleController.LoadRandomBattle();
        
        //foreach(var data in battleController.Context.Field.Where(i => i != null)) {
        //    SpawnCreture(data.CreatureData);
        //};

        foreach (var creatureData in battleController.Context.Field[0..5].Where(i => i != null).Select(i => i.CreatureData))
        {
            var newCard = Instantiate(cardOnTablePrefab);
            newCard.LoadFromCreatureData(creatureData);

            tableConroller.playerCards.Add(newCard);

            tableConroller.palyerCardsDict[creatureData] = newCard;
        };



        foreach (var creatureData in battleController.Context.Field[5..10].Where(i => i != null).Select(i => i.CreatureData))  
        {
            var newCard = Instantiate(cardOnTablePrefab);
            newCard.LoadFromCreatureData(creatureData);

            tableConroller.enemyCards.Add(newCard);

            tableConroller.enemyCardsDict[creatureData] = newCard;
        };
    }




}
