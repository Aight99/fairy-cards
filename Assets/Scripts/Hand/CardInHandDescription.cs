using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInHandDescription : MonoBehaviour
{

    [SerializeField] TextMeshPro name;
    [SerializeField] TextMeshPro EffectDescription;
    [SerializeField] TextMeshPro Description;

    [SerializeField] SpriteRenderer sprite;
   
    public void DisplayCardDesctiption(HandCardData data)
    {
        name.text = data.name;
        EffectDescription.text = data.EffectDescription;
        //Description.text = data.Description;
        sprite.sprite = data.Image;
    }

    private void Disable()
    {
        name.gameObject.SetActive(false);
        EffectDescription.gameObject.SetActive(false);  
        //Description.gameObject.SetActive(false);
        sprite.gameObject.SetActive(false);
    }

}
