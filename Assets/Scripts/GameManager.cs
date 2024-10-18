using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private int coins = 0;


    private bool isPaused;
    [SerializeField] GameObject _pauseCanvas;
    [SerializeField] Text _coinText;

    [SerializeField] Slider _healthSlider;
    private Animator _pausePanelAnimator;

    public bool pauseAnimation = false;

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

        _pausePanelAnimator = _pauseCanvas.GetComponentInChildren<Animator>();
    }

    public void Pause()
    {
        if(!isPaused && !pauseAnimation)
        {
            Time.timeScale = 0;
            isPaused = true;
            _pauseCanvas.SetActive(true);
        }
        else if(isPaused && !pauseAnimation)
        {
            pauseAnimation = true;

             StartCoroutine(Resume());
        }
    }

    IEnumerator Resume()
    {
        _pausePanelAnimator.SetBool("Close", true);

        yield return new WaitForSecondsRealtime(1);
        
        Time.timeScale = 1;
        _pauseCanvas.SetActive(false);
        isPaused = false;
        pauseAnimation=false;

    }

    public void AddCoin()
    {
        coins++;
        _coinText.text = coins.ToString();
    }    

    public void SetHealthBar(int maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
    }

    public void UpdateHealtBar(int health)
    {
        _healthSlider.value = health;
    }

    public void AddHealth(int health)
    {
        _healthSlider.value += health;
    }

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
