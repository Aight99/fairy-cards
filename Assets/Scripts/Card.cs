using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class Card : MonoBehaviour
{
    [SerializeField] public GameObject _gardInfo;
    [SerializeField] private CardTypes cardType;
    
    public Character character;
    
    public EventHandler onCursorEnter;
    public EventHandler onCursorLeft;
    public EventHandler onClick;
    public EventHandler onDead;

    public bool canAttack = true;

    private BoxCollider _boxCollider;
    private bool prevHitValue;

    public Attack[] Attacks;
    public TextMeshPro healthPointText;
    public StatsInfo stats;
    private int healthPoints;

    private void Awake()
    {
        canAttack = true;
        _boxCollider = GetComponent<BoxCollider>();

        Ray ray = GameManager.mainCamera.ScreenPointToRay(Input.mousePosition);
        prevHitValue = _boxCollider.Raycast(ray, out RaycastHit hitInfo, 1000);


        healthPointText.text = stats.healthPoints.ToString();
        healthPoints = stats.healthPoints;

        GameManager.onTurnEnd += (sender, args) =>
        {
            canAttack = true;
        };

    }

    private void Update()
    {
        bool isClick = Input.GetMouseButtonDown(0);

        Ray ray = GameManager.mainCamera.ScreenPointToRay(Input.mousePosition);

        bool isHit = _boxCollider.Raycast(ray, out RaycastHit hitInfo, 1000);

        if (isHit && isClick)
            onClick?.Invoke(this, null);

        if (prevHitValue && !isHit)
            onCursorLeft?.Invoke(this, null);

        if (!prevHitValue && isHit)
            onCursorEnter?.Invoke(this, null);

        prevHitValue = isHit;
    }

    public void AnimateAttack(Card target , TweenCallback onEnd)
    {

        var seq = DOTween.Sequence();
        var basePosition = transform.position;
        seq.Append(transform.DOMove(target.transform.position, 0.3f));

        seq.Append(transform.DOMove(basePosition, 0.3f));

        seq.SetEase(Ease.Linear);
        seq.onComplete += onEnd;

    }

    public void TakeDamage(int amount)
    {
        healthPoints -= amount;

        if (healthPoints <= 0)
        {
            onDead?.Invoke(this, null);
            return;
        }

        healthPointText.text = healthPoints.ToString();

       
    }
  



}
