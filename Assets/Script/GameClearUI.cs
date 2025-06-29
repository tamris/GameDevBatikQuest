using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnReplayButton()
    {
        // Ulang dari level 1
        SceneManager.LoadScene("level1"); // Ganti sesuai nama scene pertama
    }

    public void OnHomeButton()
    {
        // Kembali ke Main Menu
        SceneManager.LoadScene("MainMenu"); // Ganti sesuai nama scene main menu kamu
    }
}
