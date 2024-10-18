using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{
    private Animator mimicAnimator;
    private AudioSource _audioSourceMimic;

    [SerializeField]private int currentHealth = 3;

    void Awake()
    {
        mimicAnimator = GetComponent<Animator>();
        _audioSourceMimic = GetComponent<AudioSource>();
    }

    void Start()
    {
        SoundManager.instance.PlayLoop(_audioSourceMimic, SoundManager.instance.mimicLoopAudio);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            AttackMimic();
        }
    }

    void AttackMimic()
    {
        mimicAnimator.SetTrigger("Bite");
        SoundManager.instance.PlaySFX(_audioSourceMimic, SoundManager.instance.mimicAudio);
    }

    public void TakeDamageMimic()
    {
        currentHealth--;

        if(currentHealth <= 0)
        {
            DieMimic();
            return;
        }

        mimicAnimator.SetTrigger("IsHurt");
    }

    void DieMimic()
    {
        mimicAnimator.SetTrigger("IsDead");
        Destroy(gameObject, 0.60f);
    }
}
