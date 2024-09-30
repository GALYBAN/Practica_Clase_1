using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D characterRigdbody;
    private float horizontalInput;
    public static Animator characterAnimator;

    [SerializeField]private int healthPoints = 5;

    [SerializeField]private float characterSpeed = 4.5f;
    [SerializeField]private float jumpforce = 5f;

    private bool isAttacking;

    void Awake()
    {
        characterRigdbody = GetComponent<Rigidbody2D>();

        characterAnimator = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // characterRigdbody.AddForce(Vector2.up * jumpforce);
    }

    void Update()
    {

        Movement();

        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded && !isAttacking)
        {
            Jump();
        }

        if(Input.GetButtonDown("Fire1") && GroundSensor.isGrounded)
        {
            Attack();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterRigdbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigdbody.velocity.y);
    }
    void Movement()
    {

        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput == 0)
        {
            characterAnimator.SetBool("IsRunning", false);
        }

        if(isAttacking)
        {
            return;
        }
        else if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            characterAnimator.SetBool("IsRunning", true);
        }
        else if(horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            characterAnimator.SetBool("IsRunning", true);
        }

        /*if(horizontalInput < 0)
        {
            if(!isAttacking)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            characterAnimator.SetBool("IsRunning", true);
        }

        else if(horizontalInput > 0)
        {
            if(!isAttacking)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            characterAnimator.SetBool("IsRunning", true);
        }

        else
        {
            characterAnimator.SetBool("IsRunning", false);
        }*/
    }

    void Jump()
    {
        characterRigdbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
    }

    void Attack()
    {
        StartCoroutine(AttackingAnimation());
        characterAnimator.SetTrigger("IsAttacking");
    }

    IEnumerator AttackingAnimation()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
    }

    void TakeDamage()
    {
        healthPoints--;

        if(healthPoints <= 0)
        {
            Die();
            return;
        }

        characterAnimator.SetTrigger("IsHurt");

        

    }

     void Die()
    {
        characterAnimator.SetTrigger("IsDead");
        Destroy(gameObject, 0.45f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            TakeDamage();            
        }

        if(collision.gameObject.layer == 7)
        {
            Die();
        }
    }
}
