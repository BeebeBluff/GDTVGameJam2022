using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [field: SerializeField] public int RemainingEnemies { get; private set; }
    [field: SerializeField] public bool IsNecroDead { get; private set; }

    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject returnToMenuScreen;

    float deathDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        returnToMenuScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (RemainingEnemies == 0 && IsNecroDead)
        {
            deathDelay -= Time.deltaTime;

            if (deathDelay <= 0f)
            {
                Time.timeScale = 0;
                winScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void EnemySpawned()
    { RemainingEnemies++; }

    public void EnemyDied()
    { RemainingEnemies--; }

    public void NecroDead(bool isNecroDead)
    {
        this.IsNecroDead = isNecroDead;
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMenuScene()
    {
        Time.timeScale = 1; //Why don't these time.timescale = 1 work?!
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowReturnToMenuScreen()
    {
        returnToMenuScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        returnToMenuScreen.SetActive(false);
    }

    public void GamePause()
    {
        Time.timeScale = 0;

        //FindObjectOfType<InputReader>().DisableControls();

        loseScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
