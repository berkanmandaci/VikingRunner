using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllyPool : MonoBehaviour
{
    [SerializeField] private Transform allyPool;
    [SerializeField] private List<GameObject> ally;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private LevelController levelController;
    
    private GateController _gateController; 
    private Process _process;
    public int allyCount;
    
    private int floorCount = 0;
    private int firstFloor = 1;
    private int kalan;
    private float vectorz = 0;
    private float vectorx = 0;
    private Queue<GameObject> pyramidPool;
    private int layerCount;
    private int _kalan;
    public int index = 0;
    private bool pramid;
    
      
     private TextMeshPro pointText;
     private int _pointValue;

     private void Start()
     {
         pyramidPool = new Queue<GameObject>();
     }

     private void Update()
     {
         if (InputManager.isFinish&&!pramid)
         {
             Debug.Log("Piramit time");
             SetPyramid();
             pramid = true;
         }
     }

     private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Gate"))
        {
            _gateController = other.GetComponent<GateController>();
            _process = _gateController._processs;
            _pointValue = _gateController.pointValue;
            allyCount=transform.childCount;

            switch (_process)
            {
                case Process.Add:
                    
                    InstantiateCircle(_pointValue);
                    break;  
                case Process.Remove:  
                    DestroySheep(_pointValue);  
                    break;  
                case Process.Divide:  
                    InstantiateCircle(_pointValue);
                    break;  
                case Process.Multiply:
                    _pointValue = (allyCount * _pointValue) - allyCount;
                    
                    Debug.Log(_pointValue);
                    InstantiateCircle(_pointValue); 
                    break;  
                default:  
                    Console.WriteLine("Value didn’t match earlier.");  
                    break;  
            }
        }
    }

    public void InstantiateCircle (int pieceCount)
    {
        var levelIndex = levelController.TierLevel - 1;
        float angle = 360f / (float)pieceCount;
        for (int i = 0; i < pieceCount; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angle, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;
 
            Vector3 position = allyPool.position + (direction * 0.2f);
            Instantiate(ally[levelIndex], position, rotation,allyPool);
        }
        scoreController.AddAllyCount(pieceCount);
    }
    
    public void DestroySheep(int pieceCount)
    {
        if (allyPool.childCount < pieceCount) 
            gameManager.GameOver();
          
        for (int i = 0; i < pieceCount; i++)
            Destroy(allyPool.GetChild(i).gameObject);
        
        scoreController.RemoveAllyCount(pieceCount);
    }

    public void UpgradeAlly()
    {
        var allyCounter = transform.childCount;
        
        for (int i = 0; i < allyCounter; i++)
            Destroy(transform.GetChild(i).gameObject);
        

        InstantiateCircle(allyCounter);
        scoreController.RemoveAllyCount(allyCounter);
    }
    private void SetPyramid()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            
            transform.GetChild(i).GetComponent<CapsuleCollider>().isTrigger = true;
            pyramidPool.Enqueue(transform.GetChild(i).gameObject);
        }
        
        FloorCount();
        
        for (int i = 0; i <= floorCount; i++)
        {
            SetLayer();
        }
        
        var firstVectorx = vectorx;
        var firstVectory = vectorz;
        var firstFloorCount = floorCount+1;
        
        for (int i = 0; i < kalan; i++)
        {
            if (firstFloorCount == -1)
            {
                floorCount++;
                vectorz = firstVectory; //+2f
                vectorx = firstVectorx; //0.2f side by side
                firstFloorCount = floorCount;
            }
            vectorx += 0.6f;//0.2f side by side
            
            var obj = pyramidPool.Dequeue();
           
            obj.transform.parent = null;
            obj.transform.position = new Vector3(vectorx-1f, vectorz + transform.position.y,transform.position.z );
          //  obj.transform.position = new Vector3(vectorx-0.2f, 0, vectorz + transform.position.z+2); work
            Debug.Log(transform.position.z+2);
            vectorz -= 1.6f;
            firstFloorCount--;
        }
    }
    
    private void FloorCount()
    {
        var totalSheep = transform.childCount;

        index = 1;
        while (totalSheep > index)
        {
            firstFloor += 2;
            index += firstFloor;
            floorCount++;
        }
        index -= firstFloor;
        kalan = totalSheep-index;
        if (kalan!=0)
        {
            floorCount--;
            
            firstFloor += -2;
        }
    }
    private void SetLayer()
    {
        
        vectorx = (firstFloor / 2) * -0.6f;//0.2f 
        for (int j = 0; j < firstFloor; j++)
        {
            vectorx += 0.6f;// side by side
            var obj = pyramidPool.Dequeue();
           
            obj.transform.parent = null;
            obj.transform.position = new Vector3(vectorx-0.6f, vectorz + transform.position.y,transform.position.z );
           // obj.transform.position = new Vector3(vectorx-0.2f, 0, vectorz + transform.position.z+2);
        }
        vectorz += 1.6f;
        firstFloor += -2;
        
    }
}
