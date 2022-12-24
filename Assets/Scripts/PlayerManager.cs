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

    private void Start()
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

                GameManager.PlayerCard.AnimateAttack((sender as Card));
                (sender as Card).TakeDamage(CurrentSelectedAttack.Damage);
                GameManager.ChangeState(States.EnemyAttack);
            };
        }

    }



    public void ChangeCurrentAttack(int index)
    {
        CurrentSelectedAttack = GameManager.PlayerCard.Attacks[index];

        GameManager.ChangeState(States.PlayerAttack);
    }



}
