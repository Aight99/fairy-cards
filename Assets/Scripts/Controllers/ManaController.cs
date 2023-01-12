using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BattleSystem;

public class ManaController : MonoBehaviour
{

    public static int CurrentManaValue = 3;
    public static int MaxManatManaValue = 5;

    [SerializeField] private List<GameObject> manaCoins;

    private ManaController instance;

    private void Awake()
    {
        instance = (instance == null) ? this : instance;

        Context.ManaChanged += (int amount) => 
        { 
            CurrentManaValue = amount;
            SetManaCoins(amount);
        };

    }

    private void SetManaCoins(int mana)
    {
        for (int i = 0; i < mana; i++)
        {
            manaCoins[i].SetActive(true);
        }
        for (int i = mana; i < 5; i++)
        {
            manaCoins[i].SetActive(false);
        }
    }
}
