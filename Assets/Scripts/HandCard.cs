using UnityEngine;

public class HandCard : MonoBehaviour
{
    [SerializeField] private HandCardData cardData;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
}
