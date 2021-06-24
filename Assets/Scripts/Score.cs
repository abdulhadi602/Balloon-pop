using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI score;
    public TextMeshProUGUI LevelPerecentage,Levelnumber,HighestScore;
    private static int sc;
    public GameObject GameManager,HealthSpawner,HealthLossEffect;
    private GameObject AdManager;
    AdManager ads;
    public float Speed, TMPsc;
    private static int health;
    public Shake cameraShake;

    private TextMeshProUGUI Level;
    private static int LevelCounter;
    public GameObject[] Spawners;
    private GameObject[] Deactivated_Spawners;
    private int Spawner_counter;
    public Slider LevelSlider;
    private Slider slidersc;
    private static int levelToShowAdOn;
    //Inorder to restart level we just need the score
    public static int LevelStartScore;
    int HighScore;
    public void SetScore(int score)
    {
        sc = score;
    }
    void Start()
    {
        levelToShowAdOn = 6;
        AdManager = GameObject.FindGameObjectWithTag("AdManager");
        ads = AdManager.GetComponent<AdManager>();
        slidersc = LevelSlider.GetComponent<Slider>();
        slidersc.value = 0;
        slidersc.maxValue = Speed;
        Spawner_counter = Spawners.Length-1;
        Deactivated_Spawners = new GameObject[Spawners.Length];
        Level = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        LevelStartScore = 0;
         health = HealthSpawner.transform.childCount;
          score = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.GetInt("Score_Marker") != 0)
        {
             HighScore = PlayerPrefs.GetInt("Score_Marker");       
            HighestScore.SetText("Highest Score: " + HighScore);
        }
        else
        {
            HighestScore.SetText("Highest Score: 0");
            PlayerPrefs.SetInt("Score_Marker", 0);
        }
        TMPsc = 0;
        LevelCounter = 1;
        LevelPerecentage.text = 0 + "%";
        Levelnumber.text = "Level " + LevelCounter;
    }

    public void IncrecementScore(int value)
    {
        if (sc > sc + value)
        {
            if (value == -5)
            {
              
                    Transform child = HealthSpawner.transform.GetChild(health);
                    // GameObject blood = GameObject.Instantiate(HealthLossEffect, child.position, Quaternion.identity);
                    // blood.transform.SetParent(HealthSpawner.transform);
                    //Destroy(child.gameObject);
                    child.gameObject.active = true;
                    health++;
              
                
            }
            else
            {
                if (HealthSpawner.transform.childCount > 0)
                {
                    Transform child = HealthSpawner.transform.GetChild(health-1);
                    GameObject blood = GameObject.Instantiate(HealthLossEffect, child.position, Quaternion.identity);
                    blood.transform.SetParent(HealthSpawner.transform);
                    //Destroy(child.gameObject);
                    child.gameObject.active = false;
                    health--;

                    if (health == 0)
                    {
                        GameManager.GetComponent<Manager>().EndGame();
                    
                    }
                }
                StartCoroutine(cameraShake.shake(0.25f, 0.5f));
            }
            value = 0;
            
        }
        else
        {
            TMPsc+=value;
            slidersc.value = TMPsc;
            int percentage = (int)(TMPsc / Speed * 100) ;
            if (percentage > 100)
            {
                percentage = 100;
            }
            LevelPerecentage.text =  percentage + "%";
          
         
        }

        sc += value;
        if (sc > HighScore)
        {
            HighScore = sc;
            HighestScore.SetText("Highest Score: " + HighScore);
            PlayerPrefs.SetInt("Score_Marker", HighScore);
        }

        score.SetText("" + sc);
        if (TMPsc >= Speed)
        {
            //change value of speed and whatever is being added below to change when the level will change
            Speed = Speed+3;
            TMPsc = 0;
            if (LevelCounter == 6)
            {
                ads.ShowAd();
            }else if (LevelCounter >6 && LevelCounter%3 ==0 && LevelCounter <= 10)
            {
                ads.ShowAd();
            }
            else if(LevelCounter>10 && LevelCounter%2 ==0)
            {
                ads.ShowAd();
            }



            StartCoroutine(LevelStart(LevelCounter));
            LevelCounter++;
        }
    }
    private IEnumerator LevelStart(int levelCounter)
    {
        LevelStartScore = sc;
        DeactivateSpawners();
        Level.text = "Level " + levelCounter + " Complete!";
        Color alpha;
        alpha = Level.color;
        while (Level.color.a < 1)
        {
            alpha.a += 0.009f;
            Level.color = alpha;
            yield return null;
        }
        while (Level.color.a > 0)
        {
            alpha.a -= 0.009f;
            Level.color = alpha;
            yield return null;
        }

        // BalloonSpawner.GetComponent<SpawnBalloon>().IncreaseSpeed();
        LevelPerecentage.text = 0 + "%";
        Levelnumber.text = "Level " + LevelCounter;
        slidersc.maxValue = Speed;
        slidersc.value = TMPsc;
        ReactivateSpawners();
    }
    private void DeactivateSpawners()
    {
        for(int i = 0; i < Spawners.Length; i++) 
        {
            if(Spawners[i].active == true)
            {
                Spawners[i].SetActive(false);
                Deactivated_Spawners[i] = Spawners[i];
            }
        }
      
        GameObject[] balloons = GameObject.FindGameObjectsWithTag("Balloon");
        foreach(GameObject balloon in balloons)
        {
            Destroy(balloon);
        }
    }
    private void ReactivateSpawners()
    {
        for (int i = 0; i < Deactivated_Spawners.Length; i++)
        {
            if (Deactivated_Spawners[i] != null && Deactivated_Spawners[i].active == false)
            {         
                Deactivated_Spawners[i].SetActive(true);
            }
        }
        if (LevelCounter % 3 == 0)
        {
            if (Spawner_counter >= 0)
            {
                Spawners[Spawner_counter].SetActive(true);
                Spawner_counter--;
                // BalloonSpawner2.SetActive(true);
                /** if (BalloonSpawner2.active == true)
                 {
                     BalloonSpawner2.GetComponent<SpawnBalloon>().IncreaseSpeed();
                 }**/
            }
        }
        
        foreach (GameObject spawn_Point in Spawners)
        {
            if (spawn_Point.active == true)
            {
                spawn_Point.GetComponent<SpawnBalloon>().IncreaseSpeed();
            }
        }
       
    }
    public void SetScoreToStartOfLevel()
    {
        sc = LevelStartScore;
        score.SetText("" + sc);

        TMPsc = 0;
        slidersc.value = 0;
        LevelPerecentage.text = "0%";
        ReplenishHealth();
        
    }
    public void ReplenishHealth() 
    {
        health = 0;
        foreach (Transform child in HealthSpawner.transform)
        {
            if (child.gameObject.tag == "HealthBarHeart")
            {
                child.gameObject.SetActive(true);
                health++;
            }
        }
    }
}
