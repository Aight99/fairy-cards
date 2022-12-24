using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    public static List<Card> EnemyesCardList;


    public static void Init()
    {
        EnemyesCardList = GameManager.EnemyCardsList;


        foreach (var enemy in EnemyesCardList) {
            enemy.onDead += (object sender, EventArgs args) => {
                GameManager.DeleteEnemy((sender as Card));
            };
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.CurrentState != States.EnemyAttack)
            return;

        var randomEnemy = EnemyesCardList[Random.Range(0, EnemyesCardList.Count)];

        var randomAttack = randomEnemy.Attacks[Random.Range(0, randomEnemy.Attacks.Length)];


        randomEnemy.AnimateAttack(GameManager.PlayerCard, () => { GameManager.ChangeState(States.Idle); });
        GameManager.PlayerCard.TakeDamage(randomAttack.Damage);
        GameManager.ChangeState(States.Waiting);

        
    }
}
