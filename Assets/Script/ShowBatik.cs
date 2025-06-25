using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBatik : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         // Load sprite dari Resources/Batik/batik1.png
        Sprite batikSprite = Resources.Load<Sprite>("Batik/batik1");

        // Buat GameObject kosong
        GameObject batikObj = new GameObject("BatikImage");

        // Tambahkan SpriteRenderer
        SpriteRenderer sr = batikObj.AddComponent<SpriteRenderer>();
        sr.sprite = batikSprite;

        // Atur posisi batik (contoh: posisi x=0, y=0)
        batikObj.transform.position = new Vector2(0, 0);

        // (Opsional) Skala kalau terlalu besar/kecil
        batikObj.transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
