using System;
using System.Linq;
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


    }

    private void Update()
    {
        bool isClick = Input.GetMouseButtonDown(0);

        Ray ray = GameManager.mainCamera.ScreenPointToRay(Input.mousePosition);

        bool isHit = _boxCollider.Raycast(ray, out RaycastHit hitInfo, 1000);

        var hits = Physics.RaycastAll(ray, 1000);


        hits = hits.OrderBy(hit =>
        {
            var sp = hit.collider.GetComponent<SpriteRenderer>();

            if (sp == null)
                return -1;

            return sp.sortingOrder;


        }).ToArray();
        bool isFirstInPile = hits.Length > 0 && hits[^1].collider == _boxCollider;   

       

        if (_prevHitValue && !isFirstInPile)
            onCursorLeft?.Invoke(this, null);

        if (!_prevHitValue  && isFirstInPile)
            onCursorEnter?.Invoke(this, null);


        if ( isClick && isFirstInPile)
            onPlay?.Invoke(this, null);

        _prevHitValue = isFirstInPile;
    }

    public void SetSortingLayer(int layerNumber)
    {
        _spriteRenderer.sortingOrder = layerNumber;
        _cardSortingLayer = layerNumber;
    }
}