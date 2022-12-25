using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    public static Hand inctance = null;
    
    [SerializeField] private Transform handBase;
    [SerializeField] private float maxHandRadius = 100f;
    [SerializeField] private float moveSpeed = .3f;
    [SerializeField] private Deck deck;
    [SerializeField] private int _startingCardCount = 5;
    [SerializeField] private int _maxCardsInHand = 10;

    private List<Vector3> _cardPositions;
    private List<HandCard> _cards = new ();
    private int _currentCardIndex = 0;
    private float _moveCooldown = 0;
    private float _cardGap;

    private void Start()
    {
        if (inctance == null)
        {
            inctance = this;
        }
        
        _cardGap = deck.GetCardSize().x * deck.GetCardLocalScale().x * .6f;
        SetCardPositions(_startingCardCount);
        GetCardsFromDeck(_startingCardCount);
        GameManager.onTurnEnd += (sender, args) =>
        {
            Debug.Log("Draw");
            GetCardsFromDeck(2);
        };
    }

    private void UseCard(HandCard card)
    {
        deck.DiscardCard(card);
        var cardTransform = card.transform;
        cardTransform.DOMove(cardTransform.up * 30, moveSpeed).OnComplete(() =>
        {
            // TODO Иногда запускается после SetActive(true),
            // в колоде появляется пустая выключенная карта
            cardTransform.gameObject.SetActive(false);
        });
        _cards.Remove(card);
        SetCardPositions(_cards.Count);
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].transform.DOMove(_cardPositions[i], moveSpeed);
        }
    }

    private void SetCardPositions(int cardCount)
    {
        var gapHandRadius = _cardGap * (cardCount - 1);
        var handRadius = (gapHandRadius <= maxHandRadius)? gapHandRadius : maxHandRadius;
        var handLenght = handRadius * 2;
        var positionStep = handLenght / (cardCount - 1);
        _cardPositions = new List<Vector3>(cardCount);
        var cardPosition = handBase.position + new Vector3(handRadius, 0, 0);
        for (var i = 0; i < cardCount; i++)
        {
            _cardPositions.Add(cardPosition);
            cardPosition -= new Vector3(positionStep, 0, 0);
        }
    }
    
    private void UpdateCardPositions()
    {
        var count = _cards.Count;
        var gapHandRadius = _cardGap * (count - 1);
        var handRadius = (gapHandRadius <= maxHandRadius)? gapHandRadius : maxHandRadius;
        var handLenght = handRadius * 2;
        var positionStep = handLenght / (count - 1);
        _cardPositions = new List<Vector3>(count);
        var cardPosition = handBase.position + new Vector3(handRadius, 0, 0);
        for (var i = 0; i < count; i++)
        {
            _cardPositions.Add(cardPosition);
            var targetPosition = cardPosition;
            _cards[i].transform.DOMove(targetPosition, moveSpeed);
            cardPosition -= new Vector3(positionStep, 0, 0);
        }
    }

    private void GetCardsFromDeck(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            if (_cards.Count == _maxCardsInHand)
            {
                break;
            }
            GetCardFromDeck();
        }
    }

    private void GetCardFromDeck()
    {
        var card = deck.DrawCard();
        _cards.Add(card);
        UpdateCardPositions();
        card.onPlay += (sender, args) =>
        {
            UseCard(sender as HandCard);

            //deck.DiscardCard((sender as HandCard));
            //var cardTransform = (sender as HandCard).transform;
            //cardTransform.DOMove(cardTransform.up * 30, moveSpeed).OnComplete(() =>
            //{
            //    // TODO Иногда запускается после SetActive(true),
            //    // в колоде появляется пустая выключенная карта
            //    cardTransform.gameObject.SetActive(false);
            //});
            //_cards.Remove((sender as HandCard));
            //SetCardPositions(_cards.Count);
            //for (int i = 0; i < _cards.Count; i++)
            //{
            //    _cards[i].transform.DOMove(_cardPositions[i], moveSpeed);
            //}
        };
    }


    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireCube(handBase.position, new Vector3(maxHandRadius * 2, 20));

    }

}