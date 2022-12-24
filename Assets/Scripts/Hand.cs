using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform handBase;
    [SerializeField] private float maxHandRadius = 100f;
    [SerializeField] private float moveSpeed = .5f;
    [SerializeField] private List<Transform> cards;

    private List<Vector3> _cardPositions;
    private int _cardsInHand;
    private int _currentCardIndex = 0;
    private float _moveCooldown = 0;
    private bool _isInteractive = false;
    private int _cardsToDraw;
    private float _cardGap;

    private void Awake()
    {
        _cardGap = cards[0].GetComponent<SpriteRenderer>().size.x * .6f * cards[0].transform.localScale.x;
        _cardsInHand = cards.Count;
        _cardsToDraw = _cardsInHand;
        SetCardPositions();
        GetAllCardsFromDeck();
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
        _currentCardIndex = (_currentCardIndex + 1) % _cardsInHand;
    }

    private void SetCardPositions()
    {
        var gapHandRadius = _cardGap * (_cardsInHand - 1);
        var handRadius = (gapHandRadius <= maxHandRadius)? gapHandRadius : maxHandRadius;
        var handLenght = handRadius * 2;
        var positionStep = handLenght / (_cardsInHand - 1);
        _cardPositions = new List<Vector3>(_cardsInHand);
        var cardPosition = handBase.position + new Vector3(-handRadius, 0, 0);
        for (var i = 0; i < _cardsInHand; i++)
        {
            _cardPositions.Add(cardPosition);
            cardPosition += new Vector3(positionStep, .1f, 0);
        }
    }

    private void GetAllCardsFromDeck()
    {
        var drawCardDuration = .3f;
        var sequence = DOTween.Sequence();
        // Поменять эту индексацию на метод AddCard()
        sequence.Append(cards[_cardsToDraw - 1].transform.DOJump(_cardPositions[_cardsToDraw - 1], 20f, 1, drawCardDuration));
        sequence.AppendCallback(() =>
            {
                if (_cardsToDraw != 0)
                {
                    _cardsToDraw--;
                    GetAllCardsFromDeck();
                }
                else
                {
                    _isInteractive = true;
                    Debug.Log("Done");
                }
            });
    }

    private void GetCardFromDeck()
    {
        
    }
}