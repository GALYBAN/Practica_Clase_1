using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    [HideInInspector] public AudioSource _audioSourceBGM;
    
    public AudioClip[] bgmClips;
    public string[] sceneNames;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        PlayBGM(currentScene.name);
    }

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

    public void PlayBGM(string sceneName)
    {
        int index = System.Array.IndexOf(sceneNames, sceneName);
        if (index != -1)
        {  
            _audioSourceBGM.Stop();
            _audioSourceBGM.clip = bgmClips[index];
            _audioSourceBGM.Play();
        }
    }

}
