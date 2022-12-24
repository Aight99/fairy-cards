using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform handBase;
    [SerializeField] private float maxHandRadius = 100f;
    [SerializeField] private float moveSpeed = .5f;
    [SerializeField] private Deck deck;
    [SerializeField] private int _startingCardCount = 5;
    [SerializeField] private int _maxCardsInHand = 10;

    private List<Vector3> _cardPositions;
    private List<HandCard> _cards = new ();
    private int _currentCardIndex = 0;
    private float _moveCooldown = 0;
    private float _cardGap;

    private void Awake()
    {
        _cardGap = deck.GetCardSize().x * deck.GetCardLocalScale().x * .6f;
        SetCardPositions(_startingCardCount);
        GetCardsFromDeck(2);
    }

    private void Update()
    {
        _moveCooldown -= Time.deltaTime;
        if (_moveCooldown > 0)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            MoveCardToNextPlace();
        }
        _moveCooldown = .5f;
    }

    private void MoveCardToNextPlace()
    {
        _currentCardIndex = (_currentCardIndex + 1) % _cards.Count;
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

    private void GetCardsFromDeck(int numberOfCards)
    {
        var drawCardDuration = .3f;
        var sequence = DOTween.Sequence();
        
        for (int i = _cardPositions.Count - numberOfCards; i < _cardPositions.Count; i++)
        {
            var card = deck.DrawCard();
            _cards.Add(card);
            var targetPosition = _cardPositions[i];
            sequence.Append(card.transform.DOJump(targetPosition, 20f, 1, drawCardDuration));
            sequence.AppendInterval(drawCardDuration);
        }
        sequence.AppendCallback(() => Debug.Log("Всё!"));
    }
}