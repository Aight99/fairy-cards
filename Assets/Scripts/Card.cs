using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{

    private BoxCollider _meshCollider;

    [SerializeField]
    private CardTypes cardType;

    [SerializeField]
    private GameObject _gardInfo;

    public Attack[] Attacks;


    public TextMeshPro healthPointText;
    public StatsInfo stats;

    private int healthPoint;

    private void Awake()
    {
        _meshCollider = GetComponent<BoxCollider>();
        healthPointText.text = stats.healthPoint.ToString();
        healthPoint = stats.healthPoint;
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





        switch (GameManager.CurrentState)
        {
            case States.Idle:
                _gardInfo.SetActive(true);
                break;

            case States.PlayerAttack:

                if (cardType == CardTypes.PlayerCard)
                    break;

                _gardInfo.SetActive(false);

                var seq = DOTween.Sequence();
                Vector3 basePosition = GameManager.PlayerCard.transform.position;
                seq.Append(GameManager.PlayerCard.transform.DOMove(transform.position, 0.3f));
                seq.Append(GameManager.PlayerCard.transform.DOMove(basePosition, 0.3f));

                seq.SetEase(Ease.Linear);

                GameManager.ChangeState(States.Waiting);

                seq.OnComplete(() => GameManager.ChangeState(States.EnemyAttack));


                Debug.Log($"Card Was Attack by {GameManager.CurrentSelectedAttack.Name}");

                this.TakeDamage(GameManager.CurrentSelectedAttack.Damage);


                break;
            case States.EnemyAttack:


               

                break;
            default:
                break;
        }

    }

    public void Attack()
    {
        var randomAttack = Attacks[Random.Range(0, Attacks.Length)];


        GameManager.PlayerCard.TakeDamage(randomAttack.Damage);
        var seq = DOTween.Sequence();
        var basePosition = transform.position;
        seq.Append(transform.DOMove(GameManager.PlayerCard.transform.position, 0.3f));

        seq.Append(transform.DOMove(basePosition, 0.3f));

        seq.SetEase(Ease.Linear);

        GameManager.ChangeState(States.Waiting);

        seq.OnComplete(() => GameManager.ChangeState(States.Idle));

    }

    public void TakeDamage(int amount)
    {
        healthPoint -= amount;
        healthPointText.text = healthPoint.ToString();


        if (healthPoint <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
