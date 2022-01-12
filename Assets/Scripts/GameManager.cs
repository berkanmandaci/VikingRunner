using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject inGameAndMenuUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject gameOverUI;
    
    
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
        InputManager.isFinish = false;
        InputManager.isStart = true;
        inGameUI.SetActive(true);
        menuUI.SetActive(false);
        InputManager.isMenu = false;
        InputManager.isFight = false;
        
    }

    public void FinishGame()
    {
        InputManager.isFinish = true;
        winUI.SetActive(true);
        InputManager.isStart = false;
        inGameUI.SetActive(false);
        InputManager.isFight = false;
        InputManager.isFinishAction = false;
    }

    public void MainMenu()
    {
        InputManager.isFinish = false;
        InputManager.isMenu = true;
        Application.LoadLevel(Application.loadedLevel);
        // menuUI.SetActive(true);
        // inGameAndMenuUI.SetActive(true);
        // InputManager.isFinish = false;
        // winUI.SetActive(false);
    }

    public void GameOver()
    {
        InputManager.isFinishAction = false;
        InputManager.isGameOver = true;
        gameOverUI.SetActive(true);
        InputManager.isStart = false;
        inGameUI.SetActive(false);
        InputManager.isDefence = false;
        InputManager.isFight = false;

    }
}
