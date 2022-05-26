using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private static readonly string ENEMY_TAG = "Enemy";

    float arrowKillTime = 10f;

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

        AttachAndPushTarget(other);
    }

    private void AttachAndPushTarget(Collider other)//Awful code
    {
        
        if (other.gameObject.CompareTag(ENEMY_TAG))
        {
            Transform rigTransform = other.gameObject.transform.Find("Rig").Find("root").Find("B-hips");

            gameObject.transform.SetParent(rigTransform);
        }
        else
        { gameObject.transform.SetParent(other.gameObject.transform); }
    }
}
