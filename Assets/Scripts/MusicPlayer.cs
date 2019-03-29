using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    static bool playing = false;
    bool stop = false;
    private AudioSource audioSource;
    public AudioClip song;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!playing)
        {
            DontDestroyOnLoad(this);
            playing = true;
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!stop)
            {
                audioSource.Pause();
                stop = true;
            }

            else
            {
                audioSource.UnPause();
                stop = false;
            }     
        }
    }
}
