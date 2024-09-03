using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRunUI : MonoBehaviour
{
    TransitionUI transitionUI;

    void Start()
    {
        transitionUI = GetComponent<TransitionUI>();
        transitionUI.TransitionEnd();
    }
}
