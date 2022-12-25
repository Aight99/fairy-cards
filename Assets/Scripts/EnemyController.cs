using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    public static List<Card> EnemyesCardList;


    public static void Init()
    {
        EnemyesCardList = GameManager.EnemyCardsList;


        foreach (var enemy in EnemyesCardList)
        {
            enemy.onDead += (object sender, EventArgs args) =>
            {
                GameManager.DeleteEnemy((sender as Card));
            };
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.CurrentState != States.EnemyAttack )
            return;

        //var randomEnemy = EnemyesCardList[Random.Range(0, EnemyesCardList.Count)];

        var seq = DOTween.Sequence();

        foreach (var enemy in EnemyesCardList.Where(e => e.canAttack))
        {


            var randomAttack = enemy.Attacks[Random.Range(0, enemy.Attacks.Length)];


            //enemy.AnimateAttack(GameManager.PlayerCard, () => { });

            
            var basePosition = enemy.transform.position;

            if (GameManager.PlayerCard != null)
            {
                var tween = enemy.transform.DOMove(GameManager.PlayerCard?.transform.position ?? basePosition, 0.3f);
                tween.onComplete += () =>
                {
                    GameManager.PlayerCard?.TakeDamage(randomAttack.Damage);
                };

                seq.Append(tween);


                seq.Append(enemy.transform.DOMove(basePosition, 0.3f));


                seq.AppendInterval(0.3f);
            }
            //GameManager.PlayerCard.TakeDamage(randomAttack.Damage);
            GameManager.ChangeState(States.Waiting);
        }

        seq.onComplete += () =>
        {
           
            GameManager.ChangeState(States.Idle);
        };


    }
}
