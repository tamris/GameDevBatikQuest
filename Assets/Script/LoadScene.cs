using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    
    public GameObject mainMenuUI;
    public GameObject levelSelectionUI;

    public void ShowLevelSelection()
    {
        mainMenuUI.SetActive(false);
        levelSelectionUI.SetActive(true);
    }


    public void ShowMainMenu()
    {
    levelSelectionUI.SetActive(false);
    mainMenuUI.SetActive(true);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void paused()
    {
        Time.timeScale = 0;
    }

    public void resume()
    {
        Time.timeScale = 1;
    }
}