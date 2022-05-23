using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float arrowKillTime = 10f;
    Rigidbody myRigidBody;

    private void Update()
    {
        arrowKillTime -= Time.deltaTime;

        if (arrowKillTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
