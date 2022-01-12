using System;
using System.Collections;
using System.Collections.Generic;
using UIControl;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Evilships;
    [SerializeField] private List<GameObject> Goodships;
    [SerializeField] private Transform shipTransform;
    public ParticleSystem ps;
    public ParticleSystem ps2;
    
    public int activeShipIndex;

    [SerializeField] private HpBarControl _hpBarControl;
    // Start is called before the first frame update
    void Start()
    {
        _hpBarControl=GameObject.FindWithTag("HpBar").GetComponent<HpBarControl>();
        activeShipIndex = 0;
    }

    public void SetShip(int shipIndex)
    {
        // var activeShip = shipTransform.GetChild(_activeShipIndex);
        // activeShip.gameObject.SetActive(false);

     //   if (shipIndex == 4) shipIndex = 3;
        string member="";
        if (shipIndex > 3) shipIndex = 3;
        
        if (_hpBarControl.hpValue>=0)
        {
            member = "Good";
            
        }
        if (_hpBarControl.hpValue<0)
        {
            member = "Evil";
        }

        if (activeShipIndex!=shipIndex)
        {
            ps.Play();
            ps2.Play();
        }
        
        Evilships[activeShipIndex].SetActive(false);
        Goodships[activeShipIndex].SetActive(false);
        activeShipIndex = shipIndex;
        SetMember(member);
    }

    public void SetMember(string member)
    {
        if (member=="Evil")
        {
            Evilships[activeShipIndex].SetActive(true);
        }
        if (member=="Good")
        {
            Goodships[activeShipIndex].SetActive(true);
        }
       
        
    }
}
