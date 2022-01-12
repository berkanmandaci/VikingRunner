using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIControl;
using UnityEngine;
using UnityEngine.UI;


public class Gate : MonoBehaviour
{
  //
    public int point;
    public bool multiply;
    public bool divide;
    public bool add;
    public bool remove;

    public TextMesh pointText;

    public TextMeshPro pointTextt;

    public Color adding;
    public Color32 removing;
    //
    
    [SerializeField] private HpBarControl _hpBarControl;
    [SerializeField] private ScoreControl _scoreControl;
    [SerializeField] private ShipManager _shipManager;
    [SerializeField] private string _member;
    [SerializeField] private AudioSource _audio;

   
    void Start()
    {
        
       // _audio= GetComponent<AudioSource>();
        
        
        var process = "";
        if (multiply)
        {
            process = "x";
        }
        else if (divide)
        {
            process = "/";
        }
        else if (add)
        {
            process = "+";
        }
        else if (remove)
        {
            process = "-";
        }

        pointTextt.text = process + point;
        // SetMember();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GateController"))
        {
            
            if (multiply)
            {
                _scoreControl.MultiplyFriendsCount((int)point);
            }
            else if (divide)
            {
                _scoreControl.DivideFriendsCount((int)point);
            }
            else if (add)
            {
                _scoreControl.AddFriendsCount((int)point);
                _scoreControl.InstantiateCircle(point);
            }
            else if (remove)
            {
                _scoreControl.DestroySheep((int)point);
               // if(_scoreControl.friendsCount==0) return;
                _scoreControl.RemoveFriendsCount((int)point);
            }
//            _audio.Play();
            _hpBarControl.SetHpValue(point);
            _scoreControl.SetScoreValue(Math.Abs(point));
            this.gameObject.transform.Find ("Panel").gameObject.SetActive(false);
       //     _shipManager.SetShip(_shipManager.activeShipIndex);
            Invoke(nameof(PlaySound),0.1f);
        }
    }
    
    private void PlaySound()
    {
        Destroy(gameObject);
    }
    
    private void SetMember()
    {
        if (gameObject.CompareTag("Good"))
        {
            _member = "Good";
            point = +10;
        }
        if (gameObject.CompareTag("Evil"))
        {
            _member = "Evil";
            point = -10;
        }
    }
}
