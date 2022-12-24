using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum States
{

    Idle,
    PlayerAttack,
    EnemyAttack

}

public enum CardTypes
{
    EnemyCard ,
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
    private Card PlayerCard;
    public static Attack CurrentSelectedAttack = null;

    private GameManager Instanse;

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

        PlayerCard = Instantiate(PlayerCardPrefab, PlayerCardSpot.position, Quaternion.identity);
        PlayerCard.transform.SetParent(AllCardsSpot);


        for (int i = 0; i < PlayerCard.Attacks.Length; i++)
        {
            var attackInfo = PlayerCard.Attacks[i];
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
        CurrentSelectedAttack = PlayerCard.Attacks[index];

        ChangeState(States.PlayerAttack);

    }

    // Update is called once per frame
    void Update()
    {



    }
}
