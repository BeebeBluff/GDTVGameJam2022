using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [field: SerializeField] public int RemainingEnemies { get; private set; }
    [field: SerializeField] public bool IsNecroDead { get; private set; }

    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject returnToMenuScreen;

    [SerializeField] private Slider xSlider;
    [SerializeField] private Slider ySlider;
    private CinemachineFreeLook cinemachineCamera;

    float deathDelay = 3f;

    /*
    private void Awake()
    {
        int numLevelManagers = FindObjectsOfType<LevelManager>().Length;
        int numMenuManagers = FindObjectsOfType<MenuManager>().Length;

        if (numLevelManagers > 1) //New level was loaded. Restore sensitivity settings.
        { Destroy(gameObject); }
        else if (numMenuManagers > 0)
        { Destroy(gameObject); }
        else
        { DontDestroyOnLoad(gameObject); }
    }*/

    void Start()
    {
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        returnToMenuScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetCameraSensitivity();
    }

    private void SetCameraSensitivity()
    {
        cinemachineCamera = FindObjectOfType<CinemachineFreeLook>();
        cinemachineCamera.enabled = true; //Just in case.

        XSliderMove();
        YSliderMove();
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
                cinemachineCamera.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void XSliderMove()
    {
        xSlider.onValueChanged.AddListener((v) =>
        {
            cinemachineCamera.m_XAxis.m_MaxSpeed = v;
        });

    }

    private void YSliderMove()
    {
        ySlider.onValueChanged.AddListener((v) =>
        {
            cinemachineCamera.m_YAxis.m_MaxSpeed = v;
        });
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
        cinemachineCamera.enabled = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        cinemachineCamera.enabled = true;
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
        cinemachineCamera.enabled = false;
        FindObjectOfType<InputReader>().DisableControls();
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        cinemachineCamera.enabled = true;
        FindObjectOfType<InputReader>().EnableControls();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        returnToMenuScreen.SetActive(false);
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        cinemachineCamera.enabled = false;

        loseScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
