using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleSystem;

public class GameManager : MonoBehaviour
{

    [SerializeField] Battle battleInfo;
    [SerializeField] BattleController battleController;



    private void Awake()
    {
        battleController.LoadBattle(battleInfo);

        foreach(var data in battleController.Context.Field.Where(i => i != null)) {
            SpawnCreture(data.CreatureData);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCreture(CreatureData creatureData)
    {

    }

}
