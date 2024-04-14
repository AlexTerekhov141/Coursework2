using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    private AudioSource _audioSource;
    private float musicVolume = 0.1f;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _audioSource.volume = musicVolume;
    }

    public void SetVolume(float val)
    {
        musicVolume = val;
    }
}
