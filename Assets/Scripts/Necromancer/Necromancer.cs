using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    private static readonly string PLAYER_ARROW_STRING = "PlayerArrow";
    private static readonly string DEAD_NECRO_NAME = "DeadNecromancer";
    private static readonly string ONE_SHOT_NAME = "One shot audio";

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip chantingSound;
    [SerializeField] private AudioClip deathSound;

    GameObject chantObject;
    float chantingLength;
    float timer;
    bool isNecroAlive = true;


    private void Start()
    {
        chantingLength = chantingSound.length;
        timer = chantingLength;

        SetLimbPhysics(true);

        FindObjectOfType<LevelManager>().NecroDead(false);

        StartChant();
    }

    private void Update()
    {
        KeepChanting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_ARROW_STRING))
        {
            isNecroAlive = false;
            Die(other);
        }
    }

    private void StartChant()
    {
        AudioSource.PlayClipAtPoint(chantingSound, gameObject.transform.position);

        /*
        chantObject = Instantiate(new GameObject("One Shot Sound"), gameObject.transform.position, Quaternion.identity);
        chantObject.AddComponent<AudioSource>();
        AudioSource chantAudioSource = chantObject.GetComponent<AudioSource>();

        chantAudioSource.clip = chantingSound;
        chantAudioSource.Play();
        */
    }

    private void KeepChanting()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && isNecroAlive)
        {
            StartChant();

            timer = chantingLength;
        }
    }

    private void Die(Collider other)
    {
        gameObject.name = DEAD_NECRO_NAME; //Do this so spawner's won't find it.

        FindObjectOfType<LevelManager>().NecroDead(true);

        if (GameObject.Find(ONE_SHOT_NAME))
        { Destroy(GameObject.Find(ONE_SHOT_NAME)); }

        audioSource.PlayOneShot(deathSound);

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
