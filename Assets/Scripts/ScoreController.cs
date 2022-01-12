using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI inGameScore;
    [SerializeField] private TextMeshProUGUI inGameCoinScore;
    [SerializeField] private TextMeshProUGUI lastGameScore;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Slider hpBarSlider;
    [SerializeField] private AllyPool allyPool;
    
    
    
    
    private int allyCount;
    private int inGameScoreValue;
    private int inGameCoinScoreValue;
    private int lastGameScoreValue;
    
    // Start is called before the first frame update
    void Start()
    {
        allyCount = 1;
    }


    private void Update()
    { 
      
    }

    public void AddAllyCount(int _allyCount)
    {
        allyCount += _allyCount;
        inGameScore.text = allyCount.ToString();
    }
    public void RemoveAllyCount(int _allyCount)
    {
        allyCount -= _allyCount;
        inGameScore.text = allyCount.ToString();
        
        if (allyCount<=0)
            gameManager.GameOver();
    }

    public void AddScore(int value)
    {
        inGameCoinScoreValue += value;
        inGameCoinScore.text = inGameCoinScoreValue.ToString();
    }
}
