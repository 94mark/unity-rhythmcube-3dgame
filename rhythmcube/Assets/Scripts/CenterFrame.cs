using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrame : MonoBehaviour
{
    AudioSource myAudio;
    bool musicStart = false;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                myAudio.Play();
                musicStart = true;
            }
        }
    }
}
