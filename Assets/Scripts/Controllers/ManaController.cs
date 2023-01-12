using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BattleSystem;

public class ManaController : MonoBehaviour
{

    public static int CurrentManaValue = 5;

    [SerializeField] private TextMeshPro Mana;

    private ManaController instance;

    private void Awake()
    {
        instance = (instance == null) ? this : instance;

        Context.ManaChanged += (int amount) => Mana.text = amount.ToString(); 
    }



}
