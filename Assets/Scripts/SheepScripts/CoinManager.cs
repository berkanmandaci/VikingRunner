using System;
using System.Collections;
using System.Collections.Generic;
using UIControl;
using UnityEngine;


public class CoinManager : MonoBehaviour
{
    [SerializeField] private int scoreValue;
    private HpBarControl _hpBarControl;
    private ScoreControl _scoreControl;
    private AudioSource _audio;
    public ParticleSystem ps;
    public ParticleSystem ps2;
    public ParticleSystem ps3;
    
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _hpBarControl=GameObject.FindWithTag("ShipLevelBar").GetComponent<HpBarControl>();
        _scoreControl=GameObject.FindWithTag("GlobalScore").GetComponent<ScoreControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _scoreControl.SetScoreValue(scoreValue);
            _audio.Play();
            if (gameObject.CompareTag("Bomb"))
            {
                scoreValue *= -1;
                ps.Play();
                ps2.Play();
                ps3.Play();
            } 
           
            _hpBarControl.SetHpValue(scoreValue);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

  
}
