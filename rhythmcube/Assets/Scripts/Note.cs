using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;

    Image noteImage;

    void Start()
    {
        noteImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

}
