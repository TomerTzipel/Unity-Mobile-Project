using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationManager : MonoBehaviour
{
    [SerializeField] private GameObject landscapePanel;
    [SerializeField] private GameObject portraitPanel;

    private DeviceOrientation _orientation;


    void Awake()
    {
        OnOrientationChange();
    }

    void Update()
    {
        if (_orientation != Input.deviceOrientation) OnOrientationChange();
    }

    private void OnOrientationChange()
    {
        _orientation = Input.deviceOrientation;

        if(_orientation == DeviceOrientation.Portrait || _orientation == DeviceOrientation.PortraitUpsideDown)
        {
            landscapePanel.SetActive(false);
            portraitPanel.SetActive(true);
            return;
        }

        landscapePanel.SetActive(true);
        portraitPanel.SetActive(false);
    }
}
