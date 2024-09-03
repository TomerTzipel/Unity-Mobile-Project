using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{

    //private float elapsedTime = 0f;
    private float duration = 5f;
    private Vector3 startScale = Vector3.one;
    private Vector3 targetScale = new Vector3(5f, 5f, 5f);
    //bool isGrowing = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(transform.localScale == targetScale)
        {
            transform.DOScale(startScale, duration);
            /*isGrowing = false;
            elapsedTime = 0;*/
        }

        if (transform.localScale == Vector3.one)
        {
            transform.DOScale(targetScale, duration);
            /*isGrowing = true;
            elapsedTime = 0;*/
        }

        /*
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / 5f;

        if (isGrowing)
        {

            //transform.localScale = Vector3.Lerp(startScale, targetScale, t);
        }
        else
        {
            //transform.localScale = Vector3.Lerp(targetScale, startScale, t);
        }*/




    }
}
