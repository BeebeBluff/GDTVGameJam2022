using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private static readonly string ENEMY_WEAPON_TAG = "EnemyWeapon";

    public Action PlayerDeathEvent;

    private void OnTriggerEnter(Collider other) //Enemy weapon has tag "EnemyWeapon"
    {
        if (other.gameObject.CompareTag(ENEMY_WEAPON_TAG))
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        //FindObjectOfType<Animator>().CrossFadeInFixedTime(DEATH_ANIMATION_HASH, DEATH_TRANSITION_TIME);

        PlayerDeathEvent?.Invoke();

        Destroy(GetComponent<CharacterController>());
    }
}
