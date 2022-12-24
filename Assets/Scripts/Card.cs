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

    public EventHandler onCursorEnter;
    public EventHandler onCursorLeft;
    public EventHandler onClick;
    public EventHandler onDead;



    private BoxCollider _boxCollider;
    [SerializeField]
    public GameObject _gardInfo;
    private bool prevHitValue;

    [SerializeField]
    private CardTypes cardType;



    public Attack[] Attacks;


    public TextMeshPro healthPointText;
    public StatsInfo stats;

    private int healthPoint;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();

        Ray ray = GameManager.mainCamera.ScreenPointToRay(Input.mousePosition);
        prevHitValue = _boxCollider.Raycast(ray, out RaycastHit hitInfo, 1000);


        healthPointText.text = stats.healthPoints.ToString();
        healthPoint = stats.healthPoints;

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

    public void AnimateAttack(Card target)
    {

        var seq = DOTween.Sequence();
        var basePosition = transform.position;
        seq.Append(transform.DOMove(target.transform.position, 0.3f));

        seq.Append(transform.DOMove(basePosition, 0.3f));

        seq.SetEase(Ease.Linear);

    }

    public void TakeDamage(int amount)
    {
        healthPoint -= amount;

        if (healthPoint <= 0)
            onDead?.Invoke(this, null);
    }
  



}
