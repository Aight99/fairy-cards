using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BattleSystem;

public class ManaController : MonoBehaviour
{

    public static int CurrentManaValue = 3;
    public static int MaxManatManaValue = 5;

    [SerializeField] private TextMeshPro Mana;

    private ManaController instance;

    private void Start()
    {
        instance = (instance == null) ? this : instance;

        Context.ManaChanged += (int amount) => 
        { 
            CurrentManaValue= amount;
            Mana.text = $"{amount}/{MaxManatManaValue}"; };

    }



}
