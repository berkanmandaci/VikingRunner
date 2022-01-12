using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int reqExp;
    [SerializeField] private int expValue;
    [SerializeField] private Slider slider;
    [SerializeField] private AllyPool allyPool;
    public int TierLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = reqExp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExp(int exp)
    {
        expValue += exp;
        if (expValue>=reqExp)
        {
            TierLevel++;
            expValue = 0;
            allyPool.UpgradeAlly();
        }
        slider.value = expValue;
    }
}
