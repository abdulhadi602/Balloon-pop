using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Animate_Point : MonoBehaviour
{
    public TextMeshProUGUI Level;
    private void Start()
    {
        StartCoroutine(Move_Up(transform.position.y));
    }
    public void set_Color(Color col)
    {
        col.a = 1;
        Level.color = col;
       
    }
    public void Set_Points(string p)
    {
        Level.SetText(p);
    }
    IEnumerator Move_Up(float y)
    {
        Color alpha;
        alpha = Level.color;
        while (Level.color.a > 0)
        {
            alpha.a -= 0.009f;
            Level.color = alpha;
            transform.Translate(Vector3.up * 0.0098f);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
