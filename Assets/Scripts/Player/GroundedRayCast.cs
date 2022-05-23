using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedRayCast : MonoBehaviour
{
    private static readonly Vector3 RAY_1 = new Vector3(0, -1, 0);
    private static readonly Vector3 RAY_2 = new Vector3(.5f, -1, .5f);
    private static readonly Vector3 RAY_3 = new Vector3(-.5f, -1, -.5f);
    private static readonly Vector3 RAY_4 = new Vector3(.5f, -1, -.5f);
    private static readonly Vector3 RAY_5 = new Vector3(-.5f, -1, .5f);

    [SerializeField] private float rayLength;

    public bool IsGrounded { get; private set; }

    void Update()
    {
        Vector3 position = transform.position;
        position.y += .5f;

        Debug.DrawRay(position, RAY_1 * rayLength, Color.red);
        Debug.DrawRay(position, RAY_2 * rayLength, Color.green);
        Debug.DrawRay(position, RAY_3 * rayLength, Color.blue);
        Debug.DrawRay(position, RAY_4 * rayLength, Color.yellow);
        Debug.DrawRay(position, RAY_5 * rayLength, Color.magenta);

        // For now any hit is grounded
        IsGrounded = Physics.Raycast(position, RAY_1, out _, rayLength)
            || Physics.Raycast(position, RAY_2, out _, rayLength)
            || Physics.Raycast(position, RAY_3, out _, rayLength)
            || Physics.Raycast(position, RAY_4, out _, rayLength)
            || Physics.Raycast(position, RAY_5, out _, rayLength);
    }
}
