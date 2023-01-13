using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<CardInHand> _draw;
    [SerializeField] private List<CardInHand> _discard;

    [SerializeField] private Transform _drawPoint;
    [SerializeField] private Transform _discardPoint;

    [SerializeField] private CardInHandDescription handDescription;

    private void Start()
    {
        if (_draw == null)
            Debug.LogError("Draw deck was null");

        _discard = new List<CardInHand>();

        foreach (var card in _draw)
        {
            card.onPlay.AddListener(DiscardCard);
            card.onCursorEnter.AddListener((card) => handDescription.DisplayCardDesctiption((card as CardInHand).data));
        }
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

    public void RefillDeck()
    {
        _draw.AddRange(_discard);
        _discard.Clear();

        foreach (var card in _draw)
            card.transform.SetParent(_drawPoint);
    }

    public void DiscardCard(Card card)
    {
        if (card is CardInHand)
        {
            DiscardCard(card as CardInHand);
            return;
        }
        Debug.LogError("Try to discard not hand card");
    }

    public void DiscardCard(CardInHand card)
    {
        card.transform.SetParent(_discardPoint);
        card.transform.position = _discardPoint.position;
        _discard.Add(card);
    }


}
