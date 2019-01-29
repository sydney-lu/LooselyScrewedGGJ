using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource MenuAudio;

    void Start()
    {
        MenuAudio.Play(0);
        Debug.Log("Music Start");
    }

    void Update()
    {

    }
}
