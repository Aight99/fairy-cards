using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{

    private BoxCollider _meshCollider;
    [SerializeField]
    private GameObject _gardInfo;


    // Start is called before the first frame update
    void Start()
    {
        _meshCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = GameManager.mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!_meshCollider.Raycast(ray, out RaycastHit hitInfo, 1000))
        {
            _gardInfo.SetActive(false);
            return;
        }

        switch (GameManager.CurrentState)
        {
            case States.Idle:
                _gardInfo.SetActive(true);
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
