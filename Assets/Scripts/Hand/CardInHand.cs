using System.Collections;
using System.Collections.Generic;
using BattleSystem;
using UnityEngine;
using UnityEngine.Events;


public class CardInHand : Card
{
    [SerializeField] private Vector3 ScaleShift;
    [SerializeField] private Vector3 PositionShift;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] public HandCardData data;

    public UnityEvent<Card> onPlay;

    private void Start()
    {
        sprite.sprite = data.Image;
    }

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
        
        if (data.ManaUsage  <= ManaController.CurrentManaValue)
        {
            onPlay?.Invoke(this);
            Play();
        }
    }

    protected virtual void Play()
    {
        Debug.Log($"{data.name} played");
        BattleController.Instance.ExecuteCommand(Command.PlayCardCommand(data));
    }

}
