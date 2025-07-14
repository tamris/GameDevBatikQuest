using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private LoadScene loadScene;

    void Start()
    {
        loadScene = FindObjectOfType<LoadScene>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player jatuh ke jurang");
            loadScene.GameOver(); // Pastikan fungsi ini ada
        }
    }
}
