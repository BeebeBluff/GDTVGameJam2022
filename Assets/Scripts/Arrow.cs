using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float arrowKillTime = 10f;
    float arrowForce = 20f;

    Rigidbody myRigidBody;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        arrowKillTime -= Time.deltaTime;

        if (arrowKillTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) { return; } //avoid collision with the player

        Destroy(myRigidBody);
        Destroy(GetComponent<BoxCollider>()); //stop motion and future collisions

        gameObject.transform.SetParent(other.gameObject.transform);
        
        if(other.gameObject.GetComponent<Rigidbody>() != null)
        {
            Rigidbody collisionRigidbody = other.gameObject.GetComponent<Rigidbody>();

            collisionRigidbody.AddForce(transform.forward * arrowForce);
        }
    }

}
