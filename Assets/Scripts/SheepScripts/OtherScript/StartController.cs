using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartController : MonoBehaviour
{
    public GameObject player;
    private TextMeshProUGUI levelTMP;
    private GameObject _canvas;
    private SceneChanger _sceneChanger;


    public void Start()
    {
        _sceneChanger = GetComponent<SceneChanger>();
       _canvas = GameObject.FindGameObjectWithTag("Canvas");
       var level= PlayerPrefs.GetInt("Level")+1;
       levelTMP = GameObject.FindWithTag("Level").GetComponent<TextMeshProUGUI>();
       levelTMP.text="LEVEL " + level;

        player=GameObject.FindWithTag("Player");
        
     //   player.GetComponent<Move>().started = true;

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvas.transform.Find("Play").gameObject.SetActive(true);
        // _canvas.transform.Find("Play").gameObject.SetActive(false);
        // _canvas.transform.Find("Move Box").gameObject.SetActive(true);
        //
    }
    public void SetStart()
    {
        
        for (int i = 0; i < _canvas.transform.Find("HpBarSlider").transform.childCount; i++)
        {
            _canvas.transform.Find("HpBarSlider").transform.GetChild(i).gameObject.SetActive(true);
            _canvas.transform.Find("ShipLevelSlider").transform.GetChild(i).gameObject.SetActive(true);
        }
        _canvas.transform.Find("ComboTMP").GetComponent<TextMeshProUGUI>().enabled=true;
        _canvas.transform.Find("Jolly Roger").gameObject.SetActive(true);
        _canvas.transform.Find("Union Jack").gameObject.SetActive(true);
        _canvas.transform.Find("Move Box").gameObject.SetActive(true);
        _canvas.transform.Find("Play").gameObject.SetActive(false);
        player.transform.Find("Bubble").gameObject.SetActive(true);
    }
    
}
