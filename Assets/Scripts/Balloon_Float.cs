using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Float : MonoBehaviour
{
    // Start is called before the first frame update
    public float Float_Strength = 0.2f;
    public bool is_Floating,isSpecial;
    public GameObject Boom,SpecialPower_anim;
    private int hitCounter;
    public bool isDanger;
    public bool SpecialPower_ClearScreen;
    private GameObject Score;
    private GameObject BalloonSpawner;
    AudioSource[] audios;
    
    void Start()
    {
        BalloonSpawner = GameObject.FindGameObjectWithTag("BalloonSpawner");
        audios = BalloonSpawner.GetComponents<AudioSource>();
        Score = GameObject.FindGameObjectWithTag("Score");
        is_Floating = true;
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Floating)
        {
            transform.Translate(Vector2.up * Float_Strength * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Ceiling"))
        {
            // Debug.Log("Ceiling");
            Destroy(this.gameObject);
            if (!isDanger)
            {
                Score.GetComponent<Score>().IncrecementScore(-1);
                audios[3].Play();
            }
        
                
            
        
        }else if (collision.tag.Equals("Floor"))
        {
            hitCounter++;
            //  Debug.Log("Floor");
            if (hitCounter > 1)
            {
                if (isDanger)
                {
                    GameObject.Instantiate(Boom, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.Euler(-130, 90, -90));
                    Score.GetComponent<Score>().IncrecementScore(-1);
                    audios[3].Play();
                }
                else {
                    int Rand = Random.Range(0, 3);
                    GameObject point = Instantiate(SpecialPower_anim, transform.position, Quaternion.identity) as GameObject;
                    point.GetComponent<Animate_Point>().set_Color(transform.GetChild(0).GetComponent<SpriteRenderer>().color);
                    audios[Rand].Play();
                    if (SpecialPower_ClearScreen)
                    {
                       /** GameObject[] Balloons = GameObject.FindGameObjectsWithTag("Balloon");
                        foreach (GameObject balloon in Balloons)
                        {
                            if (!balloon.GetComponent<Balloon_Float>().Get_isDanger())
                            {

                                Destroy(balloon);
                               // Score.GetComponent<Score>().IncrecementScore(1);
                            }
                        }**/
                        Score.GetComponent<Score>().IncrecementScore(5);
                        point.GetComponent<Animate_Point>().Set_Points("+5");
                    }
                  else
                     {
                        Score.GetComponent<Score>().IncrecementScore(1);
                        
                        point.GetComponent<Animate_Point>().Set_Points("+1");
                    } 
                }
                Destroy(this.gameObject);
               
            }
            
        }
      //  Debug.Log("Collision");
    }
    public bool Get_isDanger()
    {
        return isDanger;
    }
}
