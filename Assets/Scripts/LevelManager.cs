using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [field: SerializeField] public int RemainingEnemies { get; private set; }
    [field: SerializeField] public bool IsNecroDead { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnemySpawned()
    { RemainingEnemies++; }

    public void EnemyDied()
    { RemainingEnemies--; }

    public void NecroDead(bool isNecroDead)
    {
        this.IsNecroDead = isNecroDead;
    }

    // Update is called once per frame
    void Update()
    {


        if (RemainingEnemies == 0 && IsNecroDead)
        {
            //Pause game, load Level Win canvas UI. w/ buttons for Main Menu and next level.

            Debug.Log("Level Won!");
        }
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ReloadLevel()
    {
        //pause game, load death canvas with buttons for main menu or replay level.
        Debug.Log("Level lost");
    }

}
