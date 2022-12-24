using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum States
{

    Idle,
    PlayerAttack,
    EnemyAttack

}

public class GameManager : MonoBehaviour
{

    public static States CurrentState = States.Idle;
    public static Camera mainCamera;


    private GameManager Instanse;

    // Start is called before the first frame update
    void Start()
    {
        Instanse = this;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
