using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    [HideInInspector] public AudioSource _audioSourceBGM;
    public AudioClip BGAudioClip;
 void Awake()
 {
    if(instance != null && instance != this)
    {
        Destroy(gameObject);
    }
    else
    {
        instance = this;
    }

    _audioSourceBGM = GetComponent<AudioSource>();

    _audioSourceBGM.loop = true;
    _audioSourceBGM.mute = false;
    _audioSourceBGM.volume = 0.5f;
    
 }

    public void PlayBGM(AudioClip clip)
    {
        _audioSourceBGM.clip = clip;
        _audioSourceBGM.Play();
    }

    public void StopBGM()
    {
        _audioSourceBGM.Stop();
    }

    public void PauseBGM()
    {
        _audioSourceBGM.Pause();
    }

}
