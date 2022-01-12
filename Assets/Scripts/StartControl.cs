using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartControl : MonoBehaviour
{
    
    [SerializeField] private GameObject ınGameUI;
    [SerializeField] private GameObject menuUI;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Click Button
    public void StartGame()
    {
        menuUI.SetActive(false);
        ınGameUI.SetActive(true);
        InputManager.isMenu = false;
        InputManager.isStart = true;
    }
}
