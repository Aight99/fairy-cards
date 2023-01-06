using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInHand : Card
{
    [SerializeField]
    private Vector3 ScaleShift;

    protected override void CursorEnter()
    {
        transform.localScale += ScaleShift;
    }

    protected override void CursorLeft()
    {
        transform.localScale -= ScaleShift;
    }
}
