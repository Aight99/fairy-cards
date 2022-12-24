using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    private List<HandCard> _cards = new ();
    private List<HandCard> _discard = new ();
    private int _currentSortingLayer;
    
    private void Awake()
    {
        _cards.AddRange(GetComponentsInChildren<HandCard>());
        _currentSortingLayer = 0;
        Shuffle();
    }

    private void Shuffle()
    {
        _cards = _cards.OrderBy(i => Random.value).ToList();
        foreach (var card in _cards)
        {
            card.GetComponent<SpriteRenderer>().sortingOrder = _currentSortingLayer;
            _currentSortingLayer++;
        }
    }

    public Vector2 GetCardSize() => _cards[0].GetComponent<SpriteRenderer>().size;

    public Vector3 GetCardLocalScale() => _cards[0].transform.localScale;

    public HandCard DrawCard()
    {
        var card = _cards[0];
        _cards.RemoveAt(0);
        if (_cards.Count == 0)
        {
            _cards.AddRange(_discard);
        }
        return card;
    }

    public void DiscardCard(HandCard card)
    {
        // Disable and add to discard deck
    }
}
