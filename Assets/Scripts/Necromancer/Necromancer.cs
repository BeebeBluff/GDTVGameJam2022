using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    private static readonly string PLAYER_ARROW_STRING = "PlayerArrow";

    private void Start()
    {
        SetLimbPhysics(true);

        FindObjectOfType<LevelManager>().NecroDead(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_ARROW_STRING))
        {
            Die(other);
        }
    }

    private void Die(Collider other)
    {
        gameObject.name = "DeadNecromancer"; //Do this so spawner's won't find it.

        FindObjectOfType<LevelManager>().NecroDead(false);

        GetComponent<Animator>().enabled = false;
        SetLimbPhysics(false);
        CreateForceExplosion(other);
    }

    private void SetLimbPhysics(bool isAlive)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Rigidbody rigidbody in rigidbodies) //True if alive
        { rigidbody.isKinematic = isAlive; } //Kinematic means not affected by gravity

        foreach (Collider collider in colliders)
        { collider.enabled = !isAlive; } // False if alive

        GetComponent<Collider>().enabled = isAlive;
    }

    private void CreateForceExplosion(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(other.gameObject.transform.position, 1f);

        foreach (Collider nearbyObjects in colliders)
        {
            Rigidbody rigidbody = nearbyObjects.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(1500f, other.gameObject.transform.position, 1.5f);
            }
        }
    }
}
