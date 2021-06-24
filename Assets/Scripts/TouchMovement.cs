using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    public bool isHeart;
    private void Update()
    {
        if (isBeingHeld)
        {
            if (!isHeart)
            {
                GetComponent<Balloon_Float>().is_Floating = false;
            }
            else
            {
                GetComponent<Heart_Float>().is_Floating = false;
            }
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);

        }
        else
        {
            if (!isHeart)
            {
                GetComponent<Balloon_Float>().is_Floating = true;
            }
            else
            {
                GetComponent<Heart_Float>().is_Floating = true;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {     
            isBeingHeld = true;
        }

    }

    private void OnMouseUp()
    {
        isBeingHeld = false; 
    }
}
