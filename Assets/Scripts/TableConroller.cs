using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableConroller : Updateble
{
    [SerializeField] Transform enemyCardBase;
    [SerializeField] Transform playerCardBase;


    // Список точек на которых могут находится карты
    [SerializeField] Transform[] enemyPoints = new Transform[5];
    [SerializeField] Transform[] playerPoints = new Transform[5];

    // Это типо список карт которые должны быть у противника и у игрока соответсвенно
    // Не имеет отношение к позиции карты картам на столе
    public List<CardOnTable> enemyCards = new List<CardOnTable>();
    public List<CardOnTable> playerCards = new List<CardOnTable>();

    // Я почему-то не могу добавить слушателя ивенту у нового префаб, хз почему поэтому костыли 
    [SerializeField] EmptySpaceOnTable[] emptySpaces = new EmptySpaceOnTable[10];
    //[SerializeField] EmptySpaceOnTable emptySpacePrefab;

    // Это все карты на стороне противника и игрока, не важно путое это место(да пустое место тоже карта) или игровая карта
    private Card[] enemyCardsOnTable = new Card[5];
    private Card[] playerCardsOnTable = new Card[5];

    private Card SelectedPlayerCard = null;


    [SerializeField] private Updateble HandController;
    [SerializeField] private Updateble EnemyController;
    [SerializeField] private EmptySpaceOnTable HandTrigger;
    private Updateble currentUpdateble;



    private void Start()
    {
        currentUpdateble = this;

        if (enemyCards.Count > 5)
            Debug.LogError("Enemy cards list more than 5");

        if (playerCards.Count > 5)
            Debug.LogError("Player cards list more than 5");


        HandTrigger.onClick.AddListener((trigger) => currentUpdateble = HandController );

        int useEmptySpaces = 0;

        #region Spawn Enemy Card

        int shift = (5 - enemyCards.Count) / 2;

        for (int i = 0; i < shift; i++)
        {
            //enemyCardsOnTable[i] = Instantiate(emptySpacePrefab, enemyCardBase);
            enemyCardsOnTable[i] = playerCardsOnTable[i] = emptySpaces[useEmptySpaces++];

        }

        for (int i = 0; i < enemyCards.Count; i++)
        {
            enemyCardsOnTable[i + shift] = enemyCards[i];
            enemyCardsOnTable[i + shift].onClick.AddListener(EnemyCardClick);
        }

        for (int i = enemyCards.Count + shift; i < 5; i++)
        {
            //enemyCardsOnTable[i] = Instantiate(emptySpacePrefab, enemyCardBase);
            enemyCardsOnTable[i] = emptySpaces[useEmptySpaces++];

        }

        #endregion

        #region Spawn Player Card
        shift = (5 - playerCards.Count) / 2;

        for (int i = 0; i < shift; i++)
        {
            //playerCardsOnTable[i] = Instantiate(emptySpacePrefab, playerCardBase);
            playerCardsOnTable[i] = emptySpaces[useEmptySpaces++];
            playerCardsOnTable[i].onClick.AddListener(EmptyPlayerSpaceClick);
        }

        for (int i = 0; i < playerCards.Count; i++)
        {
            playerCardsOnTable[i + shift] = playerCards[i];
            playerCardsOnTable[i + shift].onClick.AddListener(PlayerCardClick);
        }

        for (int i = playerCards.Count + shift; i < 5; i++)
        {
            //playerCardsOnTable[i] = Instantiate(emptySpacePrefab, playerCardBase);
            playerCardsOnTable[i] = emptySpaces[useEmptySpaces++];
            playerCardsOnTable[i].onClick.AddListener(EmptyPlayerSpaceClick);
        }

        #endregion

        RebaseCardPosition();
    }

    public override void _Start()
    {

        Debug.Log("Table select");

        currentUpdateble = this;
    }

    private void RebaseCardPosition()
    {
        for (int i = 0; i < 5; i++)
        {
            enemyCardsOnTable[i].transform.position = enemyPoints[i].position;
            playerCardsOnTable[i].transform.position = playerPoints[i].position;
        }
    }

    private void PlayerCardClick(Card card)
    {
        SelectedPlayerCard = card;
    }

    private void EnemyCardClick(Card card)
    {
        if (SelectedPlayerCard == null) return;

        int selectedPlayerCardIndex = Array.IndexOf(playerCardsOnTable, SelectedPlayerCard);
        int selectedEnemyCardIndex = Array.IndexOf(enemyCardsOnTable, card);

        if (!checkEnemyEnableToAttack(selectedEnemyCardIndex)) return;

        Debug.Log($"Player Card {selectedPlayerCardIndex} attack enemy card {selectedEnemyCardIndex}");

        SelectedPlayerCard = null;

        currentUpdateble = EnemyController;
    }

    // Индекс карты на столе !!!
    private bool checkEnemyEnableToAttack(int enemyCardIndex) => true;


    private void EmptyPlayerSpaceClick(Card card)
    {

        if (SelectedPlayerCard == null) return;

        int selectedPlayerCardIndex = Array.IndexOf(playerCardsOnTable, SelectedPlayerCard);
        int selectedeEmptySpaceIndex = Array.IndexOf(playerCardsOnTable, card);


        if (!checkEmptySpaceEnableToMove(selectedeEmptySpaceIndex)) return;

        var temp = playerCardsOnTable[selectedPlayerCardIndex];
        playerCardsOnTable[selectedPlayerCardIndex] = playerCardsOnTable[selectedeEmptySpaceIndex];
        playerCardsOnTable[selectedeEmptySpaceIndex] = temp;

        SelectedPlayerCard = null;

        RebaseCardPosition();

        currentUpdateble = EnemyController;
    }

    // Индекс места на столе !!!
    private bool checkEmptySpaceEnableToMove(int emptySpaceInde) => true;


    public override void _Update()
    {

        for (int i = 0; i < 5; i++)
        {
            enemyCardsOnTable[i]._Update();
            playerCardsOnTable[i]._Update();
        }

        HandTrigger._Update();

    }

    public override Updateble GetNextUpdateble() => currentUpdateble;

}
