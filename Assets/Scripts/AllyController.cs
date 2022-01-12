using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private ScoreController scoreController;
    private bool arrowTrigger;
    private Transform enemyPoolPos;
    private bool myFinishAction=true;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreController=  GameObject.FindWithTag("ScoreController").GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }

    //Arrow hit Ally
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Step"))
        {
            Animator.Play("Dance");
            transform.rotation = new Quaternion(0, 180, 0, 1);
            myFinishAction = false;
        }
        if (!other.CompareTag("Arrow")) return;
        arrowTrigger = true;
        Destroy(other.gameObject);
        if (InputManager.isDefence) return;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!arrowTrigger) return;

        scoreController.RemoveAllyCount(1);
    }

    private void Move()
    {
        if (InputManager.isFinish&& myFinishAction)
        {
            Debug.Log("Calısmamamalıııı");
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10 * Time.deltaTime);
        }
        
        Debug.Log(InputManager.isFinish);
        if (!InputManager.isStart|| InputManager.isFinish) return;
        
        Animator.SetBool("Run",true);
        transform.position = Vector3.Slerp(transform.position, transform.parent.position, 0.5f * Time.deltaTime);
        transform.rotation=Quaternion.identity;
        
       
    }

    private void CheckDestroy()
    {
        
    }
}
