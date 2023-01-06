using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Card : Updateble
{
    public UnityEvent onCursorEnter;
    public UnityEvent onCursorLeft;
    public UnityEvent onClick;


    private BoxCollider boxCollider;
    private bool prevHitValue;
    
    public void Start()
    {

        prevHitValue = false;

        onCursorEnter = new UnityEvent();
        onCursorLeft = new UnityEvent();
        onClick = new UnityEvent();


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
            onClick?.Invoke();
        }

        if (prevHitValue && !isHit)
        {
            CursorLeft();
            onCursorLeft?.Invoke();
        }

        if (!prevHitValue && isHit)
        {
            CursorEnter();
            onCursorEnter?.Invoke();
        }

        prevHitValue = isHit;

    }


    protected virtual void Click() { }
    protected virtual void CursorEnter() { }
    protected virtual void CursorLeft() { }

}
