using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int ScoreValue;
     private LevelController levelController;
     private ScoreController scoreController;
    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.FindWithTag("LevelController").GetComponent<LevelController>();
        scoreController = GameObject.FindWithTag("ScoreController").GetComponent<ScoreController>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally"))
        {
            //TODO add ScoreValue
           
            
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    { 
        scoreController.AddScore(ScoreValue);
        levelController.AddExp(ScoreValue);
        //TODO play anim and play Sound
    }
}
