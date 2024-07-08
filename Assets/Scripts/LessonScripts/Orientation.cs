using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation : MonoBehaviour
{

    void Update()
    {
        DeviceOrientation orientation = Input.deviceOrientation;
        

        switch (orientation)
        {
            case DeviceOrientation.Unknown:
                Debug.Log("Unkown");
                break;
            case DeviceOrientation.Portrait:
                Debug.Log("Portrait");
                break;
            case DeviceOrientation.PortraitUpsideDown:
                Debug.Log("PortraitUpSideDown");
                break;
            case DeviceOrientation.LandscapeLeft:
                Debug.Log("LandscapeLeft");
                break;
            case DeviceOrientation.LandscapeRight:
                Debug.Log("LandscapeRight");
                break;
            case DeviceOrientation.FaceUp:
                Debug.Log("FaceUp");
                break;
            case DeviceOrientation.FaceDown:
                Debug.Log("FaceDown");
                break;
        }


    }
}
