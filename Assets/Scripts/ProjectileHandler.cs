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
        GetComponent<InputReader>().AttackEvent += InputReader_AttackEvent;

        cosmeticArrow.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        GetComponent<InputReader>().AttackEvent -= InputReader_AttackEvent;
    }

    private void InputReader_AttackEvent()
    {

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
