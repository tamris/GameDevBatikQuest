using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batik : MonoBehaviour
{
    
    public int batikValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if(player != null)
        {
            player.addBatik(batikValue);
            Destroy(gameObject);
        }
    }
}
