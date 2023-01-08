using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardInHand : Card
{
    [SerializeField] private Vector3 ScaleShift;
    [SerializeField] private Vector3 PositionShift;

    public UnityEvent<Card> onPlay;

    protected override void CursorEnter()
    {
        transform.localScale += ScaleShift;
        transform.localPosition += PositionShift;
    }

    protected override void CursorLeft()
    {
        transform.localScale -= ScaleShift;
        transform.localPosition -= PositionShift; 
    }


    protected override void Click()
    {
        onPlay?.Invoke(this);
        Debug.Log("Card in Hand was played");
    }

    protected virtual void Play()
    {

    }

}
