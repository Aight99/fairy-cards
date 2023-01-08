using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] Battle battleInfo;
    [SerializeField] BattleSystem.BattleSystem battleSystem;



    private void Awake()
    {
        battleSystem.LoadBattle(battleInfo);

        foreach(var data in battleSystem.Context.Field.Where(i => i != null)) {
            SpawnCreture(data.creatureData);
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
