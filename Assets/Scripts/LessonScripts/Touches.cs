using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touches : MonoBehaviour
{
    Vector2 initialPos;
    float startTime;
    bool isSwipping = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            Touch touch0 = Input.GetTouch(0);

            if(touch0.phase == TouchPhase.Began)
            {
                initialPos = touch0.position;
                startTime = Time.time;
                isSwipping = true;
            }
            else if(touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled)
            {
                if (isSwipping)
                {
                    Vector2 currPos = touch0.position;
                    float endTime = Time.time;

                    if(currPos.x > initialPos.x)
                    {
                        Debug.Log("Right");
                    }
                    if (currPos.x < initialPos.x)
                    {
                        Debug.Log("Left");
                    }

                    if (currPos.y > initialPos.y)
                    {
                        Debug.Log("Up");
                    }
                    if (currPos.y < initialPos.y)
                    {
                        Debug.Log("Down");
                    }

                    if(endTime - startTime > 2)
                    {
                        Debug.Log("fail");
                    }

                }

                isSwipping = false;
            }
        }
    }
}
