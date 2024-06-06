using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;

    private void FixedUpdate()
    {
        Vector3 move = speed * Time.fixedDeltaTime * Vector3.back;
        rb.MovePosition(transform.position + move);
    }
}
