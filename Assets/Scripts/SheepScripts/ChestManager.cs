using System;
using System.Collections;
using System.Collections.Generic;
using UIControl;
using UnityEngine;


public class ChestManager : MonoBehaviour
{
    [SerializeField] private int scoreValue;
    private HpBarControl _hpBarControl;
    private ScoreControl _scoreControl;
    private AudioSource _audio;
    

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _hpBarControl = GameObject.FindWithTag("ShipLevelBar").GetComponent<HpBarControl>();
        _scoreControl = GameObject.FindWithTag("GlobalScore").GetComponent<ScoreControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _scoreControl.SetScoreValue(scoreValue);
            _audio.Play();
            if (gameObject.CompareTag("Bomb")) scoreValue *= -1;
            

            _hpBarControl.SetHpValue(scoreValue);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }
            
        }
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
