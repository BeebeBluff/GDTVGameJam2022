using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject creditsScreen;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadCreditsScreen()
    {
        if (creditsScreen != null)
        { creditsScreen.SetActive(true); }
    }

    public void CloseCreditsScreen()
    {
        if (creditsScreen != null)
        { creditsScreen.SetActive(false); }
    }

}
