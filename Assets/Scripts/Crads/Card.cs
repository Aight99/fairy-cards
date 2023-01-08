using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : Updateble
{
    public UnityEvent<Card> onCursorEnter;
    public UnityEvent<Card> onCursorLeft;
    public UnityEvent<Card> onClick;


    private BoxCollider boxCollider;
    private bool prevHitValue;
    
    public void Start()
    {

        prevHitValue = false;

        onCursorEnter = new UnityEvent<Card>();
        onCursorLeft = new UnityEvent<Card>();
        onClick = new UnityEvent<Card>();


        boxCollider = GetComponent<BoxCollider>();
        
        if (boxCollider == null)
            Debug.LogError($"{this} Card object has no box collider");
    }

    public override void _Update()
    {
        bool isClick = Input.GetMouseButtonDown(0);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool isHit = boxCollider.Raycast(ray, out RaycastHit hitInfo, 1000);

        if (isHit && isClick)
        {
            Click();    
            onClick?.Invoke(this);
        }

        if (prevHitValue && !isHit)
        {
            CursorLeft();
            onCursorLeft?.Invoke(this);
        }

        if (!prevHitValue && isHit)
        {
            CursorEnter();
            onCursorEnter?.Invoke(this);
        }

        prevHitValue = isHit;

    }

    public Vector3 getSize() => boxCollider.bounds.size;


    protected virtual void Click() { }
    protected virtual void CursorEnter() { }
    protected virtual void CursorLeft() { }

}
