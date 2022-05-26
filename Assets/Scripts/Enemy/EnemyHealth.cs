using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private static readonly string PLAYER_ARROW_STRING = "PlayerArrow";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_ARROW_STRING))
        { 
            Die();

            Collider[] colliders = Physics.OverlapSphere(other.gameObject.transform.position, 1f);

            foreach (Collider nearbyObjects in colliders)
            {
                Rigidbody rigidbody = nearbyObjects.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(1500f, other.gameObject.transform.position, 1.5f);
                }
            }
            /* It might be fine that this is called in this script. But I am having trouble
             * Writing code that is not redundant. So we (you) may need to refactor this. 
             * Also, we need to deactivate the Enemy state machine */

        }
    }

    private void Die()
    {
        SetLimbPhysics(false);

        GetComponent<Animator>().enabled = false;
        Destroy(GetComponent<CharacterController>());
    }

    private void SetLimbPhysics(bool isAlive) //I got lazy and just copy and pasted this lol
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Rigidbody rigidbody in rigidbodies) //True if alive
        { rigidbody.isKinematic = isAlive; } //Kinematic means not affected by gravity

        foreach (Collider collider in colliders)
        { collider.enabled = !isAlive; } // False if alive

        GetComponent<CharacterController>().enabled = isAlive;
        GetComponent<Collider>().enabled = isAlive;

    }
}
