using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private bool interactable;

    private Animator _starAnimator;
    public StarUI starUI;

    // Start is called before the first frame update
    void Awake()
    {
        _starAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactable)
        {
            if (gameObject.CompareTag("Star1"))
            {
                starUI.UnlockStar(0);
            }
            else if (gameObject.CompareTag("Star2"))
            {
                starUI.UnlockStar(1);
            }
            else if (gameObject.CompareTag("Star3"))
            {
                starUI.UnlockStar(2);
            }
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSourceGlobal, SoundManager.instance.coinAudio);
            Destroy(gameObject);
        }     
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
            
        if(collider.gameObject.CompareTag("Player"))
        {
            interactable = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            interactable = false;
        }
    }

}
