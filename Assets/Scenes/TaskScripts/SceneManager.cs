using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] float minDistance;
    [SerializeField] RawImage _image;

    private bool _isPinchInProgress = false;
    private bool _isRotateInProgress = false;

    Vector2 initialTouch0Position;
    Vector2 initialTouch1Position;

    Vector2 lastTouch0Position;
    Vector2 lastTouch1Position;
    private float zoomValue = 100f; 

    float LastPositionsDistance
    {
        get { return Vector2.Distance(lastTouch0Position, lastTouch1Position); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount != 2) return;

        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        if (touch0.phase == TouchPhase.Began)
        {
            initialTouch0Position = touch0.position;
            lastTouch0Position = initialTouch0Position;
        }

        if (touch1.phase == TouchPhase.Began)
        {
            initialTouch1Position = touch1.position; 
            lastTouch1Position = initialTouch1Position;
        }

        if(touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
        {
            HandlePinch(touch0,touch1);
        }

        if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Canceled)
        {
            //zoomValue = 100f;
        }


        HandleRotate(touch0, touch1);
    }

    private void HandlePinch(Touch touch0, Touch touch1)
    {
        float currentDistance = Vector2.Distance(touch0.position, touch1.position);
         
        if(currentDistance - LastPositionsDistance < -minDistance )
        {
            zoomValue = (currentDistance/Vector2.Distance(initialTouch0Position, initialTouch1Position)) * 100f;
            ZoomIn(); 
            lastTouch0Position = touch0.position;
            lastTouch1Position = touch1.position;
        }

        if (currentDistance - LastPositionsDistance > minDistance)
        {
            zoomValue = (currentDistance / Vector2.Distance(initialTouch0Position, initialTouch1Position)) * 100f;
            ZoomOut(); 
            lastTouch0Position = touch0.position;
            lastTouch1Position = touch1.position;
        }
    }

    private void HandleRotate(Touch touch0, Touch touch1)
    {

    }

    private void ZoomIn()
    {
        _image.transform.localScale = new Vector3(0.01f * zoomValue, 0.01f * zoomValue,1f);
        Debug.Log("ZoomIn" + zoomValue);
       
    }

    private void ZoomOut()
    {
        _image.transform.localScale = new Vector3(0.01f * zoomValue, 0.01f * zoomValue, 1f);
        Debug.Log("ZoomOut" + zoomValue);
    }
}
