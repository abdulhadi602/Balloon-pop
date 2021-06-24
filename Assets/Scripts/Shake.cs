using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shake : MonoBehaviour
{
    public TextMeshProUGUI score;
    Color red = Color.red;
    Color temp;
    Vector3 originalPos = new Vector3(0, 0, -10);
    public IEnumerator shake(float duration, float magnitude)
    {
        temp = Color.black;
        temp.a = 0.5f;
      

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            score.color = red;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
         score.color = temp;
    
        transform.localPosition = originalPos;
    }
}
