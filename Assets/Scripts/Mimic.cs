using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{
    private Animator mimicAnimator;

    [SerializeField]private int healthPoints = 3;

    void Awake()
    {
        mimicAnimator = GetComponent<Animator>();

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            AttackMimic();
        }
    }

    void AttackMimic()
    {
        mimicAnimator.SetTrigger("Bite");
    }

    public void TakeDamageMimic()
    {
        healthPoints--;

        if(healthPoints <= 0)
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
