using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource _audioSourceGlobal;

    public AudioClip coinAudio;
    public AudioClip jumpAudio;
    public AudioClip attackAudio;
    public AudioClip hurtAudio;
    public AudioClip mimicAudio;
    public AudioClip[] swordAttack;

    [SerializeField] private AudioClip _coinAudio; 
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

        _audioSourceGlobal = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioSource source, AudioClip clip) 
    {
        source.PlayOneShot(clip);
    }

    public void PlayLoop(AudioSource sourceLoop, AudioClip clipLoop)
    {
        sourceLoop.Play(clipLoop);
    }

}
