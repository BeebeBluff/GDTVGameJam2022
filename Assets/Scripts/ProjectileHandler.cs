using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] Transform arrowSpawnLocation;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject cosmeticArrow;

    [SerializeField] float arrowSpeed = 20f;

    private void Start()
    {
        cosmeticArrow.SetActive(false);
    }

    public void LoadArrow()
    {
        cosmeticArrow.SetActive(true);
    }

    public void ShootArrow() //Animation Event
    {
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnLocation.position, transform.rotation);
        newArrow.GetComponent<Rigidbody>().velocity = transform.forward * arrowSpeed;

        cosmeticArrow.SetActive(false);
    }
}
