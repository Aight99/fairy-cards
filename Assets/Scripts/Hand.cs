using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform handBase;
    [SerializeField] private float handRadius = 100f;
    [SerializeField] private int numberOfCards = 5;
    [SerializeField] private Transform card;
    [SerializeField] private float moveSpeed = .5f;

    private List<Vector3> _cardPositions;
    private int _currentCardIndex = 0;
    private float _moveCooldown = 0;

    private void Awake()
    {
        Debug.Log($"Base: {handBase.position.x}");
        SetCardPositions();
        card.DOMove(_cardPositions[_currentCardIndex], moveSpeed);
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
        if (Input.GetKey(KeyCode.A))
        {
            numberOfCards--;
            SetCardPositions();
        }
        if (Input.GetKey(KeyCode.D))
        {
            numberOfCards++;
            SetCardPositions();
        }
        _moveCooldown = .5f;
    }

    private void OnValidate()
    {
        _currentCardIndex = 0;
        SetCardPositions();
        card.DOMove(_cardPositions[_currentCardIndex], moveSpeed);
    }

    private void MoveCardToNextPlace()
    {
        _currentCardIndex = (_currentCardIndex + 1) % numberOfCards;
        card.DOMove(_cardPositions[_currentCardIndex], moveSpeed);
    }

    private void SetCardPositions()
    {
        float handLenght = handRadius * 2;
        float positionStep = handLenght / numberOfCards;
        _cardPositions = new List<Vector3>(numberOfCards);
        Vector3 cardPosition = handBase.position + new Vector3(-handRadius, 0, 0);
        for (int i = 0; i < numberOfCards; i++)
        {
            _cardPositions.Add(cardPosition);
            cardPosition += new Vector3(positionStep, 0, 0);
        }
        Debug.Log($"First: {_cardPositions[0].x}, Second: {_cardPositions[^1].x}");
    }
}