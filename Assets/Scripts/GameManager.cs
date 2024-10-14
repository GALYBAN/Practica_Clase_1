using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private int coins = 0;

    private bool isPaused;
    [SerializeField] GameObject _pauseCanvas;
    [SerializeField] Text _coinText;

    void Start()
    {
        BGMManager.instance.PlayBGM(BGMManager.instance.BGAudioClip);
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
    }

    public void Pause()
    {
        if(!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            _pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            _pauseCanvas.SetActive(false);  
        }
    }
    public void AddCoin()
    {
        coins++;
        _coinText.text = coins.ToString();
    }    
}
