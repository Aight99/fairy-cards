using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<CardInHand> _draw;
    private List<CardInHand> _discard;

    private void Start()
    {
        if (_draw == null)
            Debug.LogError("Draw deck was null");

        _discard = new List<CardInHand>();
    }

    private void Shuffle() => _draw.OrderBy(card => Random.value);

    public CardInHand DrawCard()
    {
        var newCard = _draw.FirstOrDefault();

        if (newCard == null)
            Debug.LogError("Try to draw null card");

        _draw.RemoveAt(0);

        return newCard;
    }

    public void DiscadCard(CardInHand card)
    {
        _discard.Add(card);
    }


}
