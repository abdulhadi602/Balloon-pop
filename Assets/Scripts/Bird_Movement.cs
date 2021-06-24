using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Movement : MonoBehaviour
{
    private static float Timer;
    public float Starting_Timer = 5f;
    // Start is called before the first frame update
    public float MovementSpeed = 3;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * MovementSpeed * Time.deltaTime, Space.World);
        Timer -= Time.fixedDeltaTime;
        if (Timer < 0)
        {
            Destroy(gameObject);
            Timer = Random.Range(Starting_Timer, Starting_Timer / 4);
        }
    }
}
