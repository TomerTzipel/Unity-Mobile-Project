using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Webcam : MonoBehaviour
{

    public RawImage displayImage;
    private WebCamTexture webCamTexture;

    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();

        displayImage.texture = webCamTexture;

        webCamTexture.Play();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        if(webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }
}
