using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    private static float Timer;
    public float Starting_Timer = 5f;
    public GameObject Bird;
    Transform[] spawn_point;
    static int Rand;
    void Start()
    {
        Timer = Starting_Timer;
        spawn_point = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawn_point[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.fixedDeltaTime;
        if (Timer < 0)
        {              
                Rand = Random.Range(0, transform.childCount);
                GameObject go = Instantiate(Bird, spawn_point[Rand].position, Quaternion.identity) as GameObject;
                Timer =Random.Range(Starting_Timer,Starting_Timer/4);          
        }
    }
   
}
