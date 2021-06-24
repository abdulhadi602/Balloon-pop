using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalloon : MonoBehaviour
{
    // Start is called before the first frame update
    private float Timer;
    public float Starting_Timer=5f;
    public float Increase_speed;
    public GameObject BalloonObject, GameManager,Heart;
    public GameObject[] Hearts;
    Transform[] spawn_point;
    static int Rand;
    private static Color balloon_clr;
    public float SpawnSpeedLimit;
   
    private GameObject go;
    public bool AllowOverPower;
    void Start()
    {
        Timer = Starting_Timer;
        spawn_point = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            spawn_point[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GetComponent<Manager>().gameOver)
        {
            Timer -= Time.fixedDeltaTime;
                if (Timer <= 0)
                {
           
                balloon_clr.a = 0.7f;
                balloon_clr.r = Random.Range(0, 1f);
                balloon_clr.g = Random.Range(0, 1f);
                balloon_clr.b = Random.Range(0, 1f);
                Rand = Random.Range(0, transform.childCount);
                go = Instantiate(BalloonObject, spawn_point[Rand].position, Quaternion.identity) as GameObject;
                SetPowers(go);
                go.transform.GetChild(0).GetComponent<SpriteRenderer>().color = balloon_clr;
                if (Hearts.Length>0 )
                {
                    int inactive_hearts = 0;
                    foreach (GameObject heart in Hearts)
                    {
                        if (heart.active == false)
                        {
                            inactive_hearts++;
                            int chance = Random.Range(0, 10);
                            if (chance >= 8)
                            {
                                GameObject[] SpawnedHeartPowers = GameObject.FindGameObjectsWithTag("Heart");
                                if (SpawnedHeartPowers.Length < inactive_hearts)
                                {
                                    if (Rand >0)
                                    {
                                        GameObject.Instantiate(Heart, spawn_point[Rand-1].position, Quaternion.identity);
                                    }
                                    else
                                    {
                                        GameObject.Instantiate(Heart, spawn_point[Rand + 1].position, Quaternion.identity);
                                    }
                                }
                                
                            }
                          
                            break;
                        }
                    }
                   
                }
               
              
                  
                
                Timer = Starting_Timer;
            }
            
        }
    /**    if (AllowOverPower)
        {
            GameObject[] BalloonCount = GameObject.FindGameObjectsWithTag("Balloon");
            int count = 0;
            int power=0;
            foreach (GameObject balloon in BalloonCount)
            {
                if (!balloon.GetComponent<Balloon_Float>().Get_isDanger())
                {
                    count++;
                }
                if (balloon.GetComponent<Balloon_Float>().SpecialPower_ClearScreen)
                {
                    power++;
                }
            }
            if (power == 0)
            {
                if (count > 4)
                {
                   
                   
                    try
                    {
                       
                            ForcePower("Clear_Screen", go);
                        
                    }
                    catch (MissingReferenceException e)
                    {
                        go = Instantiate(BalloonObject, spawn_point[Rand].position, Quaternion.identity) as GameObject;
                    
                            ForcePower("Clear_Screen", go);
                        
                    }
                }
            }
        }**/

    }

    private void ForcePower(string Power , GameObject balloon )
    {
        Balloon_Float floatsc = balloon.GetComponent<Balloon_Float>();
        
        if (Power.Equals("Clear_Screen"))
        {
           
                floatsc.isDanger = false;
                balloon.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                floatsc.SpecialPower_ClearScreen = true;
                balloon.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            
        }
        else if (Power.Equals("Danger"))
        {
            floatsc.isDanger = true;
            balloon.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            floatsc.SpecialPower_ClearScreen = false;
            balloon.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    private void SetPowers(GameObject balloon)
    {
        Balloon_Float floatsc = balloon.GetComponent<Balloon_Float>();
        int Rand = Random.RandomRange(0, 101);
        if (Rand > 95)
        {
            floatsc.isDanger = true;
            balloon.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            floatsc.isDanger = false;
            balloon.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            int Rand2 = Random.RandomRange(0, 20);
            if (Rand2 > 18)
            {
                floatsc.SpecialPower_ClearScreen = true;
                balloon.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                floatsc.SpecialPower_ClearScreen = false;
                balloon.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    public void IncreaseSpeed()
    {
        if (Starting_Timer > SpawnSpeedLimit)
        {
            Starting_Timer -= Increase_speed;
        }
    }
}
