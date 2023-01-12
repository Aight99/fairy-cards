using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleSystem;

public class TableConroller : Updateble
{
    [SerializeField] Transform enemyCardBase;
    [SerializeField] Transform playerCardBase;


    // ������ ����� �� ������� ����� ��������� �����
    [SerializeField] Transform[] enemyPoints = new Transform[5];
    [SerializeField] Transform[] playerPoints = new Transform[5];

    // ��� ���� ������ ���� ������� ������ ���� � ���������� � � ������ �������������
    // �� ����� ��������� � ������� ����� ������ �� �����
    public List<CardOnTable> enemyCards = new List<CardOnTable>();
    public List<CardOnTable> playerCards = new List<CardOnTable>();

    public Dictionary<CreatureData , CardOnTable> enemyCardsDict = new Dictionary<CreatureData , CardOnTable>();
    public Dictionary<CreatureData , CardOnTable> palyerCardsDict = new Dictionary<CreatureData , CardOnTable>();


    // � ������-�� �� ���� �������� ��������� ������ � ������ ������, �� ������ ������� ������� 
    [SerializeField] EmptySpaceOnTable[] emptySpaces = new EmptySpaceOnTable[10];
    //[SerializeField] EmptySpaceOnTable emptySpacePrefab;

    // ��� ��� ����� �� ������� ���������� � ������, �� ����� ����� ��� �����(�� ������ ����� ���� �����) ��� ������� �����
    private Card[] enemyCardsOnTable = new Card[5];
    private Card[] playerCardsOnTable = new Card[5];

    private Card SelectedPlayerCard = null;


    [SerializeField] private Updateble HandController;
    [SerializeField] private Updateble EnemyController;
    [SerializeField] private EmptySpaceOnTable HandTrigger;
    private Updateble currentUpdateble;


    [SerializeField] private Battle battleInfo;
    [SerializeField] private BattleController battleController;


    private void Start()
    {
        battleController.LoadBattle(battleInfo);

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


    private void RebaseCardOrder()
    {
        var field = battleSystem.Context.Field;

        int usedEmptySpaces = 0;

        for (int i = 0; i < 5; i++) {
            var creatureData = field[i].creatureData;
            
            playerCardsOnTable[i] = creatureData ? palyerCardsDict[creatureData] : emptySpaces[usedEmptySpaces++];
        }

        for (int i = 5; i < 10; i++) {
            var creatureData = field[i].creatureData;
            
            enemyCardsOnTable[i] = creatureData ? enemyCardsDict[creatureData] : emptySpaces[usedEmptySpaces++];
        }
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

        if (!checkEnemyEnableToAttack(selectedPlayerCardIndex , selectedEnemyCardIndex)) return;

        Debug.Log($"Player Card {selectedPlayerCardIndex} attack enemy card {selectedEnemyCardIndex + 5}");

        
        battleController.ExecuteCommand(Command.AttackCommand(selectedPlayerCardIndex, selectedEnemyCardIndex + 5));


        RebaseCardOrder();
        RebaseCardPosition();
        SelectedPlayerCard = null;

        currentUpdateble = EnemyController;
    }

    // ������ ����� �� ����� !!!
    private bool checkEnemyEnableToAttack(int playerInex , int enemyCardIndex) => true;


    private void EmptyPlayerSpaceClick(Card card)
    {

        if (SelectedPlayerCard == null) return;

        int selectedPlayerCardIndex = Array.IndexOf(playerCardsOnTable, SelectedPlayerCard);
        int selectedeEmptySpaceIndex = Array.IndexOf(playerCardsOnTable, card);


        if (!checkEmptySpaceEnableToMove(selectedPlayerCardIndex ,  selectedeEmptySpaceIndex)) return;

        battleController.ExecuteCommand(Command.MoveCommand(selectedPlayerCardIndex, selectedeEmptySpaceIndex));

        //var temp = playerCardsOnTable[selectedPlayerCardIndex];
        //playerCardsOnTable[selectedPlayerCardIndex] = playerCardsOnTable[selectedeEmptySpaceIndex];
        //playerCardsOnTable[selectedeEmptySpaceIndex] = temp;

        SelectedPlayerCard = null;

        RebaseCardOrder();  
        RebaseCardPosition();

        currentUpdateble = EnemyController;
    }

    // ������ ����� �� ����� !!!
    private bool checkEmptySpaceEnableToMove(int playerInex , int emptySpaceInde) => true;


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
