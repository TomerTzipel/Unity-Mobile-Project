using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapElementMovement : MonoBehaviour
{
    [SerializeField] private MapSettings SO_MapSettings;
    [SerializeField] private Rigidbody rb;

    private void FixedUpdate()
    {
        Vector3 move = SO_MapSettings.TilesSpeed * Time.fixedDeltaTime * Vector3.back;
        rb.MovePosition(transform.position + move);
    }
}
