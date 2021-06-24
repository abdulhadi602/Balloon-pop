using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_Float : MonoBehaviour
{
    // Start is called before the first frame update
    public float Float_Strength = 0.2f;
    public bool is_Floating;
    private int hitCounter;
    private GameObject Score;
    private GameObject BalloonSpawner;
    AudioSource[] audios;
    void Start()
    {
        Score = GameObject.FindGameObjectWithTag("Score");
        BalloonSpawner = GameObject.FindGameObjectWithTag("BalloonSpawner");
        audios = BalloonSpawner.GetComponents<AudioSource>();      
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
        /**  if (collision.tag.Equals("Ceiling"))
          {
            
          }**/
       
        //  Debug.Log("Floor");
        
            if (collision.tag.Equals("Floor"))
            { 
            hitCounter++;
                if (hitCounter > 1)
                {
                    Score.GetComponent<Score>().IncrecementScore(-5);
                    int Rand = Random.Range(0, 3);
                    audios[Rand].Play();
                    Destroy(this.gameObject);
                }
            }
            
        
      
        //  Debug.Log("Collision");
    }
   
}
