using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Nama Scene Tujuan")]
    public string sceneToLoad;

    [Header("Delay Sebelum Teleport (detik)")]
    public float delayBeforeTeleport = 1.5f;

    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTeleporting) return;

        if (other.CompareTag("Player"))
        {
            isTeleporting = true;
            StartCoroutine(DelayTeleport());
        }
    }

    private System.Collections.IEnumerator DelayTeleport()
    {
        yield return new WaitForSeconds(delayBeforeTeleport);
        SceneManager.LoadScene(sceneToLoad);
    }
}
