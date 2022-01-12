using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool checkDefence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.isDefence&&!InputManager.isFight)
        {
            animator.SetBool("Defence",true);
        }
        else
        {
            animator.SetBool("Defence",false);
        }

        if (InputManager.isFight)
        {
            //TODO attack
            animator.SetBool("Attack",true);
        }
        else
        {
            animator.SetBool("Attack",false);
        }

        // if (InputManager.isFinish&& InputManager.isFinishAction)
        // {
        //     animator.Play("Dance");
        // }
      
        
    }
}
