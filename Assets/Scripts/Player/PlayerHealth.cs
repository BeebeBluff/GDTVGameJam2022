using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private static readonly string ENEMY_WEAPON_TAG = "EnemyWeapon";

    private void OnTriggerEnter(Collider other) //Enemy weapon has tag "EnemyWeapon"
    {
        if (other.gameObject.CompareTag(ENEMY_WEAPON_TAG))
        {
            Debug.Log("OUCH! I'm hit!");
        }
    }
}
