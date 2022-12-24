using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandCard : MonoBehaviour
{
    [SerializeField] private HandCardData cardData;

    public EventHandler onCursorEnter;
    public EventHandler onCursorLeft;
    public EventHandler onPlay;


    private BoxCollider _boxCollider;
    private bool prevHitValue;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        _boxCollider = GetComponent<BoxCollider>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        prevHitValue = _boxCollider.Raycast(ray, out RaycastHit hitInfo, 1000);

        onCursorEnter += (sender, args) =>
        {
            (sender as HandCard).transform.localScale += new Vector3(0.3f, 0.3f, 0);
        };
        
        onCursorLeft += (sender, args) =>
        {
            (sender as HandCard).transform.localScale -= new Vector3(0.3f, 0.3f, 0);
        };

        onPlay += (sender, args) =>
        {
            Debug.Log("Card play");
        };

    }

    private void Update()
    {
        bool isClick = Input.GetMouseButtonDown(0);
       
        Ray ray = GameManager.mainCamera.ScreenPointToRay(Input.mousePosition);

        bool isHit = _boxCollider.Raycast(ray, out RaycastHit hitInfo, 1000);

        if (prevHitValue && !isHit)
            onCursorLeft?.Invoke(this, null);

        if (!prevHitValue && isHit)
            onCursorEnter?.Invoke(this, null);

        if (isHit && isClick)
            onPlay?.Invoke(this, null);

        prevHitValue = isHit;
    }

}