using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableConroller : Updateble
{

    [SerializeField] List<Transform> enemyPoints;
    [SerializeField] List<Transform> playerPoints;


    public List<CardOnTable> enemyCards = new List<CardOnTable>();
    public List<CardOnTable> playerCards = new List<CardOnTable>();


    private void Start()
    {
        if (enemyCards.Count > 5)
            Debug.LogError("Enemy cards list more than 5");
        
        if (playerCards.Count > 5)
            Debug.LogError("Player cards list more than 5");

        int shift = (5 - enemyCards.Count) / 2;

        for (int i = 0; i < enemyCards.Count; i++)
            enemyCards[i].transform.position = enemyPoints[shift + i].position;

        shift = (5 - playerCards.Count) / 2;

        for (int i = 0; i < playerCards.Count; i++)
            playerCards[i].transform.position = playerPoints[shift + i].position;

    }

    public override void _Update()
    {
       
       


    }
}
