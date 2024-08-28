using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionUI : MonoBehaviour
{
    private Animator animator;
    public bool IsStartScene;
    [SerializeField] Image blackScreen;

    private void Awake()
    {
        if (IsStartScene)
        {
            blackScreen.gameObject.SetActive(true);
        }
        else
        {
            blackScreen.gameObject.SetActive(false);
        }

        animator = GetComponent<Animator>();
    }

    public void TransitionStart()
    {
        if (animator != null)
        {
            animator.SetTrigger("Close");
        }
        else
        {
            Debug.LogError("Animator not assigned!");
        }
    }

    public void TransitionEnd()
    {
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            Debug.LogError("Animator not assigned!");
        }
    }

    public void OnCloseAnimationEnd()
    {
        blackScreen.gameObject.SetActive(true);
    }

    public void OnOpenAnimationEnd()
    {
        blackScreen.gameObject.SetActive(false);
    }
}
