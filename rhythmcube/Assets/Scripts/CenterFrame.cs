using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrame : MonoBehaviour
{
    bool musicStart = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                AudioManager.instance.PlayBGM("BGM0");
                musicStart = true;
            }
        }
    }
}
