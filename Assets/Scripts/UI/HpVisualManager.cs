using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpVisualManager : MonoBehaviour
{
    [SerializeField] private GameObject[] emptyHeartsPortrait;
    [SerializeField] private GameObject[] fullHeartsPortrait;

    [SerializeField] private GameObject[] emptyHeartsLandscape;
    [SerializeField] private GameObject[] fullHeartsLandscape;

    private void Awake()
    {
        if (emptyHeartsPortrait == null) Debug.LogError("No Empty Hearts Portrait");
        if (fullHeartsPortrait == null) Debug.LogError("No Full Hearts Portrait");
        if (emptyHeartsPortrait.Length != fullHeartsPortrait.Length) Debug.LogError("Not the same amount of full hearts to empty hearts Portrait");

        if (emptyHeartsLandscape == null) Debug.LogError("No Empty Hearts Landscape");
        if (fullHeartsLandscape == null) Debug.LogError("No Full Hearts Landscape");
        if (emptyHeartsLandscape.Length != fullHeartsLandscape.Length) Debug.LogError("Not the same amount of full hearts to empty hearts Landscape");
    }

    public void UpdateHpVisual(int hp)
    {
        if (hp < 0) return;
        SetHp(hp, fullHeartsPortrait, emptyHeartsPortrait);
        SetHp(hp, fullHeartsLandscape, emptyHeartsLandscape);
    }

    private void SetHp(int hp, GameObject[] fullHearts, GameObject[] emptyHearts)
    {
        bool value = true;
        for (int i = 0; i < emptyHearts.Length; i++)
        {
            if (i == hp) { value = !value; }

            fullHearts[i].SetActive(value);
            emptyHearts[i].SetActive(!value);
        }
    }

    
}
