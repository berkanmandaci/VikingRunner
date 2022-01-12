using System;
using TMPro;
using UnityEngine;

namespace UIControl
{
    public class ScoreControl : MonoBehaviour
    {
         private int _score;
         [SerializeField] private TextMeshProUGUI _scoreTMP;
         [SerializeField] private TextMeshProUGUI _gameScoreTMP;
         [SerializeField] private Transform _sheepPool;
         private int point;
         public int friendsCount;
         [SerializeField] private GameObject sheep;
         [SerializeField] private TextMeshPro _sheepCount;

         private void Start()
        {
            friendsCount = 0;
            _score = 0;
            point = PlayerPrefs.GetInt("Score");
            _gameScoreTMP.text = point.ToString();
        }

         public void RemoveFriendsCount()
         {
             friendsCount--;
             
         }
         public void AddFriendsCount()
         {
             friendsCount++;
         }
        public void SetScoreValue(int hpIndex)
        {
            _score += hpIndex;
        }

        public void AddFriendsCount(int count)
        {
            friendsCount += count;
        }
        public void RemoveFriendsCount(int count)
        {
            if (count>friendsCount)
            {
                friendsCount = 0;
            }
            else
            {
                friendsCount -= count;
            }
        }
        public void MultiplyFriendsCount(int count)
        {
          
            var result = friendsCount;
            friendsCount *= count;
            result = friendsCount-result;
            InstantiateCircle(result);
        }
        public void DivideFriendsCount(int count)
        {
            var result = friendsCount;
            friendsCount /= count;
            result -= friendsCount;
            DestroySheep(result);
        }

        public void SetFinishScore(float multiple)
        {
            point = (int)(_score*multiple) + point;
            _scoreTMP = GameObject.FindWithTag("GameScore").GetComponent<TextMeshProUGUI>();
            _scoreTMP.text = "+"+((int)(_score*multiple)).ToString();
            PlayerPrefs.SetInt("Score", point);
        }

        private void Update()
        {
            _sheepCount.text = friendsCount.ToString();
        }


        public void InstantiateCircle (int pieceCount) 
        {
            Debug.Log("instantiate");
            float angle = 360f / (float)pieceCount;
            for (int i = 0; i < pieceCount; i++)
            {
                Quaternion rotation = Quaternion.AngleAxis(i * angle, Vector3.up);
                Vector3 direction = rotation * Vector3.forward;
 
                Vector3 position = _sheepPool.position + (direction * 0.001f);
                Instantiate(sheep, position, rotation,_sheepPool);
            }
        }

        public void DestroySheep(int pieceCount)
        {
            if (_sheepPool.childCount < pieceCount) pieceCount = _sheepPool.childCount;
          
            for (int i = 0; i < pieceCount; i++)
            {
                Destroy(_sheepPool.GetChild(i).gameObject);
            }
        }
    }
}
