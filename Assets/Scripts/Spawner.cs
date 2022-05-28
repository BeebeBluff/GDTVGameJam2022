using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 8f;
    float timer;
    [SerializeField] GameObject spawnPrefab;

    private static readonly string NECROMANCER_NAME = "Necromancer";

    private void Start()
    {
        timer = spawnDelay;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && GameObject.Find(NECROMANCER_NAME) != null)
        {
            Instantiate(spawnPrefab, gameObject.transform.position, Quaternion.identity);

            timer = spawnDelay;
        }
    }
}
