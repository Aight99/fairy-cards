using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaManager : MonoBehaviour
{

    public static int CurrentManaValue;


    public static int MaxManValue;

    [SerializeField]
    private int _startManaValue;
    [SerializeField]
    private TextMeshPro manaText;

    private static ManaManager instance;

    private void Start()
    {
        instance = this;
    }

    public static void Init()
    {
        MaxManValue = instance._startManaValue;
        CurrentManaValue = MaxManValue;
        instance.manaText.text = $"{CurrentManaValue}/{MaxManValue}";
        GameManager.onTurnEnd += (sender, args) =>
        {
            MaxManValue += 1;
            CurrentManaValue = MaxManValue;
            instance.manaText.text = $"{CurrentManaValue}/{MaxManValue}";
        };
    }

    public static void ResetMana()
    {
        CurrentManaValue = MaxManValue;
        instance.manaText.text = $"{CurrentManaValue}/{MaxManValue}";
    }


    public static void TakeMana(int amount)
    {
        CurrentManaValue -= amount;
        instance.manaText.text = $"{CurrentManaValue}/{MaxManValue}";
    }
}
