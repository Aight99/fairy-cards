using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : Updateble
{
    public List<CardInHand> cardsInHand = new();

    [SerializeField] private Vector3 ScaleShift;
    [SerializeField] private Vector3 PositionShift;

    [SerializeField] private Transform center;
    [SerializeField] private float gap;

    [SerializeField] private Deck deck;

    [SerializeField] private Updateble TableController;
    [SerializeField] private EmptySpaceOnTable TableTrigger;

    private Updateble currenUpdatable;


    private Vector2 cardSize;
    

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            cardsInHand.Add(deck.DrawCard());
        }

        cardSize = cardsInHand[0].getSize();

        TableTrigger.onClick.AddListener((trigger) => currenUpdatable = TableController);

        RebaseCardPosition();
    }

    public override void _Start()
    {
        Debug.Log("Hand select");

        currenUpdatable = this;

        //center.transform.localScale += ScaleShift;
        center.transform.localPosition += PositionShift;
        RebaseCardPosition();
    }

    public override void _End()
    {
       
        //center.transform.localScale -= ScaleShift;
        center.transform.localPosition -= PositionShift;
        RebaseCardPosition();
    }

    private void DrawCardFromDeck()
    {
        cardsInHand.Add(deck.DrawCard());

        RebaseCardPosition();
    }

    private void RebaseCardPosition()
    {

        float totalHandLength = cardsInHand.Count * cardSize.x + (cardsInHand.Count - 1) * gap;

        Vector3 startPoint = center.position - new Vector3(totalHandLength / 2 - cardSize.x / 2, 0);

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].transform.position = startPoint + new Vector3(i * (cardSize.x + gap), 0);
        }
    }

    public override void _Update()
    {

        foreach (var card in cardsInHand)
        {
            card._Update();
        };

        TableTrigger._Update();
    }

    public override Updateble GetNextUpdateble() => currenUpdatable;

}
