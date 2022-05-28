using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 8f;
    [SerializeField] GameObject spawnPrefab;

    private static readonly string NECROMANCER_NAME = "Necromancer";


    void Update()
    {
        spawnDelay -= Time.deltaTime;

        if (spawnDelay <= 0f && GameObject.Find(NECROMANCER_NAME) != null)
        {
            Instantiate(spawnPrefab, gameObject.transform.position, Quaternion.identity);

            spawnDelay = 8f;
        }
    }
}
