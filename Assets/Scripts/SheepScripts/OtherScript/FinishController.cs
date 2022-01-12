using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIControl;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class FinishController : MonoBehaviour
{
    
    private HpBarControl _shipLevelBar;
    private int _levelBarValue;
    private bool _finishAction;
    private GameObject _player, _canvas, _camera;
    public PlayableDirector playableDirector;
    private ScoreControl _scoreControl;
    public bool gameOver;
    private bool finish;
    public int speed;
    private int dis;
    private float _scoreMultiplier;
    private HpBarControl _hpLevelBar;
    public int index = 0;
    private Transform mainPos;
    private int floorCount = 0;
    private int firstFloor = 1;
    private int kalan;
    private float vectorz = 0;
    private float vectorx = 0;
    private Queue<GameObject> pyramidPool;
    private int layerCount;
    private int _kalan;

    private void Start()
    {
        pyramidPool = new Queue<GameObject>();
        finish = false;
        _shipLevelBar=GameObject.FindWithTag("ShipLevelBar").GetComponent<HpBarControl>();
        _hpLevelBar =GameObject.FindWithTag("HpBar").GetComponent<HpBarControl>(); 
        _scoreControl = GameObject.FindWithTag("GlobalScore").GetComponent<ScoreControl>(); 
        _player = GameObject.FindGameObjectWithTag("Player"); 
        _canvas = GameObject.FindGameObjectWithTag("Canvas"); 
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        playableDirector=GameObject.FindWithTag("CameraFollow").GetComponent<PlayableDirector>();
        mainPos = GameObject.FindWithTag("Positions").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (!_finishAction) return;
        _levelBarValue--;
        _shipLevelBar.SliderValueChange();
        if (_levelBarValue==0)
        {
            _finishAction = false;
        }
    }

    private int FinishDistance()
    {
        var comboCount = _hpLevelBar.ComboIndex();
        int stepCount = ((_levelBarValue+(comboCount*2)) / 5); 
        _scoreMultiplier = ((stepCount * 0.2f) + 1);
        int distance = (stepCount * 2) + 1;
       // _player.GetComponent<Move>().started = false;
        return distance;

    }

    private void Move(int stepCount)//finishten sonra step sayısı kadar z arttırılıyor.
    {
        
        if (finish) return;
        if (_player.transform.position.z>transform.position.z+ stepCount+2.5f)
        {
          //  _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + speed * Time.deltaTime);
          finish = true;
          Invoke(nameof(Finish),3f);
        }
       
    }
    

    private void Update()
    {
        
        if (!gameOver) return;
        
        Move(layerCount*2); 
        
         // if (playableDirector.time>4.82f&& gameOver)
         // {
         //     Finish();
         //     gameOver = false; 
         // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GateController"))
        {
            gameOver = true;
            _levelBarValue = _shipLevelBar.hpValue;
            _finishAction = true;
            dis = FinishDistance();
            playableDirector.Play();
           // SetPyramid();
         //  SetSquare();
           StartCoroutine(SetSquare());
        }
    }

    private void Finish()
    {
        _canvas.transform.Find("Play").gameObject.SetActive(false);
        _canvas.transform.Find("Move Box").gameObject.SetActive(false);
        _canvas.transform.Find("Finish").gameObject.SetActive(true);
        _canvas.transform.Find("GameScoreArea").gameObject.SetActive(false);
        _canvas.transform.Find("ShipLevelSlider").gameObject.SetActive(false);
        _canvas.transform.Find("HpBarSlider").gameObject.SetActive(false);
        _canvas.transform.Find("ComboTMP").gameObject.SetActive(false);
        _canvas.transform.Find("Jolly Roger").gameObject.SetActive(false);
        _canvas.transform.Find("Union Jack").gameObject.SetActive(false);
        _scoreControl.SetFinishScore(_scoreMultiplier);
        PlayerPrefs.SetInt("Level", (PlayerPrefs.GetInt("Level")+1));
        _camera.GetComponent<AudioSource>().Stop();
    }
    private void SetLayer()
    {
        
        vectorx = (firstFloor / 2) * -0.2f;
        for (int j = 0; j < firstFloor; j++)
        {
            vectorx += 0.2f;
            var obj = pyramidPool.Dequeue();
            obj.GetComponent<SheepControl>().enabled = false;
            obj.transform.parent = null;
            obj.transform.position = new Vector3(vectorx-0.2f, 0, vectorz + transform.position.z+2);
        }
        vectorz += 2f;
        firstFloor += -2;
        
    }

    private void SetPyramid()
    {
        
        for (int i = 0; i < mainPos.childCount; i++)
        {
            mainPos.GetChild(i).GetComponent<CapsuleCollider>().isTrigger = true;
            pyramidPool.Enqueue(mainPos.GetChild(i).gameObject);
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
                vectorz = firstVectory+2f;
                vectorx = firstVectorx+0.2f;
                firstFloorCount = floorCount;
            }
            vectorx += 0.2f;
            
            var obj = pyramidPool.Dequeue();
            var _sheepControl = obj.GetComponent<SheepControl>();
            _sheepControl.enabled = false;
            _sheepControl.animator.SetBool("Run", false);
            obj.transform.parent = null;
            obj.transform.position = new Vector3(vectorx-0.2f, 0, vectorz + transform.position.z+2);
            Debug.Log(transform.position.z+2);
            vectorz -= 2f;
            firstFloorCount--;
        }
    }

    private void FloorCount()
    {
        var totalSheep = _scoreControl.friendsCount;

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
    
    private void SetOrder()
    {
        var totalSheep = _scoreControl.friendsCount; 
        layerCount = totalSheep / 3;
        _kalan = totalSheep % 3;
        if (kalan!=0)layerCount++;
    }

    IEnumerator SetSquare()
    {
        for (int i = 0; i < mainPos.childCount; i++)
        {
            pyramidPool.Enqueue(mainPos.GetChild(i).gameObject);
        }
        SetOrder();
        var counter = 3;
        for (int i = 0; i <= layerCount; i++)
        {
            if (i == layerCount) counter = _kalan;
            var vectorxx = -0.2f;
            for (int j = 0; j < counter; j++)
            {
                var obj = pyramidPool.Dequeue();
                obj.tag = "Friend";
                var _sheepControl = obj.GetComponent<SheepControl>();
                _sheepControl.enabled = false;
                _sheepControl.animator.SetBool("Run", false);
                obj.transform.parent = null;
                obj.transform.position = new Vector3(vectorx-0.2f, 0, vectorz + transform.position.z+2);
                vectorxx += 0.2f;
            }
            vectorz += 2;
            yield return new WaitForSeconds(0.4f);
        }
    }
    
}
