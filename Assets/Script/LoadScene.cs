using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject levelSelectionUI;
    public GameObject pausePanel;
    public GameObject creditsPanel;

    [Header("Game Over")]
    public GameObject gameOverPanel;

    public void ShowLevelSelection()
    {
        mainMenuUI.SetActive(false);
        creditsPanel.SetActive(false);
        levelSelectionUI.SetActive(true);
    }

    public void ShowCredits() // tambahkan ini
    {
        mainMenuUI.SetActive(false);
        creditsPanel.SetActive(true);
    }


    public void ShowMainMenu()
    {
        levelSelectionUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void ChangeScene(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // pastikan game tidak freeze
        SceneManager.LoadScene("MainMenu"); // ganti dengan nama scene Main Menu kamu
    }

    public void quitGame()
    {
        Debug.Log("Quit Game dipanggil.");
        Application.Quit();
    }

    public void paused()
    {
        Time.timeScale = 0f;
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void resume()
    {
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false); // ini yang kamu butuhin!
    }
    
    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}