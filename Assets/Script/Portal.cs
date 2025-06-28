using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [Header("Nama Scene Tujuan")]
    public string sceneToLoad;

    [Header("Delay Sebelum Teleport (detik)")]
    public float delayBeforeTeleport = 1.5f;

    [Header("Syarat Batik")]
    public int batikRequired = 4;

    [Header("UI Warning")]
    public GameObject warningCanvas; // Canvas atau panel yang muncul kalau belum cukup

    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTeleporting) return;

        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                if (player.currentCoin >= batikRequired)
                {
                    isTeleporting = true;
                    StartCoroutine(DelayTeleport());
                }
                else
                {
                    // Tampilkan warning UI
                    if (warningCanvas != null)
                    {
                        warningCanvas.SetActive(true);
                        // Sembunyikan lagi setelah 2 detik
                        Invoke(nameof(HideWarning), 2f);
                    }
                }
            }
        }
    }

    private void HideWarning()
    {
        if (warningCanvas != null)
            warningCanvas.SetActive(false);
    }

    private System.Collections.IEnumerator DelayTeleport()
    {
        yield return new WaitForSeconds(delayBeforeTeleport);
        SceneManager.LoadScene(sceneToLoad);
    }
}
