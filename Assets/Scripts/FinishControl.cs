using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine.Playables;

public class FinishControl : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayableDirector camPlayableDirector;
    [SerializeField] private Transform targetCamPos;
    [SerializeField] private float animTime;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally"))
        {
            InputManager.isFinish = true;
            
            camPlayableDirector.Play();
            Invoke(nameof(Finish),animTime);
            
            //camPos.DOMove(targetCamPos.position, animTime).OnComplete(() => gameManager.FinishGame());
        }
    }

    private void Finish()
    {
        gameManager.FinishGame();
    }
}
