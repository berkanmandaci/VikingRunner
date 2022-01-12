using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int sceneCount;

    private void Start()
    {
        sceneCount = 20;
         if (SceneManager.GetActiveScene().buildIndex!=PlayerPrefs.GetInt("Level"))
         {
             ReplayLevel();
         }
    }

    public void NextLevel()
    {
        var levelIndex = PlayerPrefs.GetInt("Level");
        Debug.Log(levelIndex);
        if (levelIndex==0)
        {
            PlayerPrefs.SetInt("Level", 1);
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            if ((levelIndex/sceneCount)>=1)
            {
                SceneManager.LoadScene(levelIndex-sceneCount*(levelIndex/sceneCount));
            }
            else
            {
                SceneManager.LoadScene(levelIndex);
            }
            
        }
    }


    public void ReplayLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));     
    }
}
