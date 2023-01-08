using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Updateble
{

    [SerializeField] Updateble TableController;

    private Updateble currentUpdateble;

    public override void _Start()
    {
        currentUpdateble = this;
        Debug.Log("Enmey make it`s turn");
    }

    public override void _Update()
    {
        currentUpdateble = TableController;
    }

    public override void _End()
    {
        Debug.Log("Enemy made it`s turn");
    }

    public override Updateble GetNextUpdateble() => currentUpdateble;




}
