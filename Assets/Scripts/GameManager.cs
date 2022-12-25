using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public enum States
{

    Idle,
    PlayerAttack,
    EnemyAttack,
    Waiting
}

public enum CardTypes
{
    EnemyCard,
    PlayerCard,
    HandCard
}

public class GameManager : MonoBehaviour
{


    public static EventHandler onLose;
    public static EventHandler onWin;
    public static EventHandler onTurnEnd;

    public static States CurrentState = States.Idle;
    public static Camera mainCamera;


    public Transform AllCardsSpot;
    public Transform[] EnemyCardsSpots;
    public Card[] EnemyCardsPrefab;

    public static List<Card> EnemyCardsList;

    public Transform PlayerCardSpot;
    public Card PlayerCardPrefab;
    
    
    public static Card PlayerCard;

    
    void Start()
    {
        EnemyCardsList = new List<Card>();

        mainCamera = Camera.main;

        foreach (var spot in EnemyCardsSpots)
        {
            var randomEnmeyCard = EnemyCardsPrefab[UnityEngine.Random.Range(0, EnemyCardsPrefab.Length)];
            randomEnmeyCard = Instantiate(randomEnmeyCard, spot.position, Quaternion.identity);
            randomEnmeyCard.transform.SetParent(AllCardsSpot);
            EnemyCardsList.Add(randomEnmeyCard);

            randomEnmeyCard.onCursorEnter += (object sender, EventArgs args) =>
            {
                (sender as Card)._gardInfo.SetActive(true);
            };
            
            randomEnmeyCard.onCursorLeft += (object sender, EventArgs args) =>
            {
                (sender as Card)._gardInfo.SetActive(false);
            };


        }

        PlayerCard = Instantiate(PlayerCardPrefab, PlayerCardSpot.position, Quaternion.identity);
        PlayerCard.transform.SetParent(AllCardsSpot);


        PlayerCard.onCursorEnter += (object sender, EventArgs args) =>
        {
            (sender as Card)._gardInfo.SetActive(true);
        };

        PlayerCard.onCursorLeft += (object sender, EventArgs args) =>
        {
            (sender as Card)._gardInfo.SetActive(false);
        };

        PlayerCard.onDead += (object sender, EventArgs args) =>
        {
            onLose?.Invoke(null, null);
        };

        PlayerManager.Init();
        EnemyController.Init();
        ManaManager.Init();
    }


    private void Update()
    {

    }

    public void EndPlayerTurn()
    {
        ChangeState(States.EnemyAttack);
    }

    public static void DeleteEnemy(Card enemy)
    {
        EnemyCardsList.Remove(enemy);

        if (EnemyCardsList.Count == 0)
            onWin.Invoke(null, null) ;
    }

    public static void ChangeState(States newState)
    {
        switch (newState)
        {
            case States.Idle:
                onTurnEnd.Invoke(null, null) ;
                break;
            case States.PlayerAttack:
                break;
            case States.EnemyAttack:
                break;
            case States.Waiting:
                break;
            default:
                break;
        }

        CurrentState = newState;

    }

}
