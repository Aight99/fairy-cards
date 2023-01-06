using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Updateble
{
    public List<CardInHand> cardsInHand = new();

    [SerializeField] Deck deck;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
            cardsInHand.Add(deck.DrawCard());
    }

    public override void _Update()
    {

        foreach (var card in cardsInHand)
        {
            card._Update();
        };

    }
}
