using System;
using System.Collections.Generic;
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

    public static States CurrentState = States.Idle;
    public static Camera mainCamera;


    public Transform AllCardsSpot;
    public Transform[] EnemyCardsSpots;
    public Card[] EnemyCardsPrefab;

    private List<Card> EnemyCardsList;


    public Transform PlayerCardSpot;
    public Card PlayerCardPrefab;
    public Button[] PlayerAttacksButton;
    public static Card PlayerCard { get => Instanse._playerCard; set => Instanse._playerCard = value; }
    private Card _playerCard;
    public static Attack CurrentSelectedAttack = null;

    private static GameManager Instanse;

    void Start()
    {
        EnemyCardsList = new List<Card>();

        Instanse = this;
        mainCamera = Camera.main;

        foreach (var spot in EnemyCardsSpots)
        {
            var randomEnmeyCard = EnemyCardsPrefab[UnityEngine.Random.Range(0, EnemyCardsPrefab.Length)];
            randomEnmeyCard = Instantiate(randomEnmeyCard, spot.position, Quaternion.identity);
            randomEnmeyCard.transform.SetParent(AllCardsSpot);
            EnemyCardsList.Add(randomEnmeyCard);
        }

        _playerCard = Instantiate(PlayerCardPrefab, PlayerCardSpot.position, Quaternion.identity);
        _playerCard.transform.SetParent(AllCardsSpot);


        for (int i = 0; i < _playerCard.Attacks.Length; i++)
        {
            var attackInfo = _playerCard.Attacks[i];
            PlayerAttacksButton[i].GetComponent<Image>().sprite = attackInfo.spriteImage;

        }

    }

    public static void ChangeState(States newState)
    {

        switch (newState)
        {
            case States.Idle:
                CurrentSelectedAttack = null;

                break;
            case States.PlayerAttack:
                break;
            case States.EnemyAttack:
                break;
            default:
                break;
        }

        CurrentState = newState;

    }

    public void ChangeCurrentAttack(int index)
    {
        CurrentSelectedAttack = _playerCard.Attacks[index];

        ChangeState(States.PlayerAttack);

    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case States.Idle:
                break;
            case States.PlayerAttack:
                break;
            case States.EnemyAttack:
                EnemyCardsList[Random.Range(0, EnemyCardsList.Count)].Attack();
                break;
            case States.Waiting:
                break;
            default:
                break;
        }



    }
}
