using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform GameOver,Pause;
    public bool gameOver;
    public GameObject Begin,Score,Homer, Tint, Settings;
    private bool settingsIsOpen,paused;
    private GameObject AdManager;
    AdManager ads;
    private Score scoreSC;
    void Start()
    {
        AdManager = GameObject.FindGameObjectWithTag("AdManager");
        ads = AdManager.GetComponent<AdManager>();
        scoreSC = Score.GetComponent<Score>();
        GameOver.gameObject.SetActive(false);
        gameOver = true;
        settingsIsOpen = false;
        paused = false;
        Begin.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!settingsIsOpen)
            {
                PauseGame();
            }
        }
    }

    public void Start_Game()
    {
        Time.timeScale = 1;
        gameOver = false;
        Begin.SetActive(false);
    }

    public void PauseGame()
    {
        if (!Begin.active && !GameOver.gameObject.active)
        {
            paused = true;
            Time.timeScale = 0;
            gameOver = true;
            Pause.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        gameOver = false;
        Pause.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        GameObject[] Balloons = GameObject.FindGameObjectsWithTag("Balloon");
        foreach(GameObject balloon in Balloons)
        {
            Destroy(balloon);
        }
        GameObject[] Hearts = GameObject.FindGameObjectsWithTag("Heart");
        foreach (GameObject Heart in Hearts)
        {
            Destroy(Heart);
        }
        gameOver = true;
        GameOver.gameObject.SetActive(true);
    }
    public void RestartLevel()
    {
        ads.IncreaseRetryCount();
        gameOver = false;
        GameOver.gameObject.SetActive(false);
        scoreSC.SetScoreToStartOfLevel();
        scoreSC.SetScore(0);
        Time.timeScale = 1;
       
    }
    public void ContinueLevel()
    {
        gameOver = false;
        GameOver.gameObject.SetActive(false);
        scoreSC.ReplenishHealth();
        Time.timeScale = 1;
       
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ApplicationExit()
    {
        Application.Quit();
    }
    public void RemoveHomer()
    {
        Homer.SetActive(!Homer.activeSelf);
    }
    public void RemoveTint()
    {
        Tint.SetActive(!Tint.activeSelf);
    }
    public void settingActive()
    {
        if (!Begin.active && !GameOver.gameObject.active)
        {
            if (!paused)
            {
                settingsIsOpen = !settingsIsOpen;

                Settings.SetActive(!Settings.activeSelf);
                if (settingsIsOpen)
                {
                    gameOver = true;
                    Time.timeScale = 0;
                }
                else
                {
                    gameOver = false;
                    Time.timeScale = 1;
                }
            }
        }
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://ahadi3608.wixsite.com/kittython");
    }
}
