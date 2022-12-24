using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{

    private MeshCollider _meshCollider;
    private Plane _plane;


    // Start is called before the first frame update
    void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = GameManager.mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!_meshCollider.Raycast(ray, out RaycastHit hitInfo, 1000))
            return;



        switch (GameManager.CurrentState)
        {
            case States.Idle:
                break;
            case States.PlayerAttack:
                break;
            case States.EnemyAttack:
                break;
            default:
                break;
        }

    }

    void ShowCardInfo()
    {

    }

}
