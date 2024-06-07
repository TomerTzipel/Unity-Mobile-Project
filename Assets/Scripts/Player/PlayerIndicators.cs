using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicators : MonoBehaviour
{
    [SerializeField] private PlayerMovementSettings SO_playerMovementSettings;
    [SerializeField] private MeshRenderer Mrenderer;

    [SerializeField] private Material airborneMaterial;
    [SerializeField] private Material standingMaterial;

    // Update is called once per frame
    void Update()
    {
        if (SO_playerMovementSettings.State == PlayerState.Standing)
        {
            Mrenderer.material = standingMaterial;
        }

        if (SO_playerMovementSettings.State == PlayerState.Airborne)
        {
            Mrenderer.material = airborneMaterial;
        }
    }
}
