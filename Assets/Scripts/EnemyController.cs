using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Animator animator;
    [SerializeField] private float arrowFireRange;
    [SerializeField] private float arrowFireRate;
    [SerializeField] private float arrowFireSpeed;
    
    private EnemyPoolController enemyPoolController;
    private bool fire;
    
    
    
    private GameObject player;
    
    // Start is called before the first frame update
    private void Awake()
    {
        player=GameObject.FindWithTag("Positions");
    }

    void Start()
    {
        DOTween.Init();
        
        enemyPoolController = transform.parent.GetComponent<EnemyPoolController>();
        InvokeRepeating("Fire",0f,arrowFireRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        transform.position = Vector3.Slerp(transform.position, transform.parent.position, 2f * Time.deltaTime);
      
        float dist = Vector3.Distance(transform.position,player.transform.position);
        if (dist<arrowFireRange) fire = true;
        
    }

    private void Fire()
    {
        if (fire && !InputManager.isFight)
        {
            animator.SetBool("Fire",true);
            var InstantiateTarget = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

            var newArrow = Instantiate(arrow, InstantiateTarget, (Quaternion.identity));
            var Target = new Vector3(player.transform.position.x, player.transform.position.y + 1f,
                player.transform.position.z);

            newArrow.transform.DOMove(Target, arrowFireSpeed);
        }
        else
        {
            animator.SetBool("Fire",false);
        }
        
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ally"))
        {
            Destroy(gameObject);
        }
    }
}
