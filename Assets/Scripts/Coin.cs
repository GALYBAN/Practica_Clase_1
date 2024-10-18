using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool interactable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            GameManager.instance.AddCoin();
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSourceGlobal, SoundManager.instance.coinAudio);
            Destroy(gameObject);
            interactable = true;
        }
    }

}
