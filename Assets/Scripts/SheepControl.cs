using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UIControl;
using UnityEngine;


public class SheepControl : MonoBehaviour
{
    [SerializeField] private Transform mainPos;
    [SerializeField] private float speed;
    public Animator animator;
    [SerializeField] private Animator textAnimator;
    private GameObject _gameObject;

    private ScoreControl _scoreControl;
    // Start is called before the first frame update
    void Start()
    {
        mainPos = GameObject.FindWithTag("Positions").GetComponent<Transform>();
        _scoreControl = GameObject.FindWithTag("GlobalScore").GetComponent<ScoreControl>();
        if (CompareTag("Friend")) animator.SetBool("Run",false);
        else
        {
            animator.SetBool("Run",true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareTag("Friend")) return;


        transform.position = Vector3.Slerp(transform.position, mainPos.position, speed * Time.deltaTime);
        transform.rotation=Quaternion.identity;
        //transform.position=Vector3.Lerp(transform.position,mainPos.position,speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            Destroy(other.gameObject);
            Destroy(gameObject);
            _scoreControl.RemoveFriendsCount();
        }

        if (other.CompareTag("Friend"))
        {
            other.GetComponent<SheepControl>().textAnimator.SetBool("isPointed", true);
            other.GetComponent<SheepControl>().animator.SetBool("Run", true);
            _gameObject = other.gameObject;
            other.transform.parent = mainPos;
            other.transform.rotation=Quaternion.identity*new Quaternion(0,-180,0,0);
            other.transform.tag = "Untagged";
            other.GetComponent<CapsuleCollider>().isTrigger = false;
            _scoreControl.AddFriendsCount();
            Invoke(nameof(PlayAnim),0.5f);
           // 
        }
        if (other.CompareTag("GateController"))
        {
            if (gameObject.GetComponent<SheepControl>().enabled == false) return;
          
            textAnimator.SetBool("isPointed", true);
            _gameObject = gameObject;
            _gameObject.transform.parent = mainPos;
            _gameObject.transform.rotation=Quaternion.identity*new Quaternion(0,-180,0,0);
            _gameObject.transform.tag = "Untagged";
            _gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
            _scoreControl.AddFriendsCount();
            Invoke(nameof(PlayAnim),0.5f);
            // 
        }
        
    }

    private void PlayAnim()
    {
        _gameObject.GetComponent<SheepControl>().textAnimator.SetBool("isPointed", false);
    }

   
}
