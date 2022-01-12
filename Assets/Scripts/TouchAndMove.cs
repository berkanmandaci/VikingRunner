using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchAndMove : MonoBehaviour, IDragHandler
{
    public float constX;
    public bool slideDown;
    
    private Touch touch;
    public float vitezaModificare;
    
    //private StartController _canvas;
    private RectTransform rectTransform;
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    
    [SerializeField] private RawImage touchObject;
    [SerializeField] private Animator animator;
  //  [SerializeField] private Animator animatorAxe;



    void Start()
    {
        rectTransform= GetComponent<RectTransform>(); 
        rectTransform.anchoredPosition=Vector2.zero;
        // _canvas = GameObject.FindWithTag("Canvas").GetComponent<StartController>();
        
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (!InputManager.isStart|| InputManager.isFight) return;
        
     //   animatorAxe.SetBool("Run",true);
        
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + eventData.delta.x, rectTransform.anchoredPosition.y );
       
        float posX = rectTransform.anchoredPosition.x / constX;
        
        if (posX >= 2.5f)
        {
            posX = 2.5f;
        }
        
        if(posX <= -2.5f)
        {
            posX = -2.5f;
        }
        
        player.transform.position = new Vector3(posX, player.transform.position.y, player.transform.position.z);
    }

    private void Update()
    {
        if (!InputManager.isStart|| InputManager.isFight) return;
        if ( InputManager.isFinish) return;
        
        //animator.SetBool("Run",true);
        
        var newSpeed=  InputManager.isDefence ? speed/2 : speed;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + newSpeed * Time.deltaTime);
    }
}
