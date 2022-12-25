using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Button[] PlayerAttacksButton;

    public static Attack CurrentSelectedAttack;

    private static PlayerManager instance;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    public static void Init()
    {
        for (int i = 0; i < GameManager.PlayerCard.Attacks.Length; i++)
        {
            instance.PlayerAttacksButton[i].GetComponent<Image>().sprite = GameManager.PlayerCard.Attacks[i].spriteImage;
        }

        foreach (var enemyCard in GameManager.EnemyCardsList)
        {
            

            enemyCard.onClick += (object sender, EventArgs args) =>
            {

                if (GameManager.CurrentState != States.PlayerAttack)
                    return;

                if (CurrentSelectedAttack == null)
                    return;

                if (!GameManager.PlayerCard.canAttack)
                    return;

                GameManager.PlayerCard.AnimateAttack((sender as Card) , () => { GameManager.ChangeState(States.EnemyAttack); });
                (sender as Card).TakeDamage(CurrentSelectedAttack.Damage);
                ManaManager.TakeMana(CurrentSelectedAttack.Cost);
                GameManager.ChangeState(States.Waiting);
            };
        }
    }



    public void ChangeCurrentAttack(int index)
    {
        if (GameManager.CurrentState != States.Idle)
            return;

        var newAttack = GameManager.PlayerCard.Attacks[index];

        if (newAttack.Cost > ManaManager.CurrentManaValue)
            return;

        CurrentSelectedAttack = newAttack;

        GameManager.ChangeState(States.PlayerAttack);
    }



}
