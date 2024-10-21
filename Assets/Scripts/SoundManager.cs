using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [HideInInspector] public AudioSource _audioSourceGlobal;

    public AudioClip coinAudio;
    public AudioClip jumpAudio;
    public AudioClip hurtAudio;

    public AudioClip mimicAudio;
    public AudioClip mimicLoopAudio;
    public AudioClip[] swordAttack;

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

        DontDestroyOnLoad(gameObject);

        _audioSourceGlobal = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioSource source, AudioClip clip) 
    {
        source.PlayOneShot(clip);
    }
    public void PlayLoop(AudioSource sourceLoop, AudioClip clipLoop)
    {
        sourceLoop.clip = clipLoop;
        sourceLoop.Play();
    }


}
