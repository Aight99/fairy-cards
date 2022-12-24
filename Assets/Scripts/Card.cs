using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{

    private BoxCollider _meshCollider;

    [SerializeField]
    private CardTypes cardType;

    [SerializeField]
    private GameObject _gardInfo;

    public Attack[] Attacks;


    private void Awake()
    {
        _meshCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = GameManager.mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!_meshCollider.Raycast(ray, out RaycastHit hitInfo, 1000))
        {
            _gardInfo.SetActive(false);
            return;
        }
        _gardInfo.SetActive(true);



        switch (GameManager.CurrentState)
        {
            case States.Idle:
                break;

            case States.PlayerAttack:

                if (cardType == CardTypes.PlayerCard)
                    break;

                _gardInfo.SetActive(false);
                Debug.Log($"Card Was Attack by {GameManager.CurrentSelectedAttack.Name}");


                GameManager.ChangeState(States.EnemyAttack);
                break;
            case States.EnemyAttack:



                break;
            default:
                break;
        }

    }


}
