using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    AudioSource theAudio;
    NoteManager theNote;

    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        theNote = FindObjectOfType<NoteManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            theAudio.Play();
            PlayerController.s_canPresskey = false;
            theNote.RemoveNote();
        }
    }
}
