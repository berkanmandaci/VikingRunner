using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FightAreaController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject EnemyPool;
    [SerializeField] private EnemyPoolController enemyPoolController;


    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckEndFight();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally"))
        {
            Debug.Log("Fight Start");
            InputManager.isFight = true;
            enemyPoolController.thisFight = true;
            IsFight();
        }
    }

    private void IsFight()
    {
        var targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
        player.transform.DOMove(targetPos, 0.8f).SetEase(Ease.Linear);
    }

    private void Attack()
    {
        //TODO attack anim
    }

    private void CheckEndFight()
    {
        if (EnemyPool.transform.childCount==0)
        {
            Debug.Log("Fight End");
            InputManager.isFight = false;
            Destroy(gameObject);
        }
    }
}
