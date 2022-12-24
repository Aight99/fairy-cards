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
    private SpriteRenderer _spriteRenderer;
    private int _cardSortingLayer;
    private bool _prevHitValue;

    private void OnEnable()
    {
        _cardSortingLayer = _spriteRenderer.sortingOrder;
    }

    private void Awake()
    {
        var scaleFactor = 1.3f;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider>();

        _spriteRenderer.color = Random.ColorHSV();


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _prevHitValue = _boxCollider.Raycast(ray, out RaycastHit hitInfo, 1000);

        onCursorEnter += (sender, args) =>
        {
            (sender as HandCard).transform.localScale *= scaleFactor;
            _spriteRenderer.sortingOrder = 500;
        };
        
        onCursorLeft += (sender, args) =>
        {
            (sender as HandCard).transform.localScale /= scaleFactor;
            _spriteRenderer.sortingOrder = _cardSortingLayer;
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

        if (_prevHitValue && !isHit)
            onCursorLeft?.Invoke(this, null);

        if (!_prevHitValue && isHit)
            onCursorEnter?.Invoke(this, null);

        if (isHit && isClick)
            onPlay?.Invoke(this, null);

        _prevHitValue = isHit;
    }

    public void SetSortingLayer(int layerNumber)
    {
        _spriteRenderer.sortingOrder = layerNumber;
        _cardSortingLayer = layerNumber;
    }
}